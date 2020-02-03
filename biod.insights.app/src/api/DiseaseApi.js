import axios from 'client';

const headers = {
  'X-Entity-Type': 'Disease'
};

function getDiseaseRiskByLocation({ geonameId }) {
  return axios.get(
    `/api/diseaserisk`,
    {
      params: {
        geonameId
      }
    },
    { headers }
  );
}

function getDisease({ diseaseId }) {
  return axios.get(`/api/disease/${diseaseId}`, { headers });
}

function getDiseaseCaseCount({ diseaseId, geonameId }) {
  const url = `/api/disease/${diseaseId}/casecount`;

  return axios.get(url, {
    params: {
      diseaseId,
      geonameId
    }
  });
}

export default {
  getDiseaseRiskByLocation,
  getDisease,
  getDiseaseCaseCount
};
