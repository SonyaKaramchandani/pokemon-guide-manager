const { pluralize, formatNumber } = require("./handlebarsUtils");

const singleArray = ["Lao People's Democratic Republic, Laos"];

const multipleArray = ["ProMed", "News Media", "WHO", "CDC"];

describe("handlebarsUtils", () => {
  test("plurality of 1 number", () => {
    expect(pluralize(1, "case", "cases")).toBe("case");
  });
  test("plurality for more than 1 number", () => {
    expect(pluralize(23, "disease", "diseases")).toBe("diseases");
  });
  test("plurality of 1 array item", () => {
    expect(pluralize(singleArray, "location", "locations")).toBe("location");
  });
  test("plurality of more than 1 array item", () => {
    expect(pluralize(multipleArray, "source", "sources")).toBe("sources");
  });
  test("format number as word", () => {
    expect(formatNumber(2)).toBe("Two");
  });
  test("format number as digit", () => {
    expect(formatNumber(34)).toBe(34);
  });
});
