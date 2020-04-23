const fs = require("fs");
const Handlebars = require("handlebars");
const constants = require("../constants.json");
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

const LocationType = {
  Unknown: 0,
  City: 2,
  Province: 4,
  Country: 6
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
function formatNumberAsWord(number) {
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
 * Formats the min to max values to a category display text using the average
 * @param {number} minVal - min interval value
 * @param {number} maxVal - max interval value
 * @param {boolean} isModelNotRun - whether the model was run
 * @return {string} Formatted category string representation
 */
function getInterval(minVal, maxVal, isModelNotRun) {
  const avg = (minVal + maxVal) / 2;
  return isModelNotRun
    ? 'Not calculated'
    : avg < 0.01
    ? 'Unlikely'
    : avg <= 0.1
    ? 'Low'
    : avg <= 0.5
    ? 'Moderate'
    : avg <= 0.9
    ? 'High'
    : avg <= 1
    ? 'Very high'
    : 'Not calculated';
}

/**
 * Formats the min to max values to an interval display text using the average
 * @param {number} minVal - min interval value
 * @param {number} maxVal - max interval value
 * @param {boolean} isModelNotRun - whether the model was run
 * @return {string} Formatted interval string representation
 */
function getIntervalRange(minVal, maxVal, isModelNotRun) {
  const avg = (minVal + maxVal) / 2;
  return isModelNotRun
    ? ''
    : avg < 0.01
    ? '<1%'
    : avg <= 0.1
    ? '1% to 10%'
    : avg <= 0.5
    ? '11% to 50%'
    : avg <= 0.9
    ? '51% to 90%'
    : avg <= 1
    ? '91% to 100%'
    : '';
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

/**
 * Returns the location type messaging based on the locationTypeId
 * @param {Number} locationTypeId Id of the LocationType
 * * @return {String} Location type string
 */
function locationTypeMsg(locationTypeId) {
  switch (locationTypeId) {
    case LocationType.City:
      return "municipal";
    case LocationType.Province:
      return "provincial/state";
    case LocationType.Country:
      return "national";
    default:
      return "unknown";
  }
}

/**
 * Returns the HTML-rendered MJML Subcomponent through handlebars
 * @param {String} templateName Id of the LocationType
 * @param {Object} data the data to inject
 * * @return {String} rendered subcomponent template
 */
function loadMjmlSubcomponent(templateName, data = {}) {
  const mjmlTemplatePath = `${__dirname}/../${constants.emailFolder}/${templateName}`;
  const emailContent = fs.readFileSync(mjmlTemplatePath).toString();
  if (
    emailContent.match(new RegExp(`loadMjmlSubcomponent "${templateName}"`))
  ) {
    throw `The sub-component template '${templateName}' cannot reference itself!`;
  }
  const template = Handlebars.compile(emailContent);
  return template({
    ...data
  });
}
function checkIfNesting(array) {
  return array.some(e => e.caseCountChange.hasReportedCasesNesting === true);
}

function ifAnyNotCity(array) {
  return array.some(e => e.locationType !== LocationType.City);
}

function ifCity(v1, options) {
  if (v1 === LocationType.City) {
    return options.fn(this);
  }
  return options.inverse(this);
}
/**
 * An Handlebars helper to format numbers
 *  @param {Integer} value Number to format
 *  @param {Boolean} asWord Whether to represent the number as a word
 *  @param {Integer} decimalLength The length of the decimals
 *  @param {String} thousandsSep The thousands separator
 *  @param {String} decimalSep The decimals separator
 *  @return {String} formatted numeric representation of input
 * From: https://gist.github.com/DennyLoko/61882bc72176ca74a0f2
 * Based on:
 * mu is too short: http://stackoverflow.com/a/14493552/369867
 * VisioN: http://stackoverflow.com/a/14428340/369867
 * Demo: http://jsfiddle.net/DennyLoko/6sR87/
 */
function formatNumber(value, options) {
  // Helper parameters
  var dl = options.hash["decimalLength"] || 2;
  var ts = options.hash["thousandsSep"] || ",";
  var ds = options.hash["decimalSep"] || ".";

  // Parse to float
  var value = parseFloat(value);

  // The regex
  var re = "\\d(?=(\\d{3})+" + (dl > 0 ? "\\D" : "$") + ")";

  // Formats the number with the decimals
  const num = value.toFixed(Math.max(0, ~~dl));

  // Returns the formatted number
  return (ds ? num.replace(".", ds) : num).replace(
    new RegExp(re, "g"),
    "$&" + ts
  );
}

Handlebars.registerHelper(
  "gaURIComponent",
  rootObject =>
    new Handlebars.SafeString(
      gaURIComponent(
        rootObject.emailType,
        rootObject.config,
        rootObject.userId,
        rootObject.eventId,
        rootObject.isDoNotTrackEnabled
      )
    )
);

Handlebars.registerHelper("pluralize", pluralize);
Handlebars.registerHelper("formatNumberAsWord", formatNumberAsWord);
Handlebars.registerHelper("formatPercentRange", formatPercentRange);
Handlebars.registerHelper("numAdditionalRecords", numAdditionalRecords);
Handlebars.registerHelper("outbreakPotentialMsg", outbreakPotentialMsg);
Handlebars.registerHelper("ifGreaterThan", ifGreaterThan);
Handlebars.registerHelper("getInterval", getInterval);
Handlebars.registerHelper("getIntervalRange", getIntervalRange);
Handlebars.registerHelper("locationTypeMsg", locationTypeMsg);
Handlebars.registerHelper("loadMjmlSubcomponent", loadMjmlSubcomponent);
Handlebars.registerHelper("checkIfNesting", checkIfNesting);
Handlebars.registerHelper("ifAnyNotCity", ifAnyNotCity);
Handlebars.registerHelper("ifCity", ifCity);
Handlebars.registerHelper("formatNumber", formatNumber);

module.exports = {
  analyticsHtml,
  pluralize,
  formatNumberAsWord,
  formatPercentRange,
  numAdditionalRecords,
  outbreakPotentialMsg,
  ifGreaterThan,
  getInterval,
  getIntervalRange,
  locationTypeMsg,
  loadMjmlSubcomponent,
  checkIfNesting,
  ifAnyNotCity,
  ifCity,
  formatNumber
};
