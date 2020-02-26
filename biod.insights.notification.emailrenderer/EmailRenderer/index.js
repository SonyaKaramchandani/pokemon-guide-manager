const fs = require("fs").promises;
const mjml2html = require("mjml");
const Handlebars = require("handlebars");
const config = require("./config.json");
const { analyticsHtml, gaURIComponent } = require("./analytics");

const mjmlOptions = {};
const emailsFolder = "emails";

Handlebars.registerHelper(
  "gaURIComponent",
  (...params) => new Handlebars.SafeString(gaURIComponent(...params))
);

module.exports = async function(context, req) {
  context.log("JavaScript HTTP trigger function processed a request.");

  const emailName = req.query.name || (req.body && req.body.name);
  const reqData = req.query.data || (req.body && req.body.data);
  const data = (reqData && JSON.parse(reqData)) || {};

  context.log(`Loading mjml for email ${emailName}`);
  const mjmlTemplatePath =
    __dirname + `/${emailsFolder}/${emailName.toLowerCase()}.mjml`;
  const emailContent = await fs.readFile(mjmlTemplatePath);

  context.log(`Loaded mjml for ${emailName}. Compiling mjml template.`);
  const template = Handlebars.compile(emailContent.toString());

  const analytics = analyticsHtml(data, emailName, config);
  context.log(`Generating analytics html`, analytics);

  context.log(`Injecting data into ${emailName} mjml.`);
  const htmlOutput = mjml2html(
    template({
      ...data,
      emailName,
      config,
      analyticsHtml: analytics
    }),
    {
      ...mjmlOptions,
      filePath: mjmlTemplatePath
    }
  );

  if (emailName) {
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
