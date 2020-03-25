/** @jsx jsx */
import { navigate } from '@reach/router';
import { useNonMobileEffect } from 'hooks/useNonMobileEffect';
import orderBy from 'lodash.orderby';
import React, { useEffect, useState } from 'react';
import { useBreakpointIndex } from '@theme-ui/match-media';
import { jsx } from 'theme-ui';

import EventApi from 'api/EventApi';
import { IPanelProps } from 'components/Panel';
import esriMap from 'map';
import { Geoname } from 'utils/constants';
import { getPreferredMainPage } from 'utils/profile';
import * as dto from 'client/dto';

import EventDetailPanelDisplay from './EventDetailPanelDisplay';
import { ActivePanel } from '../sidebar-types';
import { isMobile } from 'utils/responsive';

type EventDetailPanelContainerProps = IPanelProps & {
  activePanel: ActivePanel;
  geonameId?: number;
  diseaseId?: number;
  eventId: number;
  eventTitleBackup: string;
  summaryTitle: string;
  locationFullName?: string;
};

const EventDetailPanelContainer: React.FC<EventDetailPanelContainerProps> = ({
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
  const [event, setEvent] = useState<dto.GetEventModel>({
    caseCounts: {},
    importationRisk: null,
    exportationRisk: null,
    eventInformation: {},
    eventLocations: [],
    sourceAirports: [],
    destinationAirports: [],
    articles: [],
    isLocal: true
  });
  const [isLoading, setIsLoading] = useState(false);
  const [hasError, setHasError] = useState(false);

  const handleZoomToLocation = () => {
    esriMap.setExtentToEventDetail();
  };

  const loadEvent = () => {
    setHasError(false);
    setIsLoading(true);
    EventApi.getEvent({
      eventId,
      diseaseId,
      ...(geonameId !== Geoname.GLOBAL_VIEW && { geonameId })
    })
      .then(({ data }) => {
        // eslint-disable-next-line no-param-reassign
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

  const isMobileDevice = isMobile(useBreakpointIndex());
  if (isMobileDevice && activePanel !== 'EventDetailPanel') {
    return null;
  }

  if (!eventId) {
    return null;
  }

  return (
    <EventDetailPanelDisplay
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