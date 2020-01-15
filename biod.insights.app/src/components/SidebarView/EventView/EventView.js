import React, { useState } from 'react';
import { EventListPanel } from './EventListPanel';
import { EventDetailPanel } from '../EventDetailPanel';
import { useEffect } from 'react';
import LocationApi from 'api/LocationApi';
import aoiLayer from 'map/aoiLayer';
import esriMap from 'map';
import eventsView from 'map/events';

const EventView = props => {
  const [eventDetailPanelIsVisible, setEventDetailPanelIsVisible] = useState(false);
  const [eventId, setEventId] = useState(null);

  useEffect(() => {
    LocationApi.getUserLocations()
      .then(({ data: { geonames } }) => aoiLayer.renderAois(geonames));
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

  const handleOnEventListLoad = (data) => {
    eventsView.updateEventView(data.countryPins);
    esriMap.showEventsView(true);
  };

  return (
    <>
      <EventListPanel eventId={eventId} onSelect={handleOnSelect} onEventListLoad={handleOnEventListLoad} />
      {eventDetailPanelIsVisible && <EventDetailPanel eventId={eventId} onClose={handleOnClose} />}
    </>
  );
};

export default EventView;
