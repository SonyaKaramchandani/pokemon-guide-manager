import React, { useState, useEffect } from 'react';
import { Button } from 'semantic-ui-react';
import { Loading } from 'components/Loading';
import EventApi from 'api/EventApi';
import DiseaseApi from 'api/DiseaseApi';
import styles from './EventListPanel.module.scss';

function EventListItem({ diseaseId, locationId, id, name, description, onSelect }) {
  return (
    <div action onClick={() => onSelect(id)}>
      <header>{name}</header>
      <div>{description}</div>
    </div>
  );
}

function EventListPanel({ diseaseId, locationId, onClose, onSelect }) {
  const [events, setEvents] = useState([]);
  const [disease, setDisease] = useState({});
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (diseaseId) {
      setLoading(true);
      Promise.all([EventApi.getEvents({ id: diseaseId }), DiseaseApi.getDisease({ id: diseaseId })])
        .then(results => {
          setEvents(results[0].data);
          setDisease(results[1].data);
        })
        .finally(() => setLoading(false));
    }
  }, [diseaseId]);

  if (!diseaseId) {
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
        <header>{disease.name}</header>
        <div>{disease.description}</div>
      </div>
      {events.map(event => (
        <EventListItem key={event.id} {...event} onSelect={onSelect} />
      ))}
    </Container>
  );
}

export default EventListPanel;
