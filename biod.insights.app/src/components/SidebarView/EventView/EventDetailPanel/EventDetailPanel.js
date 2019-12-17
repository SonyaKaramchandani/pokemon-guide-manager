import React, { useState, useEffect } from 'react';
import { Button } from 'semantic-ui-react';
import { Loading } from 'components/Loading';
import EventApi from 'api/EventApi';
import styles from './EventDetailPanel.module.scss';

function EventDetailPanel({ eventId, onClose }) {
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

  const Container = ({ children }) => <div className={styles.panel}>{children}</div>;

  if (loading) {
    return (
      <Container>
        <Loading />
      </Container>
    );
  }

  return (
    <Container>
      <Button className="ui button" onClick={onClose}>
        Close
      </Button>
      <div>
        <header>{event.name}</header>
        <div>{event.description}</div>
      </div>
      {event.locations.map(location => (
        <div key={location.id}>
          <header>{location.name}</header>
          <div>{location.description}</div>
        </div>
      ))}
    </Container>
  );
}

export default EventDetailPanel;
