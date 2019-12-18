import axios from 'axios';

function getDiseaseRiskByLocation({ geonameId }) {
  return geonameId
    ? axios.get(`/api/diseaserisk?geonameId=${geonameId}`)
    : axios.get(`/api/diseaserisk`);
}

function getDisease({ diseaseId }) {
  return axios.get(`/api/disease/${diseaseId}`);
}

export default {
  getDiseaseRiskByLocation,
  getDisease
};
