import axios from 'axios';

function getDestinationAirport(eventId, geonameIds = '-1') {
  return axios.get('/api/geoname/GetDestinationAirports', {
    params: {
      EventId: eventId,
      GeonameIds: geonameIds // Global view takes in -1 for geonames
    }
  });
}

function getCountryShape(geonameId) {
  return axios.get('/api/geoname/GetCountryShapeAsText', {
    params: {
      GeonameId: geonameId
    }
  });
}

function getGeonameShapes(geonameIds) {
  return axios.get('/api/geoname/GetGeonameShapesAsText', {
    params: {
      GeonameIds: geonameIds.join(',')
    }
  });
}

export default {
  getDestinationAirport,
  getCountryShape,
  getGeonameShapes
};
