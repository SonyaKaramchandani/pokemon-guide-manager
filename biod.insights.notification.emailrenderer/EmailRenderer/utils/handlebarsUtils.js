const Handlebars = require("handlebars");
const { analyticsHtml, gaURIComponent } = require("../analytics");

const singleDigitNumbers = {
  1: "One",
  2: "Two",
  3: "Three",
  4: "Four",
  5: "Five",
  6: "Six",
  7: "Seven",
  8: "Eight",
  9: "Nine"
};

const OutbreakPotentialCategory = {
  Sustained: 1,
  Sporadic: 2,
  NeedsMapSustained: 3,
  NeedsMapUnlikely: 4,
  Unlikely: 5,
  Unknown: 6
};
/**
 *
 * @param {Array | Number} number
 * An array of items or number representing the quantity to evaluate
 * @param {String} single
 * Singular form of string
 * @param {String} plural
 * Plural form of string
 */
function pluralize(number, single, plural) {
  if (number === 1 || number.length === 1) {
    return single;
  } else {
    return plural;
  }
}

/**
 *
 * @param {Number} number
 * A number to be represented as either a word or digit
 */
function formatNumber(number) {
  if (singleDigitNumbers.hasOwnProperty(number)) {
    return singleDigitNumbers[number];
  } else {
    return number;
  }
}

/**
 * Formats the min to max values to an interval display text
 * @param {Number} number A number to be represented as either a word or digit
 * @return {String} Formatted interval string representation
 */
function formatPercentRange(minValue, maxValue) {
  return (minValue * 100).toString() + "-" + (maxValue * 100).toString() + "%";
}

/**
 * Returns the additional records that exist given the total and the returned records
 * @param {Number} totalRecords
 * Number of total records that exist
 * @param {Array} records
 * Number of records returned
 * @return {Number} Number of additional records that exist
 */
function numAdditionalRecords(totalRecords, records) {
  return totalRecords - records.length;
}

/**
 * Returns the messaging for the outbreak potentials
 * @param {Number} outbreakPotentialCategoryId
 * Outbreak potential Id
 * @return {String} Outbreak potential messaging
 */

const outbreakPotentialMsg = outbreakPotentialCategoryId =>
  outbreakPotentialCategoryId === OutbreakPotentialCategory.Sustained ||
  outbreakPotentialCategoryId === OutbreakPotentialCategory.NeedsMapSustained
    ? "Potential for sustained local transmission"
    : outbreakPotentialCategoryId === OutbreakPotentialCategory.Sporadic
    ? "Potential for sporadic local transmission"
    : outbreakPotentialCategoryId === OutbreakPotentialCategory.Unlikely ||
      outbreakPotentialCategoryId === OutbreakPotentialCategory.NeedsMapUnlikely
    ? "Potential for local transmission unlikely"
    : outbreakPotentialCategoryId === OutbreakPotentialCategory.Unknown
    ? "Unknown potential for local transmission"
    : "";

/**
 * Formats the min to max values to the traveller interval display text
 * @param {Number} minval The min importation risk expressed as a decimal
 * @param {Number} maxval The max importation risk expressed as a decimal
 * @param {Boolean} isModelNotRun Whether the model is not run
 * * @return {String} Formatted interval string representation
 */

function formatImportationRisk(minVal, maxVal, isModelNotRun) {
  if (isModelNotRun || minVal > maxVal) {
    return "Not calculated";
  }
  if (maxVal <= 0) {
    return "Negligible";
  } else {
    return (minVal * 100).toString() + "-" + (maxVal * 100).toString() + "%";
  }
}

/**
 * Evaluates if a value is greater than another value
 * @param {Number} arg1 First argument to evaluate
 * @param {Number} arg2 Second argument to evaluate
 * * @return {Boolean} Whether arg1 is greater than arg2
 */
function ifGreaterThan(arg1, arg2) {
  return !!(arg1 > arg2);
}

Handlebars.registerHelper(
  "gaURIComponent",
  (...params) => new Handlebars.SafeString(gaURIComponent(...params))
);

Handlebars.registerHelper("pluralize", pluralize);
Handlebars.registerHelper("formatNumber", formatNumber);
Handlebars.registerHelper("formatPercentRange", formatPercentRange);
Handlebars.registerHelper("numAdditionalRecords", numAdditionalRecords);
Handlebars.registerHelper("outbreakPotentialMsg", outbreakPotentialMsg);
Handlebars.registerHelper("formatImportationRisk", formatImportationRisk);
Handlebars.registerHelper("ifGreaterThan", ifGreaterThan);

module.exports = {
  analyticsHtml,
  pluralize,
  formatNumber,
  formatPercentRange,
  numAdditionalRecords,
  outbreakPotentialMsg,
  formatImportationRisk,
  ifGreaterThan
};
