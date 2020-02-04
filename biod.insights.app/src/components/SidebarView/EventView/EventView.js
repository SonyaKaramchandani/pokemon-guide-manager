/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { EventListPanel } from './EventListPanel';
import { EventDetailPanel } from '../EventDetailPanel';
import EventApi from 'api/EventApi';

import { useEffect } from 'react';
import esriMap from 'map';
import eventsView from 'map/events';
import aoiLayer from 'map/aoiLayer';
import { notifyEvent } from 'utils/analytics';
import constants from 'ga/constants';

const EventView = props => {
  const [eventDetailPanelIsMinimized, setEventDetailPanelIsMinimized] = useState(false);
  const [eventListPanelIsMinimized, setEventListPanelIsMinimized] = useState(false);
  const [eventDetailPanelIsVisible, setEventDetailPanelIsVisible] = useState(false);
  const [eventId, setEventId] = useState(null);
  const [events, setEvents] = useState({ countryPins: [], eventsList: [] });
  const [isEventListLoading, setIsEventListLoading] = useState(false);

  useEffect(() => {
    aoiLayer.clearAois();
    const eventId = props['*'] || null;

    setEventId(eventId);
    loadEvents();
  }, [setEventId]);

  useEffect(() => {
    setEventDetailPanelIsVisible(!!eventId);
    if (!eventId) {
      eventsView.updateEventView(events.countryPins);
      esriMap.showEventsView(true);
    }
  }, [events, eventId, setEventDetailPanelIsVisible]);

  const loadEvents = () => {
    setIsEventListLoading(true);
    EventApi.getEvent({})
      .then(({ data }) => {
        setEvents(data);
      })
      .finally(() => {
        setIsEventListLoading(false);
      });
  };

  const handleOnSelect = (eventId, title) => {
    setEventId(eventId);
    notifyEvent({
      action: constants.Action.OPEN_EVENT_DETAILS,
      category: constants.Category.EVENTS,
      label: `Open from list: ${eventId} | ${title}`,
      value: eventId
    });
  };

  const handleOnClose = () => {
    setEventId(null);
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
        events={events}
        onSelect={handleOnSelect}
        isMinimized={eventListPanelIsMinimized}
        isEventListLoading={isEventListLoading}
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
