import axios from 'axios';
import { responseInterceptor, errorInterceptor } from './interceptors';
import docCookies from './../utils/cookieHelpers';

const axiosInstance = axios.create({
  withCredentials: true
});

export function init({ insightsApiBaseUrl }) {
  axiosInstance.defaults.baseURL = insightsApiBaseUrl;
  axiosInstance.defaults.headers.common = {
    Authorization: `Bearer ${docCookies.getItem('_jid') || ''}`
  };
}

export const CancelToken = axios.CancelToken;

axiosInstance.interceptors.response.use(responseInterceptor, errorInterceptor);

export default axiosInstance;
