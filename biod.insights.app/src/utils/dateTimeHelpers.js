import { parseISO, format } from 'date-fns';

export const formatDate = d => {
  return format(parseISO(d), 'MMM. d, yyyy');
};
