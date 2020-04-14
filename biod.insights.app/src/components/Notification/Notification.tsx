import { InsightsReduxState } from 'app-redux';
import { InsightsReduxNotification, NotificationReduxAction } from 'app-redux/notification';
import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Dispatch } from 'redux';
import { Message } from 'semantic-ui-react';

const Notification: React.FC = () => {
  const { message, notificationType } = useSelector<InsightsReduxState, InsightsReduxNotification>(
    state => state.notification
  );
  const dispatch = useDispatch<Dispatch<NotificationReduxAction>>();

  if (!message) {
    return null;
  }
  setTimeout(() => dispatch({ type: 'CLEAR_NOTIFICATION' }), 2000);

  const isPositiveColor = notificationType === 'SUCCESS';

  return (
    <div className="bd-toast bd-animation-fade-in">
      <Message positive={isPositiveColor} negative={!isPositiveColor}>
        {message}
      </Message>
    </div>
  );
};

export default Notification;
