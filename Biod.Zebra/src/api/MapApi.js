import axios from 'axios';

function getDestinationAirport(id, geonameIds) {
    return axios.get(window.biod.urls.getDestinationAirports, {
        params: {
            'EventId': id,
            'GeonameIds': geonameIds || '-1'
        }
    })
}

function getCountryShape(id) {
    return axios.get(window.biod.urls.getCountryShapeAsText, {
        params: {
            'GeonameId': id
        }
    });
}

export default {
    getDestinationAirport,
    getCountryShape
};
