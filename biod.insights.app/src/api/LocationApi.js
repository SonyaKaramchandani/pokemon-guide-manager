import axios, { CancelToken } from 'client';
import AwesomeDebouncePromise from 'awesome-debounce-promise';
let searchLocationsCancel = null;

const _default = { userId: '557c8807-1eb3-41c5-a310-1450e6c68b08' };

const headers = {
  'X-Entity-Type': 'Location'
};

function getUserLocations({ userId } = _default) {
  return axios.get(`/api/user/${userId}/location`, { headers });
}

function postUserLocation({ userId = _default.userId, geonameId }) {
  return axios.post(
    `/api/user/${userId}/location`,
    { geonameId },
    {
      headers
    }
  );
}

function deleteUserLocation({ userId = _default.userId, geonameId }) {
  return axios.delete(`/api/user/${userId}/location/${geonameId}`, {
    headers
  });
}

const searchLocations = AwesomeDebouncePromise(({ name }) => {
  searchLocationsCancel && searchLocationsCancel();

  return axios.get(`/api/geonamesearch?name=${name}`, {
    cancelToken: new CancelToken(c => (searchLocationsCancel = c)),
    headers
  });
}, 500);

function getGeonameShapes(geonameIds) {
  return axios.get('/api/geoname', {
    params: {
      geonameIds: geonameIds.join(',')
    }
  });
}

export default {
  getUserLocations,
  postUserLocation,
  deleteUserLocation,
  searchLocations,
  getGeonameShapes
};
