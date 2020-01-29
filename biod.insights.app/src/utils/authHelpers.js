import AuthApi from '../api/AuthApi';

export const isUserAdmin = userProfile =>
  userProfile && userProfile.roles.some(r => r.name === 'Admin');

export const isLoggedIn = () => {
  return AuthApi.refreshToken()
    .then(() => true)
    .catch(() => false);
};
