import axios from 'client';
import { AxiosResponse } from 'axios';
import * as dto from 'client/dto';

function getProfile(): Promise<AxiosResponse<dto.UserModel>> {
  return axios.get('/api/userprofile');
}

function getRoles(): Promise<AxiosResponse<dto.UserRoleModel[]>> {
  return axios.get('/api/role');
}

function updateProfile(
  personalDetailsModel: dto.UserPersonalDetailsModel
): Promise<AxiosResponse<dto.UserModel>> {
  return axios.put('/api/userprofile/details', personalDetailsModel, {
    headers: { 'X-Entity-Type': 'Personal details' }
  });
}

export default {
  getProfile,
  getRoles,
  updateProfile
};
