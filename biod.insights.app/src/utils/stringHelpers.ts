export const containsNoCase = (str: string, test: string): boolean => {
  if (!str) return false;
  if (!test) return true;
  return str.toLowerCase().includes(test.toLowerCase());
};
export const containsNoCaseNoLocale = (str: string, test: string): boolean => {
  if (!str) return false;
  if (!test) return true;
  return str.toLocaleLowerCase().includes(test.toLocaleLowerCase());
};

export const parseIntOrNull = (s: string): number => {
  if (s === null || s === undefined) return null;
  const val = Number(s);
  return Number.isNaN(val) ? null : val;
};
