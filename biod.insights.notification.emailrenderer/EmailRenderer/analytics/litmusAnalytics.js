const analyticsConfig = require("../config.json");

function litmusHtml(data, emailName, config = analyticsConfig) {
  const {
    LitmusTrackingId,
    IsLitmusAnalyticsEnabled,
    LitmusCampaigns
  } = config;
  const emailType = LitmusCampaigns[emailName];
  const { UserId, SentDate, Email } = data;

  if (IsLitmusAnalyticsEnabled) {
    const litmusTrackingUrl = `https://${LitmusTrackingId}.emltrk.com/${LitmusTrackingId}`;
    const customData = encodeURIComponent(
      [UserId, "", SentDate, `${emailType}`].join("|")
    );

    return `
    <style data-ignore-inlining=data-ignore-inlining>
        @@media print{ #_t { background-image: url('${litmusTrackingUrl}?p&d=${Email}&t=${customData}');}}  
        div.OutlookMessageHeader {background-image:url('${litmusTrackingUrl}?f&d=${Email}&t=${customData}')} 
        table.moz-email-headers-table {background-image:url('${litmusTrackingUrl}?f&d=${Email}&t=${customData}')} 
        blockquote #_t {background-image:url('${litmusTrackingUrl}?f&d=${Email}&t=${customData}')} 
        #MailContainerBody #_t {background-image:url('${litmusTrackingUrl}?f&d=${Email}&t=${customData}')}
    </style>
    <div id="_t"></div>
    <img src="${litmusTrackingUrl}?d=${Email}&t=${customData}" width="1" height="1" border="0" alt="" />
    `;
  }

  return "";
}

module.exports = {
  litmusHtml
};
