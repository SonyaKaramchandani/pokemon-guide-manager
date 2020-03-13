/** @jsx jsx */
import { navigate } from '@reach/router';
import { useBreakpointIndex } from '@theme-ui/match-media';
import * as dto from 'client/dto';
import constants from 'ga/constants';
import React, { useEffect, useState, useMemo, useCallback } from 'react';
import { jsx } from 'theme-ui';

import { notifyEvent } from 'utils/analytics';
import { isNonMobile } from 'utils/responsive';
import { getLocationFullName } from 'utils/stringFormatingHelpers';
import { useDependentState } from 'hooks/useDependentState';
import { parseIntOrNull } from 'utils/stringHelpers';

import esriMap from 'map';

import { EventDetailPanel } from '../EventDetailPanel';
import { DiseaseEventListPanel } from './DiseaseEventListPanel';
import { DiseaseListPanel } from './DiseaseListPanel';
import { LocationListPanel } from './LocationListPanel';
import { ActivePanel } from '../sidebar-types';

interface LocationViewProps {
  geonameId: string;
  diseaseId: string;
  eventId: string;
}

function showOutbreakExtent(eventsList: dto.GetEventModel[]) {
  const eventLocations = eventsList.reduce((a, b) => [...a, ...b.eventLocations], []);
  esriMap.showEventDetailView({ eventLocations });
}

const LocationView: React.FC<LocationViewProps> = ({
  geonameId: geonameIdParam,
  diseaseId: diseaseIdParam,
  eventId: eventIdParam
}) => {
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
  const [geonames, setGeonames] = useState<dto.GetGeonameModel[]>([]);
  const [diseases, setDiseases] = useState<dto.DiseaseRiskModel[]>([]);
  const [events, setEvents] = useState<dto.GetEventListModel>(null);
  const [locationFullName, setLocationFullName] = useState<string>(null);
  const [eventTitle, setEventTitle] = useState<string>(null);
  const [selectedDisease, setSelectedDisease] = useState<dto.DiseaseRiskModel>(null);

  // LESSON: do not mix props and states
  const geonameId = useDependentState(() => parseIntOrNull(geonameIdParam), [geonameIdParam]);
  const diseaseId = useDependentState(() => parseIntOrNull(diseaseIdParam), [diseaseIdParam]);
  const eventId = useDependentState(() => parseIntOrNull(eventIdParam), [eventIdParam]);
  const activePanel = useDependentState<ActivePanel>(
    () =>
      geonameId != null && diseaseId && eventId
        ? 'EventDetailPanel'
        : geonameId != null && diseaseId
        ? 'DiseaseEventListPanel'
        : geonameId != null
        ? 'DiseaseListPanel'
        : 'LocationListPanel',
    [geonameId, diseaseId, eventId]
  );
  const isVisibleDLP = useDependentState(() => !!(geonameId != null), [
    geonameId,
    diseaseId,
    eventId
  ]);
  const isVisibleDELP = useDependentState(() => !!(geonameId != null && diseaseId), [
    geonameId,
    diseaseId,
    eventId
  ]);
  const isVisibleEDP = useDependentState(() => !!(geonameId != null && diseaseId && eventId), [
    geonameId,
    diseaseId,
    eventId
  ]);
  const [isMinimizedLocationListPanel, setIsMinimizedLocationListPanel] = useState(false);
  const [isMinimizedDiseaseListPanel, setIsMinimizedDiseaseListPanel] = useState(false);
  const [isMinimizedDiseaseEventListPanel, setIsMinimizedDiseaseEventListPanel] = useState(false);
  const [isMinimizedEventDetailPanel, setIsMinimizedEventDetailPanel] = useState(false);

  useEffect(() => {
    if (activePanel === 'EventDetailPanel') {
      setIsMinimizedLocationListPanel(true);
      setIsMinimizedDiseaseListPanel(true);
    } else if (activePanel === 'DiseaseEventListPanel') {
      setIsMinimizedLocationListPanel(true);
      setIsMinimizedDiseaseListPanel(false);
    } else if (activePanel === 'DiseaseListPanel') {
      setIsMinimizedLocationListPanel(false);
      setIsMinimizedDiseaseListPanel(false);
    } else {
      setIsMinimizedLocationListPanel(false);
    }
  }, [activePanel]);

  useEffect(() => {
    const fullName = getLocationFullName(geonames, geonameId);
    setLocationFullName(fullName);
  }, [geonames, geonameId]);

  useEffect(() => {
    setSelectedDisease(
      (diseases && diseases.find(d => d.diseaseInformation.id === diseaseId)) || null
    );
  }, [diseases, diseaseId]);

  // TODO: 4d91fec5: should these 2 effects be identical?
  useEffect(() => {
    const eventList = (events && events.eventsList) || [];
    const selectedEvent = eventList.find(d => d.eventInformation.id === eventId);
    setEventTitle(selectedEvent && selectedEvent.eventInformation.title);
  }, [events, eventId, isNonMobileDevice]);

  const handleLocationListOnSelect = (geonameId: number, locationName: string) => {
    navigate(`/location/${geonameId}`);
    notifyEvent({
      action: constants.Action.OPEN_LOCATION_RISK_DETAILS,
      category: constants.Category.LOCATIONS,
      label: `Open from location panel: ${geonameId || '(none)'} | ${locationName || 'Global'}`,
      value: geonameId || null
    });
  };

  const handleDiseaseListOnSelect = (disease: dto.DiseaseRiskModel) => {
    const { id: diseaseId, name: diseaseName } = disease.diseaseInformation;
    navigate(`/location/${geonameId}/disease/${diseaseId}`);
    notifyEvent({
      action: constants.Action.OPEN_DISEASE_RISK_DETAILS,
      category: constants.Category.DISEASES,
      label: `Open from disease panel: ${diseaseId} | ${diseaseName}`,
      value: diseaseId
    });
  };
  const handleDiseaseEventListOnSelect = (eventId: number, title: string) => {
    navigate(`/location/${geonameId}/disease/${diseaseId}/event/${eventId}`);
    notifyEvent({
      action: constants.Action.OPEN_EVENT_DETAILS,
      category: constants.Category.EVENTS,
      label: `Open from list: ${eventId} | ${title}`,
      value: eventId
    });
  };

  const handleDiseaseListOnClose = () => {
    navigate(`/location`);
    isNonMobileDevice && esriMap.hideEventInfo();
    showOutbreakExtent([]);
  };

  const handleDiseaseEventListOnClose = () => {
    navigate(`/location/${geonameId}`);
    isNonMobileDevice && esriMap.showEventsView();
    showOutbreakExtent([]);
  };

  const handleEventDetailOnClose = () => {
    navigate(`/location/${geonameId}/disease/${diseaseId}`);
    isNonMobileDevice && showOutbreakExtent((events && events.eventsList) || []);
  };

  const handleGeonamesListLoad = (geonamesList: dto.GetGeonameModel[]) => {
    setGeonames(geonamesList);
  };

  const handleOnEventListLoad = (eventList: dto.GetEventListModel) => {
    setEvents(eventList);
    isNonMobileDevice && showOutbreakExtent(eventList.eventsList);
  };

  return (
    <div
      sx={{
        display: 'flex',
        height: '100%'
      }}
    >
      <LocationListPanel
        activePanel={activePanel}
        geonameId={geonameId}
        onGeonamesListLoad={handleGeonamesListLoad}
        onSelect={handleLocationListOnSelect}
        onSelectedGeonameDeleted={handleDiseaseListOnClose}
        isMinimized={isMinimizedLocationListPanel}
        onMinimize={flag => setIsMinimizedLocationListPanel(flag)}
        locationFullName={locationFullName || 'Loading...'}
      />

      {isVisibleDLP && (
        <DiseaseListPanel
          key={`DLP-${geonameId}`}
          activePanel={activePanel}
          geonameId={geonameId}
          diseaseId={diseaseId}
          onDiseasesLoad={setDiseases}
          onDiseaseSelect={handleDiseaseListOnSelect}
          onClose={handleDiseaseListOnClose}
          isMinimized={isMinimizedDiseaseListPanel}
          onMinimize={flag => setIsMinimizedDiseaseListPanel(flag)}
          summaryTitle="My Locations"
          locationFullName={locationFullName || 'Loading...'}
        />
      )}
      {isVisibleDELP && (
        <DiseaseEventListPanel
          key={`DELP-${diseaseId}`}
          activePanel={activePanel}
          geonameId={geonameId}
          diseaseId={diseaseId}
          eventId={eventId}
          disease={selectedDisease}
          onEventSelected={handleDiseaseEventListOnSelect}
          onEventListLoad={handleOnEventListLoad}
          onClose={handleDiseaseEventListOnClose}
          isMinimized={isMinimizedDiseaseEventListPanel}
          onMinimize={flag => setIsMinimizedDiseaseEventListPanel(flag)}
          summaryTitle="Diseases"
          locationFullName={locationFullName || 'Loading...'}
          closePanelOnEventsLoadError
        />
      )}
      {isVisibleEDP && (
        <EventDetailPanel
          key={`EDP-${eventId}`}
          activePanel={activePanel}
          eventId={eventId}
          eventTitleBackup={eventTitle || 'Loading...'}
          geonameId={geonameId}
          diseaseId={diseaseId}
          onClose={handleEventDetailOnClose}
          isMinimized={isMinimizedEventDetailPanel}
          onMinimize={flag => setIsMinimizedEventDetailPanel(flag)}
          summaryTitle={
            (selectedDisease &&
              selectedDisease.diseaseInformation &&
              selectedDisease.diseaseInformation.name) ||
            undefined
          }
          locationFullName={locationFullName || 'Loading...'}
        />
      )}
    </div>
  );
};

export default LocationView;
