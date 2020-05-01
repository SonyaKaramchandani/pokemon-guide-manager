import { parseISO, format, formatDistance } from 'date-fns';

export const formatDate = (d: string, dateFormat = 'MMM d, yyyy') => {
  if (!d) return null;
  return format(parseISO(d), dateFormat);
};

// EXAMPLE: May 1, 2018 - May 31, 2018
export const formatDateUntilToday = (d: string) => {
  if (!d) return null;
  return `${format(parseISO(d), 'MMM d, yyyy')} - ${format(new Date(), 'MMM d, yyyy')}`;
};

/**
 * @param {datetime string} d - datetime value is assumed to be UTC
 * @returns today, yesterday, X days ago, X months ago, etc.
 */
export const formatDuration = (d: string) => {
  if (!d) return null;

  // input: ignore time
  const inputDate = parseISO(d.substr(0, d.indexOf('T'))); // remove time

  // today: ignore time
  const dateNow = new Date(Date.now());
  const dateToday = new Date(dateNow.getUTCFullYear(), dateNow.getUTCMonth(), dateNow.getUTCDate());

  const duration = formatDistance(inputDate, dateToday, { addSuffix: true });

  // https://date-fns.org/v2.9.0/docs/formatDistance
  // approximate minutes and hours to today
  if (duration.includes('minute') || duration.includes('hour')) {
    return 'today';
  }

  return duration;
};

type RelativeDateFormatType = 'DurationAgo' | 'AbsoluteDate' | 'AbsoluteAbbrevDate';
export const formatRelativeDate = (d: string, formatType: RelativeDateFormatType) => {
  return formatType === 'DurationAgo'
    ? formatDuration(d)
    : formatType === 'AbsoluteAbbrevDate'
    ? formatDate(d, 'MMM d, yyyy')
    : formatDate(d, 'MMMM d, yyyy');
};
