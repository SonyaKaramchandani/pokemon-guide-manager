const fs = require("fs").promises;
const mjml2html = require("mjml");
const Handlebars = require("handlebars");
const config = require("./config.json");
const handlebarsUtils = require("./utils/handlebarsUtils");
const { analyticsHtml, gaURIComponent } = require("./analytics");

const mjmlOptions = {};
const emailsFolder = "emails";

module.exports = async function(context, req) {
  context.log("JavaScript HTTP trigger function processed a request.");

  const emailType = req.query.type || (req.body && req.body.type);
  // Query data comes as string, request body is an object
  let data = (req.query.data && JSON.parse(req.query.data)) || (req.body && req.body.data) || {};

  if (process.env.AZURE_FUNCTIONS_ENVIRONMENT === "Development") {
    const reqDataFile = req.query.dataFile;
    if (reqDataFile) {
      const dataFileContent = await fs.readFile(
        `${__dirname}/../sample-data/${reqDataFile}.json`
      );
      data = (dataFileContent && JSON.parse(dataFileContent)) || data;
    }
  }

  context.log(`Mapping email type ${emailType} to file`);
  const emailName = config.filenameMappings[emailType];

  context.log(`Found file ${emailName} for type ${emailType}`);

  if (emailType && emailName) {
    const mjmlTemplatePath = `${__dirname}/${emailsFolder}/${emailName.toLowerCase()}`;
    const emailContent = await fs.readFile(mjmlTemplatePath);

    context.log(`Compiling mjml template.`);
    const template = Handlebars.compile(emailContent.toString());

    const analytics = handlebarsUtils.analyticsHtml(data, emailType, config);
    context.log(`Generating analytics html`, analytics);

    context.log(`Injecting data into ${emailType} mjml.`);
    const htmlOutput = mjml2html(
      template({
        ...data,
        emailType,
        config,
        analyticsHtml: analytics
      }),
      {
        ...mjmlOptions,
        filePath: mjmlTemplatePath
      }
    );
    context.res = {
      // status: 200, /* Defaults to 200 */
      body: htmlOutput.html,
      headers: {
        "Content-Type": "text/html"
      },
      isRaw: true
    };
  } else {
    context.res = {
      status: 400,
      body:
        "Please pass an email name on the query string or in the request body"
    };
  }
};
