/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import EventApi from 'api/EventApi';
import EventDetailPanel from './EventDetailPanel';
import { Geoname } from 'utils/constants';

const defaultValue = {
  caseCounts: {},
  importationRisk: null,
  exportationRisk: null,
  eventInformation: {},
  eventLocations: [],
  sourceAirports: [],
  destinationAirports: [],
  articles: [],
  isLocal: true
};

const EventDetailPanelContainer = ({
  geonameId,
  diseaseId,
  eventId,
  isMinimized,
  onMinimize,
  onClose
}) => {
  const [event, setEvent] = useState(defaultValue);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    if (eventId) {
      setIsLoading(true);
      EventApi.getEvent(geonameId === Geoname.GLOBAL_VIEW ? { eventId, diseaseId } : { eventId, diseaseId, geonameId })
        .then(({ data }) => {
          setEvent(data);
        })
        .finally(() => setIsLoading(false));
    }
  }, [eventId]);

  if (!eventId) {
    return null;
  }

  return (
    <EventDetailPanel
      isLoading={isLoading}
      event={event}
      onClose={onClose}
      isMinimized={isMinimized}
      onMinimize={onMinimize}
    />
  );
};

export default EventDetailPanelContainer;
