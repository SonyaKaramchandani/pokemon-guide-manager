import { createStore, combineReducers } from 'redux';
import notification, {
  InsightsReduxNotification,
  NotificationReduxAction,
  NotificationActionType
} from './notification';

export interface InsightsReduxState {
  notification: InsightsReduxNotification;
}

//=====================================================================================================================================

type InsightsReduxAction =
  | NotificationReduxAction
  | {
      type: NotificationActionType;
    };
const store = createStore<InsightsReduxState, InsightsReduxAction, {}, {}>(
  combineReducers({
    notification
  })
);

export default store;
