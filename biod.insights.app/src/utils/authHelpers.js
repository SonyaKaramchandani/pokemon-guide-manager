export const isUserAdmin = userProfile =>
  userProfile && userProfile.roles.some(r => r.name === 'Admin');
