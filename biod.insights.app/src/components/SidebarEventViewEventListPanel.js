import React, { useState, useEffect } from 'react';
import Loading from './Loading';
import SidebarViewSwitchToLocationView from './SidebarViewSwitchToLocationView';
import EventApi from 'api/EventApi';
import styles from './SidebarEventViewEventListPanel.module.scss';
import ListGroup from 'react-bootstrap/ListGroup';

function EventListItem({ id, name, description, onSelect }) {
  return (
    <ListGroup.Item action onClick={() => onSelect(id)}>
      <header>{name}</header>
      <div>{description}</div>
    </ListGroup.Item>
  );
}

function SidebarGlobalViewEventListPanel({ onSelect, onViewChange }) {
  const [events, setEvents] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    EventApi.getEvents({ id: '?' })
      .then(({ data }) => {
        setEvents(data);
      })
      .finally(() => setLoading(false));
  }, []);

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
      <SidebarViewSwitchToLocationView onViewChange={onViewChange} />
      {events.map(event => (
        <EventListItem key={event.id} {...event} onSelect={onSelect} />
      ))}
    </Container>
  );
}

export default SidebarGlobalViewEventListPanel;
