import React, { useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import Loading from './Loading';
import EventApi from 'api/EventApi';
import styles from './SidebarEventViewEventDetailPanel.module.scss';
import ListGroup from 'react-bootstrap/ListGroup';

function SidebarEventViewEventDetailPanel({ eventId, onClose }) {
  const [event, setEvent] = useState({});
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (eventId) {
      setLoading(true);
      EventApi.getEvent({ id: eventId })
        .then(({ data }) => {
          setEvent(data);
        })
        .finally(() => setLoading(false));
    }
  }, [eventId]);

  if (!eventId) {
    return null;
  }

  const Container = ({ children }) => (
    <ListGroup variant="flush" className={styles.panel}>
      {children}
    </ListGroup>
  );

  if (loading) {
    return (
      <Container>
        <Loading />
      </Container>
    );
  }

  return (
    <Container>
      <Button variant="light" block className="text-right" onClick={onClose}>
        Close
      </Button>
      <ListGroup.Item>
        <header>{event.name}</header>
        <div>{event.description}</div>
      </ListGroup.Item>
      {event.locations.map(location => (
        <ListGroup.Item key={location.id}>
          <header>{location.name}</header>
          <div>{location.description}</div>
        </ListGroup.Item>
      ))}
    </Container>
  );
}

export default SidebarEventViewEventDetailPanel;
