import { parseISO, format, formatDistance } from 'date-fns';

export const formatDate = d => {
  if (!d) return null;
  return format(parseISO(d), 'MMM d, yyyy');
};

/**
 *
 * @param {datetime string} d - datetime value is assumed to be UTC
 */
export const formatDuration = d => {
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
