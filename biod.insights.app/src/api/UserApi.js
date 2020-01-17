import axios from 'client';

function getProfile() {
  return axios.get('/api/userprofile');
}

export default {
  getProfile
};
