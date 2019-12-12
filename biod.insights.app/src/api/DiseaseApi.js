import axios from 'axios';

function getDiseaseRiskByLocation({ geonameId }) {
  return axios.get(`/api/diseaserisk`, {
    geonameId
  });
}

function getDisease({ diseaseId }) {
  return axios.get(`/api/disease/${diseaseId}`);
}

export default {
  getDiseaseRiskByLocation,
  getDisease
};
