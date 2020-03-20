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
 * Formats the min to max values to an interval display text
 * @param {number} minVal - min interval value
 * @param {number} maxVal - max interval value
 * @param {string} unit - format of the interval (e.g. "%")
 * @return {string} Formatted interval string representation
 */

function getInterval(minVal, maxVal, unit, isModelNotRun) {
  let retVal;
  let prefixLow = "";
  let prefixUp = "";

  if (isModelNotRun) {
    return "Not calculated";
  }

  if (unit === "%") {
    minVal *= 100;
    maxVal *= 100;
    if (minVal > 90) {
      minVal = 90;
      prefixLow = ">";
    }
    if (maxVal > 90) {
      maxVal = 90;
      prefixUp = ">";
    }
    if (maxVal < 1) {
      return "Unlikely";
    }
  }

  prefixLow = prefixLow.length > 0 ? prefixLow : minVal < 1 ? "<" : "";
  const roundMin = minVal >= 1 ? Math.round(minVal) : 1;
  const roundMax = maxVal >= 1 ? Math.round(maxVal) : 1;

  if (roundMin === roundMax && prefixLow !== "<") {
    prefixLow = prefixLow.length > 0 ? prefixLow : "~";
    retVal = prefixLow + roundMin + unit;
  } else {
    retVal = prefixLow + roundMin + unit + " to " + prefixUp + roundMax + unit;
  }

  return retVal;
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
  if (array.some(e => e.caseCountChange.hasReportedCasesNesting === true)) {
    return true;
  } else {
    return false;
  }
}

function ifCity(v1, options) {
  if (v1 === LocationType.City) {
    return options.fn(this);
  }
  return options.inverse(this);
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
Handlebars.registerHelper("formatNumber", formatNumber);
Handlebars.registerHelper("formatPercentRange", formatPercentRange);
Handlebars.registerHelper("numAdditionalRecords", numAdditionalRecords);
Handlebars.registerHelper("outbreakPotentialMsg", outbreakPotentialMsg);
Handlebars.registerHelper("ifGreaterThan", ifGreaterThan);
Handlebars.registerHelper("getInterval", getInterval);
Handlebars.registerHelper("locationTypeMsg", locationTypeMsg);
Handlebars.registerHelper("loadMjmlSubcomponent", loadMjmlSubcomponent);
Handlebars.registerHelper("checkIfNesting", checkIfNesting);
Handlebars.registerHelper("ifCity", ifCity);
module.exports = {
  analyticsHtml,
  pluralize,
  formatNumber,
  formatPercentRange,
  numAdditionalRecords,
  outbreakPotentialMsg,
  ifGreaterThan,
  getInterval,
  locationTypeMsg,
  loadMjmlSubcomponent,
  checkIfNesting,
  ifCity
};
