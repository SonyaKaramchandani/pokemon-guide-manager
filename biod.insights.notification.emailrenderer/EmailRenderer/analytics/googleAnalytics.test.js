const { gaHtml, gaURIComponent } = require("./googleAnalytics");
const config = {
  gaCampaigns: {
    "a-test-email": "a-test-email"
  }
};

describe("google analytics", () => {
  const emailType = "a-test-email";

  test("google analytics disabled", () => {
    const html = gaHtml({}, emailType, {
      ...config,
      isGoogleAnalyticsEnabled: false,
      gaTrackingId: "UA-TEST"
    });

    expect(html).toBe("");
  });

  test("google analytics enabled", () => {
    const userId = "UID-TEST";
    const eventId = "EVENT-TEST";
    const html = gaHtml(
      {
        userId,
        eventId
      },
      emailType,
      {
        ...config,
        isGoogleAnalyticsEnabled: true,
        gaTrackingId: "GA-TEST"
      }
    );

    expect(html).toContain(userId);
    expect(html).toContain(eventId);
    expect(html).toContain(emailType);
  });

  test("gaURIComponent", () => {
    const userId = "UID-TEST";
    const eventId = "EVENT-TEST";
    const qs = gaURIComponent(
      emailType,
      {
        ...config,
        isGoogleAnalyticsEnabled: true
      },
      userId,
      eventId,
      false
    );

    expect(qs).toContain(userId);
    expect(qs).toContain(eventId);
    expect(qs).toContain(emailType);
  });

  test("gaURIComponent do not track", () => {
    const userId = "UID-TEST";
    const eventId = "EVENT-TEST";
    const qs = gaURIComponent(
      emailType,
      {
        ...config,
        isGoogleAnalyticsEnabled: true
      },
      userId,
      eventId,
      true // do not track
    );

    expect(qs).toBe("");
  });
});
