import * as dto from 'client/dto';
import { format } from 'date-fns';

export const formatNumber = (num: number, label?: string, labelPlural?: string): string => {
  labelPlural = labelPlural || label + 's';
  const labelText = !label ? null : num == 1 ? label : labelPlural || label + 's';
  return num != null || num != undefined
    ? `${num.toLocaleString()}${labelText ? ' ' + labelText : ''}`
    : '-';
};

export const formatShortNumberRange = (
  min: number,
  max: number,
  label?: string,
  labelPlural?: string
): string => {
  labelPlural = labelPlural || label + 's';
  if (min === 0 && max === 0) return formatNumber(0, label, labelPlural);
  if (min === max) return formatNumber(min, label, labelPlural);
  return `${formatNumber(min)}-${formatNumber(max)}${label ? ' ' + labelPlural : ''}`;
};

export const formatPercent = (value: number, outof: number): string => {
  if (outof === 0) return '0%';
  const percent = (100 * value) / outof;
  return percent < 1 ? '<1%' : `${formatNumber(Math.round(percent))}%`;
};

export const formatRatio1inX = (percent: number): string => {
  if (percent === 0) return '0';
  return `1 in ${formatNumber(Math.round(1 / percent))}`;
};

export const getTopAirportShortNameList = (airports: dto.GetAirportModel[], total: number) => {
  if (!airports || !airports.length || !airports[0]) return 'No matching airports';
  const strOthers = total > 1 ? ` and ${formatNumber(total - 1, 'other')}` : '';
  return `${airports[0].name} (${airports[0].code}) ${strOthers}`;
};

export const getTravellerInterval = (
  minVal: number,
  maxVal: number,
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

export const getLocationFullName = (geoname: dto.GetGeonameModel): string => {
  const { name, province, country, locationType } = geoname;
  return locationType === dto.LocationType.Country
    ? name
    : locationType === dto.LocationType.Province
    ? [province || name, country].filter(i => !!i).join(', ')
    : locationType === dto.LocationType.City
    ? [name, province, country].filter(i => !!i).join(', ')
    : name;
};

export const formatIATA = (meta: dto.ApplicationMetadataModel) => {
  return meta ? `IATA (${format(new Date(), 'MMMM')} ${meta.iataDatasetYear})` : 'IATA';
};

export const formatLandscan = (meta: dto.ApplicationMetadataModel) => {
  return meta ? `Landscan (${meta.landscanDatasetYear})` : 'Landscan';
};
