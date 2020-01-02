import axios from 'client';

const headers = {
  'X-Entity-Type': 'Event'
};

function getEvent({ eventId, diseaseId, geonameId }) {
  const url = !!eventId ? `/api/event/${eventId}` : `/api/event`;
  return axios.get(
    url,
    {
      params: {
        diseaseId,
        geonameId
      }
    },
    { headers }
  );
}

function getEventCaseCount({ eventId }) {
  const url = `/api/event/${eventId}/casecount`;

  return axios.get(url, {
    params: {
      eventId
    }
  });
}

export default {
  getEvent,
  getEventCaseCount
};
