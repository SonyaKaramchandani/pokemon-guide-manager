import store from 'app-redux';
import axios from 'axios';
import config from 'config';

import AuthApi from 'api/AuthApi';
import docCookies from 'utils/cookieHelpers';
import { nameof } from 'utils/typeHelpers';

import axiosInstance from '.';

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

export const requestInterceptor = request => {
  //TODO: Queue all calls when token refresh in progress
  //Sets auth header for all outgoing requests
  request.headers['Authorization'] = `Bearer ${docCookies.getItem('_jid') || ''}`;
  return request;
};

export const responseInterceptor = response => {
  const method = response && response.config && response.config.method;
  if (
    [
      nameof<typeof responseActionTypes>('post'),
      nameof<typeof responseActionTypes>('put'),
      nameof<typeof responseActionTypes>('delete')
    ].includes(method)
  ) {
    const entityType = response.config.headers['X-Entity-Type'];
    if (entityType) {
      const actionType = responseActionTypes[response.config.method];
      store.dispatch({
        type: 'SHOW_SUCCESS_NOTIFICATION',
        payload: `${entityType} ${actionType} successfully`
      });
    }
  }
  return response;
};

export const errorInterceptor = error => {
  if (error && error.config && error.response && error.response.status === 401) {
    return AuthApi.refreshToken()
      .then(access_token => {
        error.config.headers['Authorization'] = `Bearer ${access_token}`;
        return axiosInstance.request(error.config);
      })
      .catch(async () => {
        await AuthApi.logOut();
        window.location.href = `${config.zebraAppBaseUrl}/Account/Login?ReturnUrl=${window.location.href}`;
        return Promise.reject(error);
      });
  }

  if (error && error.response && error.response.config) {
    const method = error.response.config.method || '';
    if (
      [
        nameof<typeof errorActionTypes>('get'),
        nameof<typeof errorActionTypes>('post'),
        nameof<typeof errorActionTypes>('put'),
        nameof<typeof errorActionTypes>('delete')
      ].includes(method)
    ) {
      const entityType = error.response.config.headers['X-Entity-Type'];
      if (entityType) {
        const actionType = errorActionTypes[method];
        store.dispatch({
          type: 'SHOW_ERROR_NOTIFICATION',
          payload: `Failed to ${actionType} ${entityType}`
        });
      }
    }
  } else if (!axios.isCancel(error)) {
    store.dispatch({
      type: 'SHOW_ERROR_NOTIFICATION',
      payload: `Network error`
    });
  }
  return Promise.reject(error);
};
