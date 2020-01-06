import React, { useState } from 'react';
import { EventListPanel } from './EventListPanel';
import { EventDetailPanel } from '../EventDetailPanel';

function EventView({ onViewChange }) {
  const [eventDetailPanelIsVisible, setEventDetailPanelIsVisible] = useState(false);
  const [eventId, setEventId] = useState(null);

  function handleEventListPanelOnSelect(eventId) {
    setEventId(eventId);
    setEventDetailPanelIsVisible(true);
  }

  function handleEventDetailPanelOnClose() {
    setEventDetailPanelIsVisible(false);
  }

  return (
    <>
      <EventListPanel eventId={eventId} onSelect={handleEventListPanelOnSelect} />
      {eventDetailPanelIsVisible && (
        <EventDetailPanel eventId={eventId} onClose={handleEventDetailPanelOnClose} />
      )}
    </>
  );
}

export default EventView;
