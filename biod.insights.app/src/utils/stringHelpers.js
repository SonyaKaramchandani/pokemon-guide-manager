export const containsNoCase = function (str, test) {
  if (!test)
    return true;
  return str.toLowerCase().includes(test.toLowerCase());
};
export const containsNoCaseNoLocale = function (str, test) {
  if (!test)
    return true;
  return str.toLocaleLowerCase().includes(test.toLocaleLowerCase());
};
