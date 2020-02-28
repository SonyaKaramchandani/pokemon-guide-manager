function gaHtml({ userId, eventId }, emailType, config) {
  const { isGoogleAnalyticsEnabled, gaTrackingId, gaCampaigns } = config;
  if (isGoogleAnalyticsEnabled) {
    const campaign = gaCampaigns[emailType];
    const qs =
      `tid=${gaTrackingId}&t=event&ec=Email&ea=Open` +
      `&cm=${gaCampaigns.UTM_MEDIUM_EMAIL}` +
      `&cs=${gaCampaigns.UTM_SOURCE_EMAIL}` +
      `&cn=${campaign}` +
      (userId ? `&uid=${userId}` : "") +
      (eventId ? `&cc=${eventId}` : "");

    return `<img src="https://www.google-analytics.com/collect?v=1&${qs}" />`;
  }

  return "";
}

function gaURIComponent(emailType, config, userId, eventId) {
  const { isGoogleAnalyticsEnabled, gaCampaigns } = config;

  if (isGoogleAnalyticsEnabled) {
    const campaign = gaCampaigns[emailType];
    const qs =
      `utm_medium=${gaCampaigns.UTM_MEDIUM_EMAIL}` +
      `&utm_source=${gaCampaigns.UTM_SOURCE_EMAIL}` +
      `&utm_campaign=${campaign}` +
      (userId ? `&userId=${userId}` : "") +
      (eventId ? `&utm_content=${eventId}` : "");

    return qs;
  }

  return "";
}

module.exports = {
  gaHtml,
  gaURIComponent
};
