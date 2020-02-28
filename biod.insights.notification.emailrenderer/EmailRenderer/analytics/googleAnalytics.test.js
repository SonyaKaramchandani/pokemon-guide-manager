const { gaHtml, gaURIComponent } = require("./googleAnalytics");
const config = {
  gaCampaigns: {
    "a-test-email": "a-test-email"
  }
};

describe("google analytics", () => {
  const emailName = "a-test-email";

  test("google analytics disabled", () => {
    const html = gaHtml({}, emailName, {
      ...config,
      isGoogleAnalyticsEnabled: false,
      gaTrackingId: "UA-TEST"
    });

    expect(html).toBe("");
  });

  test("google analytics enabled", () => {
    const UserId = "UID-TEST";
    const EventId = "EVENT-TEST";
    const html = gaHtml(
      {
        UserId,
        EventId
      },
      emailName,
      {
        ...config,
        isGoogleAnalyticsEnabled: true,
        gaTrackingId: "GA-TEST"
      }
    );

    expect(html).toContain(UserId);
    expect(html).toContain(EventId);
    expect(html).toContain(emailName);
  });

  test("gaURIComponent", () => {
    const UserId = "UID-TEST";
    const EventId = "EVENT-TEST";
    const qs = gaURIComponent(
      emailName,
      {
        ...config,
        isGoogleAnalyticsEnabled: true
      },
      UserId,
      EventId
    );

    expect(qs).toContain(UserId);
    expect(qs).toContain(EventId);
    expect(qs).toContain(emailName);
  });
});
