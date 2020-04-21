import axios from 'client';
import { AxiosResponse } from 'axios';
import { LogLevel } from '../utils/constants';

function sendLog(logLevel: LogLevel, message: string): Promise<AxiosResponse> {
  return axios.post(`/api/log/js`, {
    logLevel: logLevel,
    message: message
  });
}

export default {
  sendLog
};
