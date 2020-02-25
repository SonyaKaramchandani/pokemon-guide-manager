const { analyticsHtml } = require(".");

describe("analytics", () => {
  test("do not track", () => {
    const html = analyticsHtml({ IsDoNotTrackEnabled: true });

    expect(html).toBe("");
  });

  test("generate html", () => {
    const emailName = "a-test-email";
    const UserId = "UID";
    const EventId = "EID";
    const SentDate = "SENT-DATE";
    const Email = "a@b.com";

    const html = analyticsHtml(
      {
        UserId,
        EventId,
        SentDate,
        Email
      },
      emailName
    );

    expect(html).toContain(emailName);
    expect(html).toContain(EventId);
    expect(html).toContain(SentDate);
    expect(html).toContain(SentDate);
    expect(html).toContain(Email);
  });
});
