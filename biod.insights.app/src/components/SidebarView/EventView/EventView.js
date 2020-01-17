/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { EventListPanel } from './EventListPanel';
import { EventDetailPanel } from '../EventDetailPanel';
import { useEffect } from 'react';
import LocationApi from 'api/LocationApi';
import aoiLayer from 'map/aoiLayer';
import esriMap from 'map';
import eventsView from 'map/events';

const EventView = props => {
  const [eventDetailPanelIsMinimized, setEventDetailPanelIsMinimized] = useState(false);
  const [eventListPanelIsMinimized, setEventListPanelIsMinimized] = useState(false);
  const [eventDetailPanelIsVisible, setEventDetailPanelIsVisible] = useState(false);
  const [eventId, setEventId] = useState(null);

  useEffect(() => {
    LocationApi.getUserLocations().then(({ data: { geonames } }) => aoiLayer.renderAois(geonames));
  }, []);

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
    setEventId(null);
    setEventDetailPanelIsVisible(false);
  };

  const handleOnEventListLoad = data => {
    eventsView.updateEventView(data.countryPins);
    esriMap.showEventsView(true);
  };

  const handleEventListMinimized = value => {
    setEventListPanelIsMinimized(value);
  };

  const handleEventDetailMinimized = value => {
    setEventDetailPanelIsMinimized(value);
  };

  return (
    <div
      sx={{
        display: 'flex',
        overflowY: 'auto'
      }}
    >
      <EventListPanel
        eventId={eventId}
        onSelect={handleOnSelect}
        onEventListLoad={handleOnEventListLoad}
        isMinimized={eventListPanelIsMinimized}
        onMinimize={handleEventListMinimized}
      />
      {eventDetailPanelIsVisible && (
        <EventDetailPanel
          eventId={eventId}
          onClose={handleOnClose}
          isMinimized={eventDetailPanelIsMinimized}
          onMinimize={handleEventDetailMinimized}
        />
      )}
    </div>
  );
};

export default EventView;
