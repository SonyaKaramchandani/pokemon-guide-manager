/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import esriMap from 'map';
import EventApi from 'api/EventApi';
import EventDetailPanelDisplay from './EventDetailPanelDisplay';
import { Geoname } from 'utils/constants';
import orderBy from 'lodash.orderby';
import { navigate } from '@reach/router';
import { getPreferredMainPage } from 'utils/profile';
import { useNonMobileEffect } from 'hooks/useNonMobileEffect';

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
  activePanel,
  geonameId,
  diseaseId,
  eventId,
  eventTitleBackup,
  isMinimized,
  onMinimize,
  summaryTitle,
  locationFullName,
  onClose
}) => {
  const [event, setEvent] = useState(defaultValue);
  const [isLoading, setIsLoading] = useState(false);
  const [hasError, setHasError] = useState(false);

  const handleZoomToLocation = () => {
    esriMap.setExtentToEventDetail(event);
  };

  const loadEvent = () => {
    setHasError(false);
    setIsLoading(true);
    EventApi.getEvent(
      geonameId === Geoname.GLOBAL_VIEW ? { eventId, diseaseId } : { eventId, diseaseId, geonameId }
    )
      .then(({ data }) => {
        data.articles = orderBy(data.articles, ['publishedDate'], 'desc');
        setEvent(data);
      })
      .catch(e => {
        setHasError(true);
        if (e.response && (e.response.status == 404 || e.response.status == 400)) {
          navigate(getPreferredMainPage());
        }
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    if (eventId) {
      loadEvent();
    }
  }, [eventId]);

  useNonMobileEffect(() => {
    if (event && event.eventLocations && event.eventLocations.length) {
      esriMap.showEventDetailView(event);
    }
  }, [event]);

  if (!eventId) {
    return null;
  }

  return (
    <EventDetailPanelDisplay
      activePanel={activePanel}
      isLoading={isLoading}
      summaryTitle={summaryTitle}
      locationFullName={locationFullName}
      event={event}
      eventTitleBackup={eventTitleBackup}
      hasError={hasError}
      onClose={onClose}
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      onZoomToLocation={handleZoomToLocation}
      handleRetryOnClick={loadEvent}
    />
  );
};

export default EventDetailPanelContainer;
