const analyticsConfig = require("../config.json");
const { gaHtml } = require("./googleAnalytics");
const { litmusHtml } = require("./litmusAnalytics");

/**
 *
 * @param {Object} data data to be injected into the email
 * @param {number} emailName name of email mjml file
 */
function analyticsHtml(data, emailName, config = analyticsConfig) {
  const { IsDoNotTrackEnabled } = data;

  if (IsDoNotTrackEnabled) {
    return "";
  }

  const markup = `
    ${gaHtml(data, emailName, config)}
    ${litmusHtml(data, emailName, config)}
  `;

  return markup;
}

module.exports = {
  analyticsHtml
};
