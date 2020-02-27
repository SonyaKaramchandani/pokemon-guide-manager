import docCookies from 'utils/cookieHelpers';
import { CookieKeys } from 'utils/constants';

export const getPreferredMainPage = () =>
  docCookies.getItem(CookieKeys.PREF_MAIN_PAGE) || '/location';
