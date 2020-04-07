import axios from 'client';

function getMetadata() {
  return axios.get(`/api/app/metadata`, {
    headers: {
      'X-Entity-Type': 'Metadata'
    }
  });
}

export default {
  getMetadata
};
