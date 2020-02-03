export const SHOW_SUCCESS_NOTIFICATION = 'SHOW_SUCCESS_NOTIFICATION',
  SHOW_ERROR_NOTIFICATION = 'SHOW_ERROR_NOTIFICATION',
  CLEAR_NOTIFICATION = 'CLEAR_NOTIFICATION';

export const showSuccessNotification = message => ({
  type: SHOW_SUCCESS_NOTIFICATION,
  payload: message
});

export const showErrorNotification = message => ({
  type: SHOW_ERROR_NOTIFICATION,
  payload: message
});

export const clearNotification = () => ({
  type: CLEAR_NOTIFICATION
});
