export const containsNoCase = (str: string, test: string): boolean => {
  if (!test) return true;
  return str.toLowerCase().includes(test.toLowerCase());
};
export const containsNoCaseNoLocale = (str: string, test: string): boolean => {
  if (!test) return true;
  return str.toLocaleLowerCase().includes(test.toLocaleLowerCase());
};
