const { analyticsHtml } = require(".");
const config = {
  gaCampaigns: {
    "a-test-email": "a-test-email"
  }
};

describe("analytics", () => {
  test("do not track", () => {
    const html = analyticsHtml({ isDoNotTrackEnabled: true });

    expect(html).toBe("");
  });

  test("generate html", () => {
    const emailName = "a-test-email";
    const userId = "UID";
    const eventId = "EID";
    const sentDate = "SENT-DATE";
    const email = "a@b.com";

    const html = analyticsHtml(
      {
        userId,
        eventId,
        sentDate,
        email
      },
      emailName,
      {
        ...config,
        isGoogleAnalyticsEnabled: true,
        isLitmusAnalyticsEnabled: true
      }
    );

    expect(html).toContain(emailName);
    expect(html).toContain(eventId);
    expect(html).toContain(sentDate);
    expect(html).toContain(sentDate);
    expect(html).toContain(email);
  });
});
