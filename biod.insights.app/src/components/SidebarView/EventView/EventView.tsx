/** @jsx jsx */
import constants from 'ga/constants';
import { useNonMobileEffect } from 'hooks/useNonMobileEffect';
import React, { useState, useEffect } from 'react';
import { jsx } from 'theme-ui';
import { navigate } from '@reach/router';

import EventsApi from 'api/EventsApi';
import esriMap from 'map';
import aoiLayer from 'map/aoiLayer';
import eventsView from 'map/events';
import { notifyEvent } from 'utils/analytics';
import * as dto from 'client/dto';
import { useDependentState } from 'hooks/useDependentState';

import { EventDetailPanel } from '../EventDetailPanel';
import { EventListPanel } from './EventListPanel';
import { ActivePanel } from '../sidebar-types';
import { parseIntOrNull } from 'utils/stringHelpers';

interface EventViewProps {
  eventId: string;
}

const EventView: React.FC<EventViewProps> = ({ eventId: eventIdParam, ...props }) => {
  const [eventDetailPanelIsMinimized, setEventDetailPanelIsMinimized] = useState(false);
  const [eventListPanelIsMinimized, setEventListPanelIsMinimized] = useState(false);
  const [eventTitle, setEventTitle] = useState<string>(null);
  const [events, setEvents] = useState<dto.GetEventListModel>({ countryPins: [], eventsList: [] });
  const [isEventListLoading, setIsEventListLoading] = useState(false);

  const eventId = useDependentState(() => parseIntOrNull(eventIdParam), [eventIdParam]);
  const activePanel = useDependentState<ActivePanel>(
    () => (eventId ? 'EventDetailPanel' : 'EventListPanel'),
    [eventId]
  );
  const isVisibleEDP = useDependentState(() => !!eventId, [eventId]);

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

  // TODO: 4d91fec5: should these 2 effects be identical?
  useEffect(() => {
    const eventList = (events && events.eventsList) || [];
    const selectedEvent = eventList.find(d => d.eventInformation.id === eventId);
    setEventTitle(selectedEvent && selectedEvent.eventInformation.title);
  }, [events, eventId]);

  useNonMobileEffect(() => {
    if (!eventId) {
      eventsView.updateEventView(events.countryPins);
      esriMap.showEventsView(true);
    }
  }, [events, eventId]);

  const handleOnEventSelected = (eventId: number, title: string) => {
    navigate(`/event/${eventId}`);
    notifyEvent({
      action: constants.Action.OPEN_EVENT_DETAILS,
      category: constants.Category.EVENTS,
      label: `Open from list: ${eventId} | ${title}`,
      value: eventId
    });
  };

  const handleOnClose = () => {
    navigate(`/event`);
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
        height: '100%',
        maxWidth: ['100%', 'calc(100vw - 200px)']
      }}
    >
      <EventListPanel
        activePanel={activePanel}
        eventId={eventId}
        events={events}
        onEventSelected={handleOnEventSelected}
        isMinimized={eventListPanelIsMinimized}
        isEventListLoading={isEventListLoading}
        onMinimize={handleEventListMinimized}
      />
      {isVisibleEDP && (
        <EventDetailPanel
          activePanel={activePanel}
          eventId={eventId}
          eventTitleBackup={eventTitle || 'Loading...'}
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
