const Handlebars = require("handlebars");

var singleDigitNumbers = { 1: "One", 2: "Two", 3: "Three", 4: "Four", 5: "Five", 6: "Six", 7: "Seven", 8: "Eight", 9: "Nine" };

Handlebars.registerHelper('pluralize', function(number, single, plural) {
  if (number === 1) { return single; }
  else { return plural; }
});

Handlebars.registerHelper('formatNumber', function(key) {
  if (singleDigitNumbers.hasOwnProperty(key)) { return singleDigitNumbers[key]; }
  else { return key; }
});

Handlebars.registerHelper('divide100', function(value){
	return value/100;
});