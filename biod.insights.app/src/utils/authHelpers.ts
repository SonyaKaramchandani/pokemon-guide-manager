import AuthApi from '../api/AuthApi';
import * as dto from 'client/dto';

export const isUserAdmin = (userProfile: dto.UserModel) =>
  userProfile && userProfile.roles && userProfile.roles.some(r => r.name === 'Admin');
export const isLoggedIn = () => {
  return AuthApi.refreshToken()
    .then(() => true)
    .catch(() => false);
};
