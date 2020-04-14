import { Action } from 'redux';

export type NotificationActionType =
  | 'SHOW_SUCCESS_NOTIFICATION'
  | 'SHOW_ERROR_NOTIFICATION'
  | 'CLEAR_NOTIFICATION';
export type NotificationType = 'ERROR' | 'SUCCESS';

export interface NotificationReduxAction extends Action {
  type: NotificationActionType;
  payload?: string;
}

export interface InsightsReduxNotification {
  message: string;
  notificationType: NotificationType;
}

const initialState: InsightsReduxNotification = {
  notificationType: null,
  message: ''
};

function notification(
  state: InsightsReduxNotification = initialState,
  action: NotificationReduxAction
): InsightsReduxNotification {
  switch (action.type) {
    case 'SHOW_SUCCESS_NOTIFICATION':
      return { notificationType: 'SUCCESS', message: action.payload };
    case 'SHOW_ERROR_NOTIFICATION':
      return { notificationType: 'ERROR', message: action.payload };
    case 'CLEAR_NOTIFICATION':
      return { ...initialState };
    default:
      return state;
  }
}

export default notification;
