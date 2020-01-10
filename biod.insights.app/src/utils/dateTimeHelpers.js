import { parseISO, format, formatDistanceToNow } from 'date-fns';

export const formatDate = d => {
  return format(parseISO(d), 'MMM d, yyyy');
};

export const formatDuration = d => {
  if (!d) return null;

  return formatDistanceToNow(parseISO(d), { addSuffix: true });
};
