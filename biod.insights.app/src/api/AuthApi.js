import axios from 'axios';
import config from 'config';

const _axios = axios.create({
  withCredentials: true
});

function refreshToken() {
  return _axios.post(`${config.zebraAppBaseUrl}/Account/RefreshToken`);
}

function logOut() {
  return _axios.post(`${config.zebraAppBaseUrl}/Account/LogOff`);
}

export default {
  refreshToken,
  logOut
};
