import axios, { CancelToken } from 'client';
import { AxiosResponse } from 'axios';
import * as dto from 'client/dto';

let getDiseaseRiskByLocationCancel = null;

const headers = {
  'X-Entity-Type': 'Disease'
};

function getDiseaseRiskByLocation(options: {
  geonameId: number;
}): Promise<AxiosResponse<dto.RiskAggregationModel>> {
  const { geonameId } = options;
  getDiseaseRiskByLocationCancel && getDiseaseRiskByLocationCancel();
  return axios.get(`/api/diseaserisk`, {
    params: {
      geonameId
    },
    cancelToken: new CancelToken(c => (getDiseaseRiskByLocationCancel = c)),
    headers
  });
}

function getDisease(options: {
  diseaseId: number;
}): Promise<AxiosResponse<dto.DiseaseInformationModel>> {
  const { diseaseId } = options;
  return axios.get(`/api/disease/${diseaseId}`, { headers });
}

function getDiseaseCaseCount(options: {
  diseaseId: number;
  geonameId: number;
}): Promise<AxiosResponse<dto.CaseCountModel>> {
  const { diseaseId, geonameId } = options;
  const url = `/api/disease/${diseaseId}/casecount`;

  return axios.get(url, {
    params: {
      diseaseId,
      geonameId
    }
  });
}

function getDiseaseGroups(): Promise<AxiosResponse<dto.DiseaseGroupModel[]>> {
  return axios.get(`/api/disease/groups`, {
    headers: {
      'X-Entity-Type': 'Disease Groups'
    }
  });
}

export default {
  getDiseaseRiskByLocation,
  getDisease,
  getDiseaseCaseCount,
  getDiseaseGroups
};
