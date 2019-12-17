import React, { useState } from 'react';
import { EventListPanel } from './EventListPanel';
import { EventDetailPanel } from '../EventDetailPanel';

function EventView({ onViewChange }) {
  const [eventDetailPanelIsVisible, setEventDetailPanelIsVisible] = useState(false);
  const [eventId, setEventId] = useState('');

  function handleEventListPanelOnSelect(eventId) {
    setEventId(eventId);
    setEventDetailPanelIsVisible(true);
  }

  function handleEventDetailPanelOnClose() {
    setEventDetailPanelIsVisible(false);
  }

  return (
    <>
      <EventListPanel onViewChange={onViewChange} onSelect={handleEventListPanelOnSelect} />
      {eventDetailPanelIsVisible && (
        <EventDetailPanel eventId={eventId} onClose={handleEventDetailPanelOnClose} />
      )}
    </>
  );
}

export default EventView;
