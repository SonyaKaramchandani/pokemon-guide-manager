/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import * as dto from 'client/dto';
import orderBy from 'lodash.orderby';
import React, { useContext, useEffect, useState } from 'react';
import { jsx } from 'theme-ui';

import { AppStateContext } from 'api/AppStateContext';
import EventApi from 'api/EventApi';
import locationApi from 'api/LocationApi';
import { RiskDirectionType } from 'models/RiskCategories';
import { Geoname } from 'utils/constants';
import { MapShapesToProximalMapShapes } from 'utils/modelHelpers';
import { isMobile } from 'utils/responsive';

import esriMap from 'map';

import { IPanelProps } from 'components/Panel';

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
  onSelectedRiskTypeChanged: (val: RiskDirectionType) => void;
  seedRiskDirectionType?: RiskDirectionType;
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
  onSelectedRiskTypeChanged,
  seedRiskDirectionType = 'importation',
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
  const [activeRiskType, setActiveRiskType] = useState<RiskDirectionType>(seedRiskDirectionType);
  const { appState, amendState } = useContext(AppStateContext);

  useEffect(() => {
    onSelectedRiskTypeChanged && onSelectedRiskTypeChanged(activeRiskType);
  }, [activeRiskType]);

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

  useEffect(() => {
    if (!event || !event.proximalLocations) return;
    locationApi
      .getGeonameShapes(
        event.proximalLocations.map(e => e.locationId),
        false
      )
      .then(({ data }) => {
        const proximalShapes = MapShapesToProximalMapShapes(data, event.proximalLocations);
        amendState({
          proximalGeonameShapes: proximalShapes
        });
      });
  }, [event && event.proximalLocations]);

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
      selectedRiskType={activeRiskType}
      onSelectedRiskTypeChanged={setActiveRiskType}
    />
  );
};

export default EventDetailPanelContainer;
