const fs = require("fs").promises;
const mjml2html = require("mjml");
const Handlebars = require("handlebars");
const defaultConfig = require("./config.json");
const constants = require("./constants.json");
const handlebarsUtils = require("./utils/handlebarsUtils");

const mjmlOptions = {
  minify: true
};

// Create the config object from the config.json, but use the environment variable if it exists
const config = Object.keys(defaultConfig)
    .reduce((r, k) => {
      r[k] = process && process.env && process.env[k] !== undefined ? process.env[k] : defaultConfig[k];
      return r;
    }, {});

module.exports = async function(context, req) {
  context.log("JavaScript HTTP trigger function processed a request.");

  const emailType = req.query.type || (req.body && req.body.type);
  // Query data comes as string, request body is an object
  let data =
    (req.query.data && JSON.parse(req.query.data)) ||
    (req.body && req.body.data) ||
    {};

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
  const emailName = constants.filenameMappings[emailType];

  context.log(`Found file ${emailName} for type ${emailType}`);

  if (emailType && emailName) {
    const mjmlTemplatePath = `${__dirname}/${
        constants.emailFolder
    }/${emailName.toLowerCase()}`;
    const emailContent = await fs.readFile(mjmlTemplatePath);

    context.log(`Compiling mjml template.`);
    const template = Handlebars.compile(emailContent.toString());

    context.log(`Generating analytics html`);
    const analytics = handlebarsUtils.analyticsHtml(data, emailType, {...config, ...constants});

    context.log(`Injecting data into ${emailName}`);
    const htmlOutput = mjml2html(
      template({
        ...data,
        emailType,
        config: {...config, ...constants},
        analyticsHtml: analytics
      }),
      {
        ...mjmlOptions,
        filePath: mjmlTemplatePath
      }
    );
    context.log(`Request data injected ${JSON.stringify(data)}`);
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
        "Please pass an email type in the query string or in the request body"
    };
  }
};
