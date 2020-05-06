import axios, { CancelToken } from 'client';
import { AxiosResponse } from 'axios';
import * as dto from 'client/dto';

let searchLocationsCancel = null;
let getGeonameShapesCancel = null;

const headers = {
  'X-Entity-Type': 'Location'
};

function getUserLocations(): Promise<AxiosResponse<dto.GetUserLocationModel>> {
  return axios.get(`/api/userlocation`, { headers });
}

function postUserLocation(options: {
  geonameId: number;
}): Promise<AxiosResponse<dto.GetUserLocationModel>> {
  const { geonameId } = options;
  return axios.post(
    `/api/userlocation`,
    { geonameId },
    {
      headers
    }
  );
}

function deleteUserLocation(options: {
  geonameId: number;
}): Promise<AxiosResponse<dto.GetUserLocationModel>> {
  const { geonameId } = options;
  return axios.delete(`/api/userlocation/${geonameId}`, {
    headers
  });
}

function searchLocations(options: {
  name: string;
}): Promise<AxiosResponse<dto.SearchGeonameModel[]>> {
  const { name } = options;
  searchLocationsCancel && searchLocationsCancel();

  return axios.get(`/api/geonamesearch?name=${name}`, {
    cancelToken: new CancelToken(c => (searchLocationsCancel = c)),
    headers
  });
}

function getGeonameShapes(
  geonameIds: number[],
  cancelPreviousCalls = false
): Promise<AxiosResponse<dto.GetGeonameModel[]>> {
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

function searchCity(name: string): Promise<AxiosResponse<dto.SearchGeonameModel[]>> {
  return axios.get(`/api/citysearch`, {
    headers: {
      params: { name },
      xEntityType: 'City'
    }
  });
}

export default {
  getUserLocations,
  postUserLocation,
  deleteUserLocation,
  searchLocations,
  searchCity,
  getGeonameShapes,
  cancelGetGeonameShapes
};
