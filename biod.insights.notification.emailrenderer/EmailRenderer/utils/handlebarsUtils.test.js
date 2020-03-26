const handlebarsUtils = require("./handlebarsUtils");

const singleArray = ["Lao People's Democratic Republic, Laos"];

const multipleArray = ["ProMed", "News Media", "WHO", "CDC"];

const userLocations = [
  {
    locationType: 2,
    geonameId: 2984294,
    locationName: "Lao People's Democratic Republic, Laos",
    isLocal: true,
    outbreakPotentialCategoryId: 2,
    importationRisk: {
      isModelNotRun: true,
      minProbability: 0.05,
      maxProbability: 0.27
    }
  },
  {
    locationType: 2,
    geonameId: 2984294,
    locationName: "Lao People's Democratic Republic, Laos",
    isLocal: true,
    outbreakPotentialCategoryId: 2,
    importationRisk: {
      isModelNotRun: true,
      minProbability: 0.5,
      maxProbability: 0.7
    }
  },
  {
    locationType: 2,
    geonameId: 2984294,
    locationName: "Toronto, Ontario, Canada",
    isLocal: true,
    outbreakPotentialCategoryId: 2,
    importationRisk: {
      isModelNotRun: true,
      minProbability: 0.09,
      maxProbability: 0.1
    }
  }
];

describe("handlebarsUtils", () => {
  test("plurality of 1 number", () => {
    expect(handlebarsUtils.pluralize(1, "case", "cases")).toBe("case");
  });
  test("plurality for more than 1 number", () => {
    expect(handlebarsUtils.pluralize(23, "disease", "diseases")).toBe(
      "diseases"
    );
  });
  test("plurality of 1 array item", () => {
    expect(
      handlebarsUtils.pluralize(singleArray, "location", "locations")
    ).toBe("location");
  });
  test("plurality of more than 1 array item", () => {
    expect(handlebarsUtils.pluralize(multipleArray, "source", "sources")).toBe(
      "sources"
    );
  });
  test("format number as word", () => {
    expect(handlebarsUtils.formatNumberAsWord(2)).toBe("Two");
  });
  test("format number as digit", () => {
    expect(handlebarsUtils.formatNumberAsWord(34)).toBe(34);
  });
  test("format percent range", () => {
    expect(handlebarsUtils.formatPercentRange(0.13, 0.46)).toBe("13-46%");
  });
  test("return additional records", () => {
    expect(handlebarsUtils.numAdditionalRecords(43, userLocations)).toBe(40);
  });
  test("outbreak potential messaging", () => {
    expect(handlebarsUtils.outbreakPotentialMsg(3)).toBe(
      "Potential for sustained local transmission"
    );
  });
  test("return false for greater than", () => {
    expect(handlebarsUtils.ifGreaterThan(2, 3)).toBe(false);
  });
  test("return true for greater than", () => {
    expect(handlebarsUtils.ifGreaterThan(7, 3)).toBe(true);
  });
  test("get interval for 0 to 0", () => {
    expect(handlebarsUtils.getInterval(0, 0, "%", false)).toBe("Unlikely");
  });
  test("get interval for <1 to <1", () => {
    expect(handlebarsUtils.getInterval(0.0009237, 0.00232, "%", false)).toBe(
      "Unlikely"
    );
  });
  test("get interval for 0 to 1%", () => {
    expect(handlebarsUtils.getInterval(0, 0.01, "%", false)).toBe("<1% to 1%");
  });
  test("get interval for <1% to >1%", () => {
    expect(handlebarsUtils.getInterval(0.0009237, 0.246, "%", false)).toBe(
      "<1% to 25%"
    );
  });
  test("get interval for 1% to >1%", () => {
    expect(handlebarsUtils.getInterval(0.01352, 0.1232, "%", false)).toBe(
      "1% to 12%"
    );
  });
  test("get interval for 1 to ~1% rounded", () => {
    expect(handlebarsUtils.getInterval(0.01352, 0.01232, "%", false)).toBe(
      "~1%"
    );
  });
  test("get interval for approx value 1%", () => {
    expect(handlebarsUtils.getInterval(0.01, 0.01, "%", false)).toBe("~1%");
  });
  test("get interval for greater than 90%", () => {
    expect(handlebarsUtils.getInterval(0.95945672, 1, "%", false)).toBe(">90%");
  });
  test("get interval for aprox value >1%", () => {
    expect(handlebarsUtils.getInterval(0.3434, 0.3434, "%", false)).toBe(
      "~34%"
    );
  });
  test("get interval for percent range", () => {
    expect(
      handlebarsUtils.getInterval(0.35645653, 0.434354536, "%", false)
    ).toBe("36% to 43%");
  });
  test("get interval for not calculated", () => {
    expect(handlebarsUtils.getInterval(0.1336567, 0.434534546, "%", true)).toBe(
      "Not calculated"
    );
  });
  test("return municipal messaging", () => {
    expect(handlebarsUtils.locationTypeMsg(2)).toBe("municipal");
  });
  test("return province messaging", () => {
    expect(handlebarsUtils.locationTypeMsg(4)).toBe("provincial/state");
  });
  test("return country messaging", () => {
    expect(handlebarsUtils.locationTypeMsg(6)).toBe("national");
  });
  test("return unknown messaging", () => {
    expect(handlebarsUtils.locationTypeMsg(0)).toBe("unknown");
  });

  test("return formatted number as int with thousands separator", () => {
    expect(
      handlebarsUtils.formatNumber(123456, {
        hash: { decimalLength: "0" }
      })
    ).toBe("123,456");
  });
  test("return formatted number as decimal with thousands separator", () => {
    expect(
      handlebarsUtils.formatNumber(123456, {
        hash: {}
      })
    ).toBe("123,456.00");
  });
  test("return formatted number with thousands separator as space", () => {
    expect(
      handlebarsUtils.formatNumber(123456, {
        hash: { thousandsSep: " " }
      })
    ).toBe("123 456.00");
  });
  test("return formatted number as decimal with slashes as decimal separator", () => {
    expect(
      handlebarsUtils.formatNumber(123456, {
        hash: { decimalSep: "/" }
      })
    ).toBe("123,456/00");
  });
});
