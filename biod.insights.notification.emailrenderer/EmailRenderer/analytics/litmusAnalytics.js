function litmusHtml(data, emailType, config) {
  const {
    litmusTrackingId,
    isLitmusAnalyticsEnabled,
  } = config;

  if (isLitmusAnalyticsEnabled) {
    const { userId, sentDate, email } = data;
    const litmusTrackingUrl = `https://${litmusTrackingId}.emltrk.com/${litmusTrackingId}`;
    const customData = encodeURIComponent(
      [userId, "", sentDate, `${emailType}`].join("|")
    );

    return `
    <style data-ignore-inlining=data-ignore-inlining>
        @@media print{ #_t { background-image: url('${litmusTrackingUrl}?p&d=${email}&t=${customData}');}}
        div.OutlookMessageHeader {background-image:url('${litmusTrackingUrl}?f&d=${email}&t=${customData}')}
        table.moz-email-headers-table {background-image:url('${litmusTrackingUrl}?f&d=${email}&t=${customData}')}
        blockquote #_t {background-image:url('${litmusTrackingUrl}?f&d=${email}&t=${customData}')}
        #MailContainerBody #_t {background-image:url('${litmusTrackingUrl}?f&d=${email}&t=${customData}')}
    </style>
    <div id="_t"></div>
    <img src="${litmusTrackingUrl}?d=${email}&t=${customData}" width="1" height="1" border="0" alt="" />
    `;
  }

  return "";
}

module.exports = {
  litmusHtml
};
