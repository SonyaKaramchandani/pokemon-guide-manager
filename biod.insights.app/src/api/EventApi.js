import axios, { CancelToken } from 'client';
let getEventCancel = null;

const headers = {
  'X-Entity-Type': 'Event'
};

function getEvent({ eventId, diseaseId, geonameId }) {
  getEventCancel && getEventCancel();

  const url = `/api/event/${eventId}`;
  return axios.get(
    url,
    {
      params: {
        diseaseId,
        geonameId
      },
      cancelToken: new CancelToken(c => (getEventCancel = c))
    },
    { headers }
  );
}

function getCalculationBreakdown({ eventId, geonameId }) {
  const url = `/api/event/${eventId}/riskmodel`;
  return axios.get(
    url,
    {
      params: {
        eventId,
        geonameId
      }
    },
    {
      headers: {
        'X-Entity-Type': 'Calculation Breakdown'
      }
    }
  );
}

export default {
  getEvent,
  getCalculationBreakdown
};
