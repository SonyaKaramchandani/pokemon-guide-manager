import axios from 'axios';

const axiosInstance = axios.create();

export function init({ insightsApiBaseUrl }) {
  axiosInstance.defaults.baseURL = insightsApiBaseUrl;
}

export default axiosInstance;
