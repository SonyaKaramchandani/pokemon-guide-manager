import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: process.env.REACT_APP_INSIGHTS_API_BASEURL
});

export default axiosInstance;
