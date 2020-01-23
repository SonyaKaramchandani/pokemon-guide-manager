import axios from 'axios';
import { requestInterceptor, responseInterceptor, errorInterceptor } from './interceptors';
import docCookies from './../utils/cookieHelpers';

const axiosInstance = axios.create({
  withCredentials: true
});

export function init({ insightsApiBaseUrl }) {
  axiosInstance.defaults.baseURL = insightsApiBaseUrl;
}

export const CancelToken = axios.CancelToken;

axiosInstance.interceptors.response.use(responseInterceptor, errorInterceptor);

axiosInstance.interceptors.request.use(requestInterceptor);

export default axiosInstance;
