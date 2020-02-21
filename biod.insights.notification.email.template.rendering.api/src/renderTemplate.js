const fs = require("fs");
const mjml2html = require("mjml");
const { compile } = require("handlebars");

const mjmlOptions = {};

function getTemplate(templateName, cb) {
  fs.readFile(
    __dirname + `/templates/${templateName.toLowerCase()}.mjml`,
    function(err, data) {
      cb(data.toString());
    }
  );
}

function render(templateName, data, cb) {
  getTemplate(templateName, templateContent => {
    const template = compile(templateContent);
    const htmlOutput = mjml2html(template(data), mjmlOptions);
    cb(htmlOutput);
  });
}

module.exports = render;
