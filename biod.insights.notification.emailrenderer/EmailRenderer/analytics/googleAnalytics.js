/**
 * Generate html containing google analytics markup used for tracking email
 * @param {Object} param0 object containing userId and eventId
 * @param {number} emailType type of email to render
 * @param {Object} config email renderer configuration
 */
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

/**
 * Generate query string containing google analytics codes to be appended to a URL
 * @param {number} emailType type of email to render
 * @param {Object} config email renderer configuration
 * @param {string} userId user identifier
 * @param {number} eventId event identifier
 * @param {boolean} isDoNotTrackEnabled do not track
 */
function gaURIComponent(
  emailType,
  config,
  userId,
  eventId,
  isDoNotTrackEnabled = true
) {
  if (isDoNotTrackEnabled) {
    return "";
  }

  const { isGoogleAnalyticsEnabled, gaCampaigns } = config;

  if (isGoogleAnalyticsEnabled) {
    const campaign = gaCampaigns[emailType];
    const qs =
      `?utm_medium=${gaCampaigns.UTM_MEDIUM_EMAIL}` +
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
