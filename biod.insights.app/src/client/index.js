import axios from 'axios';
import { responseInterceptor, errorInterceptor } from './interceptors';

const axiosInstance = axios.create();

export function init({ insightsApiBaseUrl }) {
  axiosInstance.defaults.baseURL = insightsApiBaseUrl;
}

export const CancelToken = axios.CancelToken;

axiosInstance.interceptors.response.use(responseInterceptor, errorInterceptor);

export default axiosInstance;
