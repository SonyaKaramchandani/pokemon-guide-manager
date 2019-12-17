import React, { useState, useEffect } from 'react';
import { Loading } from 'components/Loading';
import { SwitchToLocationView } from 'components/SidebarView/SwitchToLocationView';
import EventApi from 'api/EventApi';
import styles from './EventListPanel.module.scss';

function EventListItem({ id, name, description, onSelect }) {
  return (
    <div action onClick={() => onSelect(id)}>
      <header>{name}</header>
      <div>{description}</div>
    </div>
  );
}

function EventListPanel({ onSelect, onViewChange }) {
  const [events, setEvents] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    EventApi.getEvents({ id: '?' })
      .then(({ data }) => {
        setEvents(data);
      })
      .finally(() => setLoading(false));
  }, []);

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
      <SwitchToLocationView onViewChange={onViewChange} />
      {events.map(event => (
        <EventListItem key={event.id} {...event} onSelect={onSelect} />
      ))}
    </Container>
  );
}

export default EventListPanel;
