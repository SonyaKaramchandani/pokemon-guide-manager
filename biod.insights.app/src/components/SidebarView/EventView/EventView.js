import React, { useState } from 'react';
import { EventListPanel } from './EventListPanel';
import { EventDetailPanel } from '../EventDetailPanel';
import { useEffect } from 'react';

const EventView = props => {
  const [eventDetailPanelIsVisible, setEventDetailPanelIsVisible] = useState(false);
  const [eventId, setEventId] = useState(null);

  useEffect(() => {
    const id = props['*'] || null;
    setEventId(id);
    setEventDetailPanelIsVisible(!!id);
  }, [props, setEventId, setEventDetailPanelIsVisible]);

  const handleOnSelect = eventId => {
    setEventId(eventId);
    setEventDetailPanelIsVisible(true);
  };

  const handleOnClose = () => {
    setEventDetailPanelIsVisible(false);
  };

  return (
    <>
      <EventListPanel eventId={eventId} onSelect={handleOnSelect} />
      {eventDetailPanelIsVisible && <EventDetailPanel eventId={eventId} onClose={handleOnClose} />}
    </>
  );
};

export default EventView;
