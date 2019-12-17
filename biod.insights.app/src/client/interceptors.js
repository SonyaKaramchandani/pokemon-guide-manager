import store from 'store';
import { showSuccessNotification, showErrorNotification } from 'actions';

export const responseInterceptor = response => {
  if (['post', 'put', 'delete'].includes(response.config.method)) {
    const actionTypes = {
      post: `created`,
      put: `updated`,
      delete: `deleted`
    };

    const entityType = response.config.headers['X-Entity-Type'];
    const actionType = actionTypes[response.config.method];
    console.log('res', response, entityType, actionType);
    store.dispatch(showSuccessNotification(`${entityType} ${actionType} successfully`));
  }
  return response;
};

export const errorInterceptor = error => {
  const actionTypes = {
    get: 'fetch',
    post: `create`,
    put: `update`,
    delete: `delete`
  };

  const entityType = error.response.config.headers['X-Entity-Type'];
  const actionType = actionTypes[error.response.config.method];
  store.dispatch(showErrorNotification(`Failed to ${actionType} ${entityType}`));

  return Promise.reject(error);
};
