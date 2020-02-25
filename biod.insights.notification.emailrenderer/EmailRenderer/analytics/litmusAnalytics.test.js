const { litmusHtml } = require("./litmusAnalytics");
const config = require("../config.json");

describe("litmus analytics", () => {
  const emailName = "a-test-email";

  test("litmus analytics disabled", () => {
    const html = litmusHtml({}, emailName, {
      ...config,
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

    const html = litmusHtml(
      {
        UserId,
        SentDate,
        Email
      },
      emailName,
      {
        ...config,
        IsLitmusAnalyticsEnabled: true,
        LitmusTrackingId
      }
    );

    expect(html).toContain(UserId);
    expect(html).toContain(Email);
    expect(html).toContain(SentDate);
    expect(html).toContain(LitmusTrackingId);
    expect(html).toContain(emailName);
  });
});
