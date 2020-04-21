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

function testGetInterval(min, max, notCalc, expected) {
  const avg = (min + max) / 2;
  const notCalcStr = notCalc ? '(NOT CALCULATED) ' : '';
  test(`${notCalcStr}get interval category for ${min} to ${max} (avg=${avg})`, () => {
    expect(handlebarsUtils.getInterval(min, max, notCalc)).toBe(expected);
  });
}

function testGetIntervalRange(min, max, notCalc, expected) {
  const avg = (min + max) / 2;
  const notCalcStr = notCalc ? '(NOT CALCULATED) ' : '';
  test(`${notCalcStr}get interval range for ${min} to ${max} (avg=${avg})`, () => {
    expect(handlebarsUtils.getIntervalRange(min, max, notCalc)).toBe(expected);
  });
}

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
  testGetInterval(0, 0, false, 'Unlikely');
  testGetInterval(0.005, 0.008, false, 'Unlikely');
  testGetInterval(0.01, 0.01, false, 'Low');
  testGetInterval(0.05, 0.1, false, 'Low');
  testGetInterval(0.005, 0.05, false, 'Low');
  testGetInterval(0.1, 0.1, false, 'Low');
  testGetInterval(0.1, 0.1005, false, 'Moderate');
  testGetInterval(0.1, 0.2, false, 'Moderate');
  testGetInterval(0.2, 0.3, false, 'Moderate');
  testGetInterval(0.3, 0.4, false, 'Moderate');
  testGetInterval(0.4, 0.5, false, 'Moderate');
  testGetInterval(0.5, 0.5, false, 'Moderate');
  testGetInterval(0.5, 0.6, false, 'High');
  testGetInterval(0.6, 0.7, false, 'High');
  testGetInterval(0.7, 0.8, false, 'High');
  testGetInterval(0.8, 0.9, false, 'High');
  testGetInterval(0.9, 0.9, false, 'High');
  testGetInterval(0.9, 0.95, false, 'Very High');
  testGetInterval(0.95, 0.99, false, 'Very High');
  testGetInterval(0.99, 1, false, 'Very High');
  testGetInterval(1, 1, false, 'Very High');
  testGetInterval(1, 1, true, 'Not calculated');
  testGetInterval(0.5, 0.5, true, 'Not calculated');
  testGetInterval(0.01, 0.8, true, 'Not calculated');
  testGetInterval(0.01, 0.08, true, 'Not calculated');
  testGetInterval(0, 0, true, 'Not calculated');
  testGetInterval(1, 2, false, 'Not calculated');
  testGetInterval(100, 200, false, 'Not calculated');
  testGetIntervalRange(0, 0, false, '<1%');
  testGetIntervalRange(0.005, 0.008, false, '<1%');
  testGetIntervalRange(0.01, 0.01, false, '1% to 10%');
  testGetIntervalRange(0.05, 0.1, false, '1% to 10%');
  testGetIntervalRange(0.005, 0.05, false, '1% to 10%');
  testGetIntervalRange(0.1, 0.1, false, '1% to 10%');
  testGetIntervalRange(0.1, 0.1005, false, '11% to 50%');
  testGetIntervalRange(0.1, 0.2, false, '11% to 50%');
  testGetIntervalRange(0.2, 0.3, false, '11% to 50%');
  testGetIntervalRange(0.3, 0.4, false, '11% to 50%');
  testGetIntervalRange(0.4, 0.5, false, '11% to 50%');
  testGetIntervalRange(0.5, 0.5, false, '11% to 50%');
  testGetIntervalRange(0.5, 0.6, false, '51% to 90%');
  testGetIntervalRange(0.6, 0.7, false, '51% to 90%');
  testGetIntervalRange(0.7, 0.8, false, '51% to 90%');
  testGetIntervalRange(0.8, 0.9, false, '51% to 90%');
  testGetIntervalRange(0.9, 0.9, false, '51% to 90%');
  testGetIntervalRange(0.9, 0.95, false, '91% to 100%');
  testGetIntervalRange(0.95, 0.99, false, '91% to 100%');
  testGetIntervalRange(0.99, 1, false, '91% to 100%');
  testGetIntervalRange(1, 1, false, '91% to 100%');
  testGetIntervalRange(1, 1, true, '');
  testGetIntervalRange(0.5, 0.5, true, '');
  testGetIntervalRange(0.01, 0.8, true, '');
  testGetIntervalRange(0.01, 0.08, true, '');
  testGetIntervalRange(0, 0, true, '');
  testGetIntervalRange(1, 2, false, '');
  testGetIntervalRange(100, 200, false, '');
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
