const { analyticsHtml, EmailTypes } = require("./analytics");

describe("analytics", () => {
  test("do not track", () => {
    const html = analyticsHtml({ IsDoNotTrackEnabled: true });

    expect(html).toBe("");
  });

  test("google analytics disabled", () => {
    const html = analyticsHtml({ IsDoNotTrackEnabled: false }, null, {
      IsGoogleAnalyticsEnabled: false,
      GaTrackingId: "UA-TEST"
    });

    expect(html).toBe("");
  });

  test("google analytics enabled", () => {
    const html = analyticsHtml(
      {
        IsDoNotTrackEnabled: false,
        UserId: "UID-TEST"
      },
      null,
      {
        IsGoogleAnalyticsEnabled: true,
        GaTrackingId: "GA-TEST"
      }
    );

    expect(html).toBe(
      `<img src="https://www.google-analytics.com/collect?v=1&tid=GA-TEST&t=event&ec=Email&ea=Open&uid=UID-TEST&cm=email&cs=insights_email&cn=confirmation" />`
    );
  });

  test("litmus analytics disabled", () => {
    const html = analyticsHtml({ IsDoNotTrackEnabled: false }, null, {
      IsLitmusAnalyticsEnabled: false,
      LitmusTrackingId: "LT-TEST"
    });

    expect(html).toBe("");
  });

  test("litmus analytics enabled", () => {
    const UserId = "UID-TEST";
    const Email = "test@email.com";
    const SentDate = "SENT-DATE";
    const LitmusTrackingId = "LT-TEST";
    const EmailType = 1;

    const expectedHtml = `<style data-ignore-inlining=data-ignore-inlining>@@media print{ #_t { background-image: url('https://LT-TEST.emltrk.com/${LitmusTrackingId}?p&d=${Email}&t=${UserId}%7C%7C${SentDate}%7C${EmailType}');}} div.OutlookMessageHeader {background-image:url('https://${LitmusTrackingId}.emltrk.com/${LitmusTrackingId}?f&d=${Email}&t=${UserId}%7C%7C${SentDate}%7C${EmailType}')} table.moz-email-headers-table {background-image:url('https://${LitmusTrackingId}.emltrk.com/${LitmusTrackingId}?f&d=${Email}&t=${UserId}%7C%7C${SentDate}%7C${EmailType}')} blockquote #_t {background-image:url('https://${LitmusTrackingId}.emltrk.com/${LitmusTrackingId}?f&d=${Email}&t=${UserId}%7C%7C${SentDate}%7C${EmailType}')} #MailContainerBody #_t {background-image:url('https://${LitmusTrackingId}.emltrk.com/${LitmusTrackingId}?f&d=${Email}&t=${UserId}%7C%7C${SentDate}%7C${EmailType}')}</style><div id=\"_t\"></div><img src=\"https://${LitmusTrackingId}.emltrk.com/${LitmusTrackingId}?d=${Email}&t=${UserId}%7C%7C${SentDate}%7C${EmailType}\" width=\"1\" height=\"1\" border=\"0\" alt=\"\" />`;

    const html = analyticsHtml(
      {
        IsDoNotTrackEnabled: false,
        UserId,
        SentDate,
        Email
      },
      "a-test-email",
      {
        IsLitmusAnalyticsEnabled: true,
        LitmusTrackingId
      }
    );

    expect(html).toBe(expectedHtml);
  });
});
