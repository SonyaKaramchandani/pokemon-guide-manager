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
    expect(handlebarsUtils.formatNumber(2)).toBe("Two");
  });
  test("format number as digit", () => {
    expect(handlebarsUtils.formatNumber(34)).toBe(34);
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
  test("return formatted percent range", () => {
    expect(handlebarsUtils.formatImportationRisk(0.13, 0.46, false)).toBe(
      "13-46%"
    );
  });
  test("return not calculated messaging", () => {
    expect(handlebarsUtils.formatImportationRisk(0.13, 0.46, true)).toBe(
      "Not calculated"
    );
  });
  test("return negligible messaging", () => {
    expect(handlebarsUtils.formatImportationRisk(0, 0, false)).toBe(
      "Negligible"
    );
  });
});
