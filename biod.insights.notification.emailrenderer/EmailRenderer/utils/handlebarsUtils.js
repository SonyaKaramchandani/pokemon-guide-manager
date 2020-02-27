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

Handlebars.registerHelper(
  "gaURIComponent",
  (...params) => new Handlebars.SafeString(gaURIComponent(...params))
);

Handlebars.registerHelper("pluralize", pluralize);
Handlebars.registerHelper("formatNumber", formatNumber);

module.exports = {
  analyticsHtml,
  pluralize,
  formatNumber
};
