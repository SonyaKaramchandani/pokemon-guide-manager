import axios, { CancelToken } from 'client';
let getDiseaseRiskByLocationCancel = null;

const headers = {
  'X-Entity-Type': 'Disease'
};

function getDiseaseRiskByLocation({ geonameId }) {
  getDiseaseRiskByLocationCancel && getDiseaseRiskByLocationCancel();
  return axios.get(
    `/api/diseaserisk`,
    {
      params: {
        geonameId
      },
      cancelToken: new CancelToken(c => (getDiseaseRiskByLocationCancel = c))
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
