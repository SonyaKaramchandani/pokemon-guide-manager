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

function getGeonameShape(geonameId) {
  return axios.get(`/api/geoname/${geonameId}`);
}

function getGeonameShapes(geonameIds, cancelPreviousCalls = false) {
  if (cancelPreviousCalls) {
    cancelGetGeonameShapes();
  }

  return axios.post(
    '/api/geoname',
    {
      geonameIds: geonameIds.filter(g => g !== null && !isNaN(g))
    },
    {
      cancelToken: new CancelToken(c => (getGeonameShapesCancel = c))
    }
  );
}

function cancelGetGeonameShapes() {
  getGeonameShapesCancel && getGeonameShapesCancel();
}

export default {
  getUserLocations,
  postUserLocation,
  deleteUserLocation,
  searchLocations,
  getGeonameShape,
  getGeonameShapes,
  cancelGetGeonameShapes
};
