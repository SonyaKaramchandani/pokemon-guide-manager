import axios, { CancelToken } from 'client';
let getEventCancel = null;

const headers = {
  'X-Entity-Type': 'Event'
};

function getEvent({ eventId, diseaseId, geonameId }) {
  getEventCancel && getEventCancel();

  const url = !!eventId ? `/api/event/${eventId}` : `/api/event`;
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

export default {
  getEvent
};
