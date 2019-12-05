import React from 'react';
import { useSelector, useDispatch } from 'react-redux';
import Toast from 'react-bootstrap/Toast';
import { clearNotification } from 'actions';

function Notification() {
  const { message, notificationType } = useSelector(state => state.notification);
  const dispatch = useDispatch();

  if (!message) {
    return null;
  }

  setTimeout(() => dispatch(clearNotification()), 1000);

  const cssClassName = notificationType === 'SUCCESS' ? `bg-success` : `bg-danger`;
  return (
    <Toast
      style={{
        position: 'absolute',
        right: 20,
        top: 54,
        zIndex: 5000
      }}
    >
      <Toast.Body className={`${cssClassName} text-white`}>{message}</Toast.Body>
    </Toast>
  );
}

export default Notification;
