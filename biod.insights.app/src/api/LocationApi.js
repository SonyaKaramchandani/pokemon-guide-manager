import axios from 'client';

const _default = { userId: '557c8807-1eb3-41c5-a310-1450e6c68b08' };

async function getUserLocations({ userId } = _default) {
  const { data } = await axios.get(`/api/user/${userId}/location`);
  return data.geonames;
}

function putUserLocation({ userId = _default.userId, geonameId }) {
  return axios.put(`/api/user/${userId}/location`, {
    geonameId
  });
}

function deleteUserLocation({ userId = _default.userId, geonameId }) {
  return axios.delete(`/api/user/${userId}/location/${geonameId}`);
}

function search(value) {
  const geonames = [
    {
      geonameId: 7534563,
      name: 'Sainte-Helene-de-Breakeyville',
      country: 'Canada',
      locationType: 2
    },
    {
      geonameId: 6690232,
      name: 'Baie-Saint-Paul',
      country: 'Canada',
      locationType: 2
    },
    {
      geonameId: 6691318,
      name: 'Bas-Saint-Laurent',
      country: 'Canada',
      locationType: 2
    },
    {
      geonameId: 6691319,
      name: 'Capitale-Nationale',
      country: 'Canada',
      locationType: 2
    },
    {
      geonameId: 6691320,
      name: 'Nord-du-Quebec',
      country: 'Canada',
      locationType: 2
    },
    {
      geonameId: 6691321,
      name: 'Gaspesie-Iles-de-la-Madeleine',
      country: 'Canada',
      locationType: 2
    },
    {
      geonameId: 6691322,
      name: 'Chaudiere-Appalaches',
      country: 'Canada',
      locationType: 2
    },
    {
      geonameId: 5950245,
      name: 'Ethel Park',
      country: 'Canada',
      locationType: 2
    },
    {
      geonameId: 5950250,
      name: 'Ethelton',
      country: 'Canada',
      locationType: 2
    },
    {
      geonameId: 5950267,
      name: 'Etobicoke',
      country: 'Canada',
      locationType: 2
    }
  ].map(item => ({ ...item, name: value + '-' + item.name }));

  const data = {
    geonames
  };

  return new Promise(resolve => {
    setTimeout(() => resolve({ data }), 500);
  });
}

export default {
  getUserLocations,
  putUserLocation,
  deleteUserLocation,
  search
};
