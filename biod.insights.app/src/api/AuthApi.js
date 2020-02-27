import axios from 'axios';
import config from 'config';
import docCookies from '../utils/cookieHelpers';

const _axios = axios.create({
  withCredentials: true
});

function refreshToken() {
  return _axios.post(`${config.zebraAppBaseUrl}/Account/RefreshToken`).then(({ data }) => {
    docCookies.setItem('_jid', data.access_token, data.expires_in);
    return data.access_token;
  });
}

function logOut() {
  return _axios.post(`${config.zebraAppBaseUrl}/Account/LogOff`);
}

export default {
  refreshToken,
  logOut
};
