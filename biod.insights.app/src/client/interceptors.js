import store from 'store';
import { showSuccessNotification, showErrorNotification } from 'actions';
import AuthApi from 'api/AuthApi';
import axios from 'axios';
import axiosInstance from './index';
import docCookies from 'utils/cookieHelpers';
import config from 'config';

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

export const requestInterceptor = request => {//TODO: Queue all calls when token refresh in progress
  //Sets auth header for all outgoing requests  
  request.headers['Authorization'] = `Bearer ${docCookies.getItem('_jid') || ''}`
  return request
}

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
  if (error && error.config && error.response && error.response.status === 401) {
    return AuthApi.refreshToken()
      .then(({ data: { access_token, expires_in } }) => {
        docCookies.setItem('_jid', access_token, expires_in);
        error.config.headers['Authorization'] = `Bearer ${access_token}`;
        return axiosInstance.request(error.config);
      })
      .catch(async () => {
        await AuthApi.logOut();
        window.location = `${config.zebraAppBaseUrl}/Account/Login?ReturnUrl=${window.location.pathname}`;
        return Promise.reject(error);
      });
  }

  if (error && error.response && error.response.config) {
    const method = error.response.config.method || '';
    const entityType = error.response.config.headers['X-Entity-Type'];

    if (entityType) {
      const actionType = errorActionTypes[method];
      store.dispatch(showErrorNotification(`Failed to ${actionType} ${entityType}`));
    }
  } else if (!axios.isCancel(error)) {
    store.dispatch(showErrorNotification(`Network error`));
  }
  return Promise.reject(error);
};
