import * as dto from 'client/dto';
import { format } from 'date-fns';

export const formatNumber = (num: number, label?: string, labelPlural?: string): string => {
  labelPlural = labelPlural || label + 's';
  const labelText = !label ? null : num == 1 ? label : labelPlural || label + 's';
  return num != null || num != undefined
    ? `${num.toLocaleString()}${labelText ? ' ' + labelText : ''}`
    : '—';
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
  const strOthers =
    total > 1 ? ` and ${formatNumber(total - 1, 'other airport', 'other airports')}` : '';
  return `${airports[0].name} (${airports[0].code}) ${strOthers}`;
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
  const month = format(new Date(), 'MMMM');
  return meta
    ? `IATA (${month} ${meta.iataDatasetYear}) and Innovata (${month} ${meta.innovataDatasetYear})`
    : 'IATA and Innovata';
};

export const formatLandscan = (meta: dto.ApplicationMetadataModel) => {
  return meta ? `Landscan (${meta.landscanDatasetYear})` : 'Landscan';
};
