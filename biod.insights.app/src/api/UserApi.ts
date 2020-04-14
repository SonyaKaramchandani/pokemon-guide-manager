import axios from 'client';
import { AxiosResponse } from 'axios';
import * as dto from 'client/dto';

function getProfile(): Promise<AxiosResponse<dto.UserModel>> {
  return axios.get('/api/userprofile');
}

export default {
  getProfile
};
