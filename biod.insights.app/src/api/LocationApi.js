import axios from 'client';

const _default = { userId: '557c8807-1eb3-41c5-a310-1450e6c68b08' };

async function getUserLocations({ userId } = _default) {
  return await axios.get(`/api/user/${userId}/location`);
}

function postUserLocation({ userId = _default.userId, geonameId }) {
  return axios.put(`/api/user/${userId}/location`, {
    geonameId
  });
}

function deleteUserLocation({ userId = _default.userId, geonameId }) {
  return axios.delete(`/api/user/${userId}/location/${geonameId}`);
}

function searchLocations({ locationName }) {
  return axios.get(`/api/geoname`, {
    name: locationName
  });
}

export default {
  getUserLocations,
  postUserLocation,
  deleteUserLocation,
  searchLocations
};
