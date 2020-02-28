const { litmusHtml } = require("./litmusAnalytics");
const config = {
  LitmusCampaigns: {
    "a-test-email": "a-test-email"
  }
};

describe("litmus analytics", () => {
  const emailName = "a-test-email";

  test("litmus analytics disabled", () => {
    const html = litmusHtml({}, emailName, {
      ...config,
      isLitmusAnalyticsEnabled: false,
      litmusTrackingId: "LT-TEST"
    });

    expect(html).toBe("");
  });

  test("litmus analytics enabled", () => {
    const UserId = "UID-TEST";
    const Email = "test@email.com";
    const SentDate = "SENT-DATE";
    const litmusTrackingId = "LT-TEST";

    const html = litmusHtml(
      {
        UserId,
        SentDate,
        Email
      },
      emailName,
      {
        ...config,
        isLitmusAnalyticsEnabled: true,
        litmusTrackingId
      }
    );

    expect(html).toContain(UserId);
    expect(html).toContain(Email);
    expect(html).toContain(SentDate);
    expect(html).toContain(litmusTrackingId);
    expect(html).toContain(emailName);
  });
});
