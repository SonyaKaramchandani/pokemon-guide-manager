const { litmusHtml } = require("./litmusAnalytics");

describe("litmus analytics", () => {
  const emailType = "a-test-email";

  test("litmus analytics disabled", () => {
    const html = litmusHtml({}, emailType, {
      isLitmusAnalyticsEnabled: false,
      litmusTrackingId: "LT-TEST"
    });

    expect(html).toBe("");
  });

  test("litmus analytics enabled", () => {
    const userId = "UID-TEST";
    const email = "test@email.com";
    const sentDate = "SENT-DATE";
    const litmusTrackingId = "LT-TEST";

    const html = litmusHtml(
      {
        userId,
        sentDate,
        email
      },
      emailType,
      {
        isLitmusAnalyticsEnabled: true,
        litmusTrackingId
      }
    );

    expect(html).toContain(userId);
    expect(html).toContain(email);
    expect(html).toContain(sentDate);
    expect(html).toContain(litmusTrackingId);
    expect(html).toContain(emailType);
  });
});
