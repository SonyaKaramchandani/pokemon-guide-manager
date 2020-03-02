const { gaHtml, gaURIComponent } = require("./googleAnalytics");
const { litmusHtml } = require("./litmusAnalytics");

/**
 *
 * @param {Object} data data to be injected into the email
 * @param {number} emailType type of email to render
 * @param {Object} config email renderer configuration
 */
function analyticsHtml(data, emailType, config) {
  const { isDoNotTrackEnabled } = data;

  if (isDoNotTrackEnabled) {
    return "";
  }

  const markup = `
    ${gaHtml(data, emailType, config)}
    ${litmusHtml(data, emailType, config)}
  `;

  return markup;
}

module.exports = {
  analyticsHtml,
  gaURIComponent
};
