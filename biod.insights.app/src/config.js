import axios from 'client';
const config = {};

export function init() {
  return new Promise((resolve, reject) => {
    axios
      .get(`/config.json?v=${Date.now()}`)
      .then(({ data }) => {
        Object.assign(config, data);
        resolve(data);
      })
      .catch(error => {
        reject(error);
      });
  });
}

export default config;