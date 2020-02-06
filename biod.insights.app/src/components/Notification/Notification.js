import React from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { clearNotification } from 'actions';
import { Message } from 'semantic-ui-react';

const Notification = () => {
  const { message, notificationType } = useSelector(state => state.notification);
  const dispatch = useDispatch();

  if (!message) {
    return null;
  }
  setTimeout(() => dispatch(clearNotification()), 2000);

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
