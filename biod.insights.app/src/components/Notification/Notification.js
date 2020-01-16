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

  const cssClassName = notificationType === 'SUCCESS' ? `positive` : `negative`;
  return (
    <Message
      className={cssClassName}
      style={{
        position: 'absolute',
        right: 20,
        top: 54,
        // map elements have zIndex 100
        // one up to display on top of map
        zIndex: 101
      }}
    >
      {message}
    </Message>
  );
};

export default Notification;
