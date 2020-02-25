const { analyticsHtml } = require(".");

describe("analytics", () => {
  test("do not track", () => {
    const html = analyticsHtml({ IsDoNotTrackEnabled: true });

    expect(html).toBe("");
  });
});
