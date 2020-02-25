const analyticsConfig = require("../config.json");

function gaHtml({ UserId, EventId }, emailName, config = analyticsConfig) {
  const { IsGoogleAnalyticsEnabled, GaTrackingId, GACampaigns } = config;
  const campaign = GACampaigns[emailName];

  if (IsGoogleAnalyticsEnabled) {
    const qs =
      `tid=${GaTrackingId}&t=event&ec=Email&ea=Open` +
      `&cm=${GACampaigns.UTM_MEDIUM_EMAIL}` +
      `&cs=${GACampaigns.UTM_SOURCE_EMAIL}` +
      `&cn=${campaign}` +
      (UserId ? `&uid=${UserId}` : "") +
      (EventId ? `&cc=${EventId}` : "");

    return `<img src="https://www.google-analytics.com/collect?v=1&${qs}" />`;
  }

  return "";
}

function gaURIComponent(
  { UserId, EventId },
  emailName,
  config = analyticsConfig
) {
  const { IsGoogleAnalyticsEnabled, GACampaigns } = config;
  const campaign = GACampaigns[emailName];

  if (IsGoogleAnalyticsEnabled) {
    const qs =
      `utm_medium=${GACampaigns.UTM_MEDIUM_EMAIL}` +
      `&utm_source=${GACampaigns.UTM_SOURCE_EMAIL}` +
      `&utm_campaign=${campaign}` +
      (UserId ? `&userId=${UserId}` : "") +
      (EventId ? `&utm_content=${EventId}` : "");

    return qs;
  }

  return "";
}

module.exports = {
  gaHtml,
  gaURIComponent
};
