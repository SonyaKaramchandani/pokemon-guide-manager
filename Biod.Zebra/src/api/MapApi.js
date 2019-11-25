import axios from 'axios';

function getDestinationAirport(eventId, geonameIds = '-1') {
    return axios.get(window.biod.urls.getDestinationAirports, {
        params: {
            'EventId': eventId,
            'GeonameIds': geonameIds        // Global view takes in -1 for geonames
        }
    })
}

function getCountryShape(geonameId) {
    return axios.get(window.biod.urls.getCountryShapeAsText, {
        params: {
            'GeonameId': geonameId
        }
    });
}

function getGeonameShapes(geonameIds) {
    return axios.get(window.biod.urls.getGeonameShapesAsText, {
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
