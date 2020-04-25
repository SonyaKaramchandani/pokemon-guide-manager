import axios, { CancelToken } from 'client';
import { AxiosResponse } from 'axios';
import * as dto from 'client/dto';

let getEventCancel = null;

function getEvent(options: {
  eventId: number;
  diseaseId: number;
  geonameId: number;
}): Promise<AxiosResponse<dto.GetEventModel>> {
  const { eventId, diseaseId, geonameId } = options;
  getEventCancel && getEventCancel();

  const url = `/api/event/${eventId}`;
  return axios.get(url, {
    params: {
      diseaseId,
      geonameId
    },
    cancelToken: new CancelToken(c => (getEventCancel = c)),
    headers: {
      'X-Entity-Type': 'Event'
    }
  });
}

function getCalculationBreakdown(options: {
  eventId: number;
  geonameId: number;
}): Promise<AxiosResponse<dto.CalculationBreakdownModel>> {
  const { eventId, geonameId } = options;
  const url = `/api/event/${eventId}/riskmodel`;
  return axios.get(url, {
    params: {
      eventId,
      geonameId
    },
    headers: {
      'X-Entity-Type': 'Calculation Breakdown'
    }
  });
}

export default {
  getEvent,
  getCalculationBreakdown
};
