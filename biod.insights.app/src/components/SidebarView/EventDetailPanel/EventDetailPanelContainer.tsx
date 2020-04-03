/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import * as dto from 'client/dto';
import orderBy from 'lodash.orderby';
import React, { useEffect, useState } from 'react';
import { jsx } from 'theme-ui';

import EventApi from 'api/EventApi';
import { Geoname } from 'utils/constants';
import { isMobile } from 'utils/responsive';

import esriMap from 'map';

import { IPanelProps } from 'components/Panel';
import { RiskType } from 'components/RisksProjectionCard/RisksProjectionCard';

import { ActivePanel } from '../sidebar-types';
import EventDetailPanelDisplay from './EventDetailPanelDisplay';

type EventDetailPanelContainerProps = IPanelProps & {
  activePanel: ActivePanel;
  geonameId?: number;
  diseaseId?: number;
  eventId: number;
  eventTitleBackup: string;
  onEventDetailsLoad: (val: dto.GetEventModel) => void;
  onEventDetailsNotFound: () => void;
  onRiskParametersClicked: () => void;
  isRiskParametersSelected: boolean;
  onSelectedRiskParametersChanged: (val: RiskType) => void;
  summaryTitle: string;
  locationFullName?: string;
};

const EventDetailPanelContainer: React.FC<EventDetailPanelContainerProps> = ({
  activePanel,
  geonameId,
  diseaseId,
  eventId,
  eventTitleBackup,
  onEventDetailsLoad,
  onEventDetailsNotFound,
  onRiskParametersClicked,
  isRiskParametersSelected,
  onSelectedRiskParametersChanged,
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
    airports: {
      sourceAirports: [],
      destinationAirports: []
    },
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
        onEventDetailsLoad(data);
      })
      .catch(e => {
        setHasError(true);
        if (e.response && (e.response.status == 404 || e.response.status == 400)) {
          onEventDetailsNotFound();
        }
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    if (eventId) {
      loadEvent();
    }
  }, [eventId]);

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
      onRiskParametersClicked={onRiskParametersClicked}
      isRiskParametersSelected={isRiskParametersSelected}
      onSelectedRiskParametersChanged={onSelectedRiskParametersChanged}
    />
  );
};

export default EventDetailPanelContainer;
