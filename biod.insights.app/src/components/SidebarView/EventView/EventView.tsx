/** @jsx jsx */
import constants from 'ga/constants';
import { useNonMobileEffect } from 'hooks/useNonMobileEffect';
import React, { useState, useEffect } from 'react';
import { jsx } from 'theme-ui';

import EventsApi from 'api/EventsApi';
import esriMap from 'map';
import aoiLayer from 'map/aoiLayer';
import eventsView from 'map/events';
import { notifyEvent } from 'utils/analytics';
import { Panels } from 'utils/constants';
import * as dto from 'client/dto';

import { EventDetailPanel } from '../EventDetailPanel';
import { EventListPanel } from './EventListPanel';

interface EventViewProps {}

const EventView: React.FC<EventViewProps> = ({ ...props }) => {
  const [eventDetailPanelIsMinimized, setEventDetailPanelIsMinimized] = useState(false);
  const [eventListPanelIsMinimized, setEventListPanelIsMinimized] = useState(false);
  const [eventDetailPanelIsVisible, setEventDetailPanelIsVisible] = useState(false);
  const [eventId, setEventId] = useState(null);
  const [eventTitle, setEventTitle] = useState<string>(null);
  const [events, setEvents] = useState<dto.GetEventListModel>({ countryPins: [], eventsList: [] });
  const [isEventListLoading, setIsEventListLoading] = useState(false);
  const [activePanel, setActivePanel] = useState(Panels.EventListPanel);

  useNonMobileEffect(() => {
    aoiLayer.clearAois();
  }, []);

  useEffect(() => {
    setIsEventListLoading(true);
    EventsApi.getEvents({})
      .then(({ data }) => {
        setEvents(data);
      })
      .finally(() => {
        setIsEventListLoading(false);
      });
  }, []);

  useEffect(() => {
    const eventId = props['*'] || null;
    if (eventId) {
      setEventId(eventId);
      setEventDetailPanelIsVisible(true);
      setActivePanel(Panels.EventDetailPanel);
    }
  }, [props, setEventId, setEventDetailPanelIsVisible, setActivePanel]);

  useNonMobileEffect(() => {
    if (!eventId) {
      eventsView.updateEventView(events.countryPins);
      esriMap.showEventsView(true);
    }
  }, [events, eventId]);

  const handleOnSelect = (eventId, title) => {
    setEventId(eventId);
    setActivePanel(Panels.EventDetailPanel);
    setEventDetailPanelIsVisible(true);

    notifyEvent({
      action: constants.Action.OPEN_EVENT_DETAILS,
      category: constants.Category.EVENTS,
      label: `Open from list: ${eventId} | ${title}`,
      value: eventId
    });
  };

  const handleOnClose = () => {
    setActivePanel(Panels.EventListPanel);
    setEventDetailPanelIsVisible(false);
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
        height: '100%'
      }}
    >
      <EventListPanel
        activePanel={activePanel}
        eventId={eventId}
        events={events}
        onSelect={handleOnSelect}
        isMinimized={eventListPanelIsMinimized}
        isEventListLoading={isEventListLoading}
        onMinimize={handleEventListMinimized}
      />
      {eventDetailPanelIsVisible && (
        <EventDetailPanel
          activePanel={activePanel}
          eventId={eventId}
          onClose={handleOnClose}
          isMinimized={eventDetailPanelIsMinimized}
          onMinimize={handleEventDetailMinimized}
          summaryTitle="My Events"
        />
      )}
    </div>
  );
};

export default EventView;
