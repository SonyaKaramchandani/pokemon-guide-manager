const { gaHtml } = require("./googleAnalytics");
const { litmusHtml } = require("./litmusAnalytics");

/**
 *
 * @param {Object} data data to be injected into the email
 * @param {number} emailName name of email mjml file
 */
function analyticsHtml(data, emailName) {
  const { IsDoNotTrackEnabled } = data;

  if (IsDoNotTrackEnabled) {
    return "";
  }

  const markup = `
    ${gaHtml(data)}
    ${litmusHtml(data, emailName)}
  `;

  return markup;
}

module.exports = {
  analyticsHtml
};
