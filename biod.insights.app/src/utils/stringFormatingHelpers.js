export const getTravellerInterval = (minVal, maxVal, includeUnit = false) => {
  if (minVal > maxVal) {
    return '-';
  }

  if (maxVal <= 0) {
    return 'Negligible';
  }

  // Calculated rounded values
  const roundedMin = Math.round(minVal);
  const roundedMax = Math.round(maxVal);

  let unit = '';
  if (includeUnit) {
    unit = ' Traveller' + (roundedMax > 1 ? 's' : '');
  }

  if (minVal < 1) {
    if (maxVal < 1) {
      return `< 1${unit}`;
    }
    return `< 1 to ${roundedMax}${unit}`;
  }

  if (minVal === maxVal || roundedMin === roundedMax) {
    return `~ ${roundedMin}${unit}`;
  }

  return `${roundedMin} to ${roundedMax}${unit}`;
};

/**
 * Formats the min to max values to an interval display text
 * @param {number} minVal - min interval value
 * @param {number} maxVal - max interval value
 * @param {string} unit - format of the interval (e.g. "%")
 * @return {string} Formatted interval string representation
 */
export const getInterval = (minVal, maxVal, unit = '') => {
  let retVal;
  let prefixLow = '';
  let prefixUp = '';

  if (unit === '%') {
    minVal *= 100;
    maxVal *= 100;
    if (minVal > 90) {
      minVal = 90;
      prefixLow = '>';
    }
    if (maxVal > 90) {
      maxVal = 90;
      prefixUp = '>';
    }
  }

  prefixLow = prefixLow.Length > 0 ? prefixLow : minVal < 1 ? '<' : '';
  var roundMin = minVal >= 1 ? Math.round(minVal) : 1;
  var roundMax = maxVal >= 1 ? Math.round(maxVal) : 1;

  if (roundMin === roundMax && prefixLow !== '<') {
    prefixLow = prefixLow.Length > 0 ? prefixLow : '~';
    retVal = prefixLow + roundMin + unit;
  } else {
    retVal = prefixLow + roundMin + unit + ' to ' + prefixUp + roundMax + unit;
  }

  return retVal;
};

export const getRiskLevel = maxProb => {
  if (maxProb !== null && maxProb >= 0) {
    if (maxProb < 0.01 && maxProb >= 0) {
      return 0;
    }
    if (maxProb < 0.2) {
      return 1;
    }
    if (maxProb >= 0.2 && maxProb <= 0.7) {
      return 2;
    }
    if (maxProb > 0.7) {
      return 3;
    }
  }
  return -1;
};

export const getProbabilityName = maxProb => {
  switch (getRiskLevel(maxProb)) {
    case 0:
      return 'None';
    case 1:
      return 'Low';
    case 2:
      return 'Medium';
    case 3:
      return 'High';
    default:
      return 'NotAvailable';
  }
};
