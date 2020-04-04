/** @jsx jsx */
import constants from 'ga/constants';
import { useNonMobileEffect } from 'hooks/useNonMobileEffect';
import React, { useState, useEffect } from 'react';
import { jsx } from 'theme-ui';
import { navigate } from '@reach/router';

import EventsApi from 'api/EventsApi';
import aoiLayer from 'map/aoiLayer';
import mapEventsView from 'map/events';
import mapEventDetailView from 'map/eventDetails';
import { notifyEvent } from 'utils/analytics';
import { parseIntOrNull } from 'utils/stringHelpers';
import * as dto from 'client/dto';
import { useDependentState } from 'hooks/useDependentState';

import { IReachRoutePage } from 'components/_common/common-props';
import { EventDetailPanel } from '../EventDetailPanel';
import { EventListPanel } from './EventListPanel';
import { ActivePanel } from '../sidebar-types';
import { TransparencyPanel } from '../TransparencyPanel';
import { RiskType } from 'components/RisksProjectionCard/RisksProjectionCard';

type EventViewProps = IReachRoutePage & {
  eventId?: string;
  hasParameters?: boolean;
};

const EventView: React.FC<EventViewProps> = ({ eventId: eventIdParam, hasParameters = false }) => {
  const [eventDetailPanelIsMinimized, setEventDetailPanelIsMinimized] = useState(false);
  const [eventListPanelIsMinimized, setEventListPanelIsMinimized] = useState(false);
  const [eventTitle, setEventTitle] = useState<string>(null);
  const [events, setEvents] = useState<dto.GetEventListModel>(null);
  const [isEventListLoading, setIsEventListLoading] = useState(false);
  const [selectedEvent, setSelectedEvent] = useState<dto.GetEventModel>(null);
  const [selectedRiskType, setSelectedRiskType] = useState<RiskType>(null);

  const eventId = useDependentState(() => parseIntOrNull(eventIdParam), [eventIdParam]);
  const activePanel = useDependentState<ActivePanel>(
    () =>
      eventId && hasParameters
        ? 'ParametersPanel'
        : eventId
        ? 'EventDetailPanel'
        : 'EventListPanel',
    [eventId]
  );
  const isVisibleEDP = useDependentState(() => !!eventId, [eventId]);
  const isVisibleTRANSPAR = useDependentState(() => !!eventId && hasParameters, [
    eventId,
    hasParameters
  ]);

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
    if (activePanel === 'ParametersPanel') {
      setEventListPanelIsMinimized(true);
    } else {
      setEventListPanelIsMinimized(false);
    }
  }, [activePanel]);

  // TODO: 4d91fec5: should these 2 effects be identical?
  useEffect(() => {
    if (events && eventId) {
      const eventList = (events && events.eventsList) || [];
      const selectedEvent = eventList.find(d => d.eventInformation.id === eventId);
      if (selectedEvent) setEventTitle(selectedEvent.eventInformation.title);
      else navigate(`/event`); // DESIGN: PT-1191: when URL ids are not in preceeding panel, go to default dashboard
    }
  }, [events, eventId]);

  useNonMobileEffect(() => {
    if (activePanel === 'EventListPanel') {
      if (events) {
        mapEventsView.updateEventView(events.countryPins);
        mapEventsView.show();
      }
    } else {
      mapEventsView.hide();
    }
  }, [activePanel, events]);

  useNonMobileEffect(() => {
    if (activePanel === 'EventDetailPanel') {
      selectedEvent && mapEventDetailView.show(selectedEvent as any); // TODO: PT-1200
    } else mapEventDetailView.hide();
  }, [activePanel, selectedEvent]);

  const handleOnEventSelected = (eventId: number, title: string) => {
    navigate(`/event/${eventId}`);
    notifyEvent({
      action: constants.Action.OPEN_EVENT_DETAILS,
      category: constants.Category.EVENTS,
      label: `Open from list: ${eventId} | ${title}`,
      value: eventId
    });
  };

  const handleRiskParametersOnSelect = () => {
    if (!eventId) return;
    navigate(`/event/${eventId}/parameters`);
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

  const handleOnEventDetailsLoad = (event: dto.GetEventModel) => {
    setSelectedEvent(event);
  };

  const handleOnEventDetails404 = () => {
    navigate(`/event`);
  };

  const handleTransparOnClose = () => {
    navigate(`/event/${eventId}`);
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
          onEventDetailsLoad={handleOnEventDetailsLoad}
          onEventDetailsNotFound={handleOnEventDetails404}
          onRiskParametersClicked={handleRiskParametersOnSelect}
          isRiskParametersSelected={hasParameters}
          onSelectedRiskParametersChanged={setSelectedRiskType}
          onClose={handleOnClose}
          isMinimized={eventDetailPanelIsMinimized}
          onMinimize={handleEventDetailMinimized}
          summaryTitle="My Events"
        />
      )}
      {isVisibleTRANSPAR && (
        <TransparencyPanel
          key={`TRANSPAR-${eventId}`}
          activePanel={activePanel}
          event={selectedEvent}
          riskType={selectedRiskType}
          onClose={handleTransparOnClose}
          eventId={eventId}
          geonameId={null}
        />
      )}
    </div>
  );
};

export default EventView;
