import { SHOW_SUCCESS_NOTIFICATION, SHOW_ERROR_NOTIFICATION, CLEAR_NOTIFICATION } from '../actions';

export const SUCCESS = 'SUCCESS',
  ERROR = 'ERROR';

const initialState = {
  notificationType: '',
  message: ''
};

const notification = (state = initialState, action) => {
  switch (action.type) {
    case SHOW_SUCCESS_NOTIFICATION:
      return { notificationType: SUCCESS, message: action.payload };
    case SHOW_ERROR_NOTIFICATION:
      return { notificationType: ERROR, message: action.payload };
    case CLEAR_NOTIFICATION:
      return { ...initialState };
    default:
      return state;
  }
};

export default notification;
