import axios from 'axios';

function getDestinationAirport(eventId, geonameIds) {
    return axios.get(window.biod.urls.getDestinationAirports, {
        params: {
            'EventId': eventId,
            'GeonameIds': geonameIds || '-1'
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
