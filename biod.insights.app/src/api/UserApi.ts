import axios from 'client';
import { AxiosResponse } from 'axios';
import * as dto from 'client/dto';

function getProfile(): Promise<AxiosResponse<dto.UserModel>> {
  return axios.get('/api/userprofile');
}

function getRoles(): Promise<AxiosResponse<dto.UserTypeModel[]>> {
  return axios.get('/api/usertype');
}

function updateProfile(
  personalDetailsModel: dto.UserPersonalDetailsModel
): Promise<AxiosResponse<dto.UserModel>> {
  return axios.put('/api/userprofile/details', personalDetailsModel, {
    headers: { 'X-Entity-Type': 'Personal details' }
  });
}

function updateNotificationSettings(
  model: dto.UserNotificationsModel
): Promise<AxiosResponse<dto.UserModel>> {
  return axios.put('/api/userprofile/notifications', model, {
    headers: { 'X-Entity-Type': 'Notification Settings' }
  });
}

function updateCustomSettings(
  model: dto.UserCustomSettingsModel
): Promise<AxiosResponse<dto.UserModel>> {
  return axios.put('/api/userprofile/customsettings', model, {
    headers: { 'X-Entity-Type': 'Custom Settings' }
  });
}

export default {
  getProfile,
  getRoles,
  updateProfile,
  updateNotificationSettings,
  updateCustomSettings
};
