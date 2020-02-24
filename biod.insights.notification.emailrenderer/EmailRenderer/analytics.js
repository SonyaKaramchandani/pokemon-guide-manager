const analyticsConfig = require("./config.json");

const EmailTypes = {
  EMAIL_CONFIRMATION: 1,
  WELCOME_EMAIL: 2,
  RESET_PASSWORD_EMAIL: 3,
  EVENT_EMAIL: 4,
  WEEKLY_BRIEF_EMAIL: 5,
  PROXIMAL_EMAIL: 6
};

const UrlTracking = {
  UTM_CAMPAIGN_CONFIRMATION: "confirmation",
  UTM_CAMPAIGN_EVENT_ALERT: "alert_new",
  UTM_CAMPAIGN_PROXIMAL: "local_activity",
  UTM_CAMPAIGN_RESET_PASSWORD: "reset_password",
  UTM_CAMPAIGN_WEEKLY_BRIEF: "weekly_brief",
  UTM_CAMPAIGN_WELCOME: "welcome",
  UTM_MEDIUM_EMAIL: "email",
  UTM_SOURCE_EMAIL: "insights_email"
};

/**
 *
 * @param {Object} data
 * @param {number} emailType
 * @param {Object} config
 */
function analyticsHtml(
  data,
  emailType = EmailTypes.EMAIL_CONFIRMATION,
  config = analyticsConfig
) {
  const { IsDoNotTrackEnabled, UserId, SentDate, Email } = data;

  const {
    LitmusTrackingId,
    IsLitmusAnalyticsEnabled,
    IsGoogleAnalyticsEnabled,
    GaTrackingId
  } = config;

  if (IsDoNotTrackEnabled) {
    return "";
  }

  const markupArr = [];
  if (IsGoogleAnalyticsEnabled && GaTrackingId) {
    const qs =
      `tid=${GaTrackingId}&t=event&ec=Email&ea=Open&uid=${UserId}` +
      `&cm=${UrlTracking.UTM_MEDIUM_EMAIL}&cs=${UrlTracking.UTM_SOURCE_EMAIL}&cn=${UrlTracking.UTM_CAMPAIGN_CONFIRMATION}`;
    markupArr.push(
      `<img src="https://www.google-analytics.com/collect?v=1&${qs}" />`
    );
  }

  if (IsLitmusAnalyticsEnabled) {
    const litmusTrackingUrl = `https://${LitmusTrackingId}.emltrk.com/${LitmusTrackingId}`;
    const customData = encodeURIComponent(
      [UserId, "", SentDate, `${emailType}`].join("|")
    );

    markupArr.push(
      `<style data-ignore-inlining=data-ignore-inlining>@@media print{ #_t { background-image: url('${litmusTrackingUrl}?p&d=${Email}&t=${customData}');}} div.OutlookMessageHeader {background-image:url('${litmusTrackingUrl}?f&d=${Email}&t=${customData}')} table.moz-email-headers-table {background-image:url('${litmusTrackingUrl}?f&d=${Email}&t=${customData}')} blockquote #_t {background-image:url('${litmusTrackingUrl}?f&d=${Email}&t=${customData}')} #MailContainerBody #_t {background-image:url('${litmusTrackingUrl}?f&d=${Email}&t=${customData}')}</style><div id="_t"></div>`
    );
    markupArr.push(
      `<img src="${litmusTrackingUrl}?d=${Email}&t=${customData}" width="1" height="1" border="0" alt="" />`
    );
  }

  return markupArr.join("");
}

module.exports = {
  analyticsHtml,
  EmailTypes
};
