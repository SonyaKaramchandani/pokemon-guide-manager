import store from 'store';
import { showSuccessNotification, showErrorNotification } from 'actions';

const responseActionTypes = {
  post: `created`,
  put: `updated`,
  delete: `deleted`
};

const errorActionTypes = {
  get: 'fetch',
  post: `create`,
  put: `update`,
  delete: `delete`
};

export const responseInterceptor = response => {
  const method = response && response.config && response.config.method;
  if (['post', 'put', 'delete'].includes(method)) {
    const entityType = response.config.headers['X-Entity-Type'];
    if (entityType) {
      const actionType = responseActionTypes[response.config.method];
      store.dispatch(showSuccessNotification(`${entityType} ${actionType} successfully`));
    }
  }
  return response;
};

export const errorInterceptor = error => {
  if (error && error.response && error.response.config) {
    const method = error.response.config.method || '';
    const entityType = error.response.config.headers['X-Entity-Type'];

    if (entityType) {
      const actionType = errorActionTypes[method];
      store.dispatch(showErrorNotification(`Failed to ${actionType} ${entityType}`));
    }
  } else {
    store.dispatch(showErrorNotification(`Network error`));
  }
  return Promise.reject(error);
};
