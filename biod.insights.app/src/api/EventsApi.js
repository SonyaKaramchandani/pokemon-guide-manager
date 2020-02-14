import axios, { CancelToken } from 'client';
let getEventsCancel = null;

const headers = {
  'X-Entity-Type': 'Events'
};

function getEvents({ diseaseId, geonameId }) {
  getEventsCancel && getEventsCancel();

  const url = `/api/event`;
  return axios.get(
    url,
    {
      params: {
        diseaseId,
        geonameId
      },
      cancelToken: new CancelToken(c => (getEventsCancel = c))
    },
    { headers }
  );
}

export default {
  getEvents
};
