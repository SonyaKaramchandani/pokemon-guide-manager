import docCookies from 'utils/cookieHelpers';
import { CookieKeys } from 'utils/constants';

export const isUserAdmin = userProfile =>
  userProfile && userProfile.roles.some(r => r.name === 'Admin');

export const isLoggedIn = () => docCookies.hasItem(CookieKeys.JWT);
