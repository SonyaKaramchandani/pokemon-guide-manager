import axios, { CancelToken } from 'client';
import AwesomeDebouncePromise from 'awesome-debounce-promise';
let searchLocationsCancel = null;
let getGeonameShapesCancel = null;

const headers = {
  'X-Entity-Type': 'Location'
};

function getUserLocations() {
  return axios.get(`/api/userlocation`, { headers });
}

function postUserLocation({ geonameId }) {
  return axios.post(
    `/api/userlocation`,
    { geonameId },
    {
      headers
    }
  );
}

function deleteUserLocation({ geonameId }) {
  return axios.delete(`/api/userlocation/${geonameId}`, {
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
  cancelgetGeonameShapes();
  
  return axios.get('/api/geoname', {
    cancelToken: new CancelToken(c => (getGeonameShapesCancel = c)),
    params: {
      geonameIds: geonameIds.join(',')
    }
  });
}

function cancelgetGeonameShapes() {
  getGeonameShapesCancel && getGeonameShapesCancel();
}

export default {
  getUserLocations,
  postUserLocation,
  deleteUserLocation,
  searchLocations,
  getGeonameShapes,
  cancelgetGeonameShapes
};
