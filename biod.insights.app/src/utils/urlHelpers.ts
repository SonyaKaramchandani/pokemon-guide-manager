import config from 'config';

export const toAbsoluteZebraUrl = url => {
  return `${config.zebraAppBaseUrl}${url}`;
};
