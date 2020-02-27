import * as dto from 'client/dto';

export const formatNumber = (num: number, label?: string, labelPlural?: string): string => {
  labelPlural = labelPlural || label + 's';
  const labelText = !label ? null : num == 1 ? label : labelPlural || label + 's';
  return num != null || num != undefined
    ? `${num.toLocaleString()}${labelText ? ' ' + labelText : ''}`
    : '-';
};

export const getTravellerInterval = (
  minVal,
  maxVal,
  includeUnit = false,
  isModelNotRun = false
) => {
  if (isModelNotRun) {
    return 'Not calculated';
  }

  if (minVal > maxVal) {
    return 'Not calculated';
  }

  if (maxVal <= 0) {
    return 'Negligible';
  }

  // Calculated rounded values
  const roundedMin = Math.round(minVal);
  const roundedMax = Math.round(maxVal);

  let unit = '';
  if (includeUnit) {
    unit = ' case' + (roundedMax > 1 ? 's' : '');
  }

  if (minVal < 1) {
    if (maxVal < 1) {
      return `<1${unit}`;
    }
    return `<1 to ${formatNumber(roundedMax)}${unit}`;
  }

  if (minVal === maxVal || roundedMin === roundedMax) {
    return `~${formatNumber(roundedMin)}${unit}`;
  }

  return `${formatNumber(roundedMin)} to ${formatNumber(roundedMax)}${unit}`;
};

/**
 * Formats the min to max values to an interval display text
 * @param {number} minVal - min interval value
 * @param {number} maxVal - max interval value
 * @param {string} unit - format of the interval (e.g. "%")
 * @return {string} Formatted interval string representation
 */
export const getInterval = (
  minVal: number,
  maxVal: number,
  unit = '',
  isModelNotRun = false
): string => {
  let retVal;
  let prefixLow = '';
  let prefixUp = '';

  if (isModelNotRun) {
    return 'Not calculated';
  }

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
    if (maxVal <= 0) {
      return 'Unlikely';
    }
  }

  prefixLow = prefixLow.length > 0 ? prefixLow : minVal < 1 ? '<' : '';
  const roundMin = minVal >= 1 ? Math.round(minVal) : 1;
  const roundMax = maxVal >= 1 ? Math.round(maxVal) : 1;

  if (roundMin === roundMax && prefixLow !== '<') {
    prefixLow = prefixLow.length > 0 ? prefixLow : '~';
    retVal = prefixLow + roundMin + unit;
  } else {
    retVal = prefixLow + roundMin + unit + ' to ' + prefixUp + roundMax + unit;
  }

  return retVal;
};

export const getRiskLevel = (maxProb: number): number => {
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

export const getProbabilityName = (maxProb: number): string => {
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

export const locationTypePrint = (locationType: dto.LocationType): string => {
  switch (locationType) {
    case dto.LocationType.City:
      return 'City/Township';
    case dto.LocationType.Province:
      return 'Province/State';
    case dto.LocationType.Country:
      return 'Country';
    default:
      return 'Unknown';
  }
};
