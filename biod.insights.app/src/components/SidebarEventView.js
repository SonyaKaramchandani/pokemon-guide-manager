import React, { useState } from 'react';
import SidebarEventViewEventListPanel from './SidebarEventViewEventListPanel';
import SidebarEventViewEventDetailPanel from './SidebarEventViewEventDetailPanel';

function SidebarEventView({ onViewChange }) {
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
      <SidebarEventViewEventListPanel
        onViewChange={onViewChange}
        onSelect={handleEventListPanelOnSelect}
      />
      {eventDetailPanelIsVisible && (
        <SidebarEventViewEventDetailPanel
          eventId={eventId}
          onClose={handleEventDetailPanelOnClose}
        />
      )}
    </>
  );
}

export default SidebarEventView;
