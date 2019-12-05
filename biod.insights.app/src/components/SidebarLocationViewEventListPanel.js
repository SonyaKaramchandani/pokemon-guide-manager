import React, { useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import Loading from './Loading';
import EventApi from 'api/EventApi';
import DiseaseApi from 'api/DiseaseApi';
import styles from './SidebarLocationViewEventListPanel.module.scss';
import ListGroup from 'react-bootstrap/ListGroup';

function DiseaseEventListItem({ diseaseId, locationId, id, name, description, onSelect }) {
  return (
    <ListGroup.Item action onClick={() => onSelect(id)}>
      <header>{name}</header>
      <div>{description}</div>
    </ListGroup.Item>
  );
}

function SidebarLocationViewEventListPanel({ diseaseId, locationId, onClose, onSelect }) {
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
        <header>{disease.name}</header>
        <div>{disease.description}</div>
      </ListGroup.Item>
      {events.map(event => (
        <DiseaseEventListItem key={event.id} {...event} onSelect={onSelect} />
      ))}
    </Container>
  );
}

export default SidebarLocationViewEventListPanel;
