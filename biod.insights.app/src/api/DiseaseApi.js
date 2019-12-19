import axios from 'client';

const headers = {
  'X-Entity-Type': 'Disease'
};

function getDiseaseRiskByLocation({ geonameId }) {
  return geonameId
    ? axios.get(`/api/diseaserisk?geonameId=${geonameId}`, { headers })
    : axios.get(`/api/diseaserisk`, { headers });
}

function getDisease({ diseaseId }) {
  return axios.get(`/api/disease/${diseaseId}`, { headers });
}

export default {
  getDiseaseRiskByLocation,
  getDisease
};
