/** @jsx jsx */
import { navigate } from '@reach/router';
import { useBreakpointIndex } from '@theme-ui/match-media';
import * as dto from 'client/dto';
import constants from 'ga/constants';
import { useDependentState } from 'hooks/useDependentState';
import { useNonMobileEffect } from 'hooks/useNonMobileEffect';
import React, { useCallback, useEffect, useMemo, useState } from 'react';
import { jsx } from 'theme-ui';

import { notifyEvent } from 'utils/analytics';
import { Geoname } from 'utils/constants';
import { isNonMobile } from 'utils/responsive';
import { getLocationFullName } from 'utils/stringFormatingHelpers';
import { parseIntOrNull } from 'utils/stringHelpers';

import mapAoiLayer from 'map/aoiLayer';
import mapEventDetailView from 'map/eventDetails';
import mapEventsView from 'map/events';

import { IReachRoutePage } from 'components/_common/common-props';
import { RiskType, GetSelectedRisk } from 'components/RisksProjectionCard/RisksProjectionCard';

import { EventDetailPanel } from '../EventDetailPanel';
import { ActivePanel } from '../sidebar-types';
import { TransparencyPanel } from '../TransparencyPanel';
import { DiseaseEventListPanel } from './DiseaseEventListPanel';
import { DiseaseListPanel } from './DiseaseListPanel';
import { LocationListPanel } from './LocationListPanel';

type LocationViewProps = IReachRoutePage & {
  geonameId?: string;
  diseaseId?: string;
  eventId?: string;
  hasParameters?: boolean;
};

const LocationView: React.FC<LocationViewProps> = ({
  geonameId: geonameIdParam,
  diseaseId: diseaseIdParam,
  eventId: eventIdParam,
  hasParameters = false
}) => {
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
  const [geonames, setGeonames] = useState<dto.GetGeonameModel[]>(null);
  const [diseases, setDiseases] = useState<dto.DiseaseRiskModel[]>(null);
  const [eventPins, setEventPins] = useState<dto.EventsPinModel[]>(null);
  const [events, setEvents] = useState<dto.GetEventListModel>(null);
  const [locationFullName, setLocationFullName] = useState<string>(null);
  const [eventTitle, setEventTitle] = useState<string>(null);
  const [selectedDisease, setSelectedDisease] = useState<dto.DiseaseRiskModel>(null);
  const [selectedEvent, setSelectedEvent] = useState<dto.GetEventModel>(null);
  const [selectedRiskType, setSelectedRiskType] = useState<RiskType>(null);

  // LESSON: do not mix props and states
  const geonameId = useDependentState(() => parseIntOrNull(geonameIdParam), [geonameIdParam]);
  const diseaseId = useDependentState(() => parseIntOrNull(diseaseIdParam), [diseaseIdParam]);
  const eventId = useDependentState(() => parseIntOrNull(eventIdParam), [eventIdParam]);
  const activePanel = useDependentState<ActivePanel>(
    () =>
      geonameId != null && diseaseId && eventId && hasParameters
        ? 'ParametersPanel'
        : geonameId != null && diseaseId && eventId
        ? 'EventDetailPanel'
        : geonameId != null && diseaseId && hasParameters
        ? 'ParametersPanel'
        : geonameId != null && diseaseId
        ? 'DiseaseEventListPanel'
        : geonameId != null
        ? 'DiseaseListPanel'
        : 'LocationListPanel',
    [geonameId, diseaseId, eventId, hasParameters]
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
  const isVisibleTRANSPAR = useDependentState(
    () => !!(geonameId != null && diseaseId && eventId && hasParameters),
    [geonameId, diseaseId, eventId, hasParameters]
  );
  const [isMinimizedLocationListPanel, setIsMinimizedLocationListPanel] = useState(false);
  const [isMinimizedDiseaseListPanel, setIsMinimizedDiseaseListPanel] = useState(false);
  const [isMinimizedDiseaseEventListPanel, setIsMinimizedDiseaseEventListPanel] = useState(false);
  const [isMinimizedEventDetailPanel, setIsMinimizedEventDetailPanel] = useState(false);
  const [isMinimizedParametersPanel, setIsMinimizedParametersPanel] = useState(false);

  useEffect(() => {
    if (activePanel === 'ParametersPanel') {
      setIsMinimizedLocationListPanel(true);
      setIsMinimizedDiseaseListPanel(true);
      setIsMinimizedDiseaseEventListPanel(true);
    } else if (activePanel === 'EventDetailPanel') {
      setIsMinimizedLocationListPanel(true);
      setIsMinimizedDiseaseListPanel(true);
      setIsMinimizedDiseaseEventListPanel(false);
    } else if (activePanel === 'DiseaseEventListPanel') {
      setIsMinimizedLocationListPanel(true);
      setIsMinimizedDiseaseListPanel(false);
      setIsMinimizedDiseaseEventListPanel(false);
    } else {
      setIsMinimizedLocationListPanel(false);
      setIsMinimizedDiseaseListPanel(false);
      setIsMinimizedDiseaseEventListPanel(false);
    }
  }, [activePanel]);

  // delete stale data, if those panels are closed
  useEffect(() => {
    if (activePanel === 'LocationListPanel') {
      setEventPins([]);
      setEvents(null);
      setSelectedEvent(null);
    } else if (activePanel === 'DiseaseListPanel') {
      setEvents(null);
      setSelectedEvent(null);
    } else if (activePanel === 'DiseaseEventListPanel') {
      setSelectedEvent(null);
    }
  }, [activePanel]);

  useNonMobileEffect(() => {
    if (geonameId === Geoname.GLOBAL_VIEW) {
      mapAoiLayer.renderAois([]); // clear user AOIs when global view is selected
    } else if (geonameId !== null) {
      mapAoiLayer.renderAois([{ geonameId: geonameId }]); // only selected user AOI
    } else {
      mapAoiLayer.renderAois(geonames || []); // display all user AOIs when no location is selected
    }
  }, [geonames, geonameId]);

  useNonMobileEffect(() => {
    if (activePanel === 'DiseaseListPanel') {
      if (eventPins) {
        mapEventsView.updateEventView(eventPins, geonameId);
        mapEventsView.show();
      }
    } else {
      mapEventsView.hide();
    }
  }, [activePanel, eventPins]);

  useNonMobileEffect(() => {
    if (activePanel === 'DiseaseEventListPanel') {
      if (events) {
        const eventsList = events.eventsList || [];
        const eventLocations = eventsList.reduce((a, b) => [...a, ...b.eventLocations], []);
        mapEventDetailView.show({ eventLocations } as any); // TODO: PT-1200
      }
    } else if (activePanel === 'EventDetailPanel' || activePanel === 'ParametersPanel') {
      if (selectedEvent) {
        mapEventDetailView.show(selectedEvent as any); // TODO: PT-1200
      }
    } else {
      mapEventDetailView.hide();
    }
  }, [activePanel, geonameId, events, selectedEvent]);

  useEffect(() => {
    if (geonameId === Geoname.GLOBAL_VIEW) {
      setLocationFullName('Global View');
    } else if (geonameId !== null && geonames) {
      const selectedGeoname = geonames.find(g => g.geonameId === geonameId);
      if (selectedGeoname) setLocationFullName(getLocationFullName(selectedGeoname));
      else navigate(`/location`); // DESIGN: PT-1191: when URL ids are not in preceeding panel, go to default dashboard
    } else {
      setLocationFullName(null);
    }
  }, [geonames, geonameId]);

  useEffect(() => {
    if (diseases && diseaseId) {
      const targetDesease = diseases.find(d => d.diseaseInformation.id === diseaseId);
      if (targetDesease) setSelectedDisease(targetDesease);
      else navigate(`/location`); // DESIGN: PT-1191: when URL ids are not in preceeding panel, go to default dashboard
    }
  }, [diseases, diseaseId]);

  // TODO: 4d91fec5: should these 2 effects be identical?
  useEffect(() => {
    if (events && eventId) {
      const eventList = (events && events.eventsList) || [];
      const selectedEvent = eventList.find(d => d.eventInformation.id === eventId);
      if (selectedEvent) setEventTitle(selectedEvent.eventInformation.title);
      else navigate(`/location`); // DESIGN: PT-1191: when URL ids are not in preceeding panel, go to default dashboard
    }
  }, [events, eventId]);

  useEffect(() => {
    if (hasParameters && selectedRiskType && selectedEvent) {
      const selectedRisk = GetSelectedRisk(selectedEvent, selectedRiskType);
      if (selectedRisk && selectedRisk.isModelNotRun) {
        // NOTE: 4c87a49b: this means URL is invalid. The parameters panel was opened via URL for "Not Calculated" case
        navigate('/location');
      }
    }
  }, [hasParameters, selectedRiskType, selectedEvent]);

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
  const handleRiskParametersOnSelectEDP = () => {
    navigate(`/location/${geonameId}/disease/${diseaseId}/event/${eventId}/parameters`);
  };

  const handleDiseaseListOnClose = () => {
    navigate(`/location`);
  };

  const handleDiseaseEventListOnClose = () => {
    navigate(`/location/${geonameId}`);
  };

  const handleEventDetailOnClose = () => {
    navigate(`/location/${geonameId}/disease/${diseaseId}`);
  };

  const handleTransparOnClose = () => {
    eventId
      ? navigate(`/location/${geonameId}/disease/${diseaseId}/event/${eventId}`)
      : navigate(`/location/${geonameId}/disease/${diseaseId}`);
  };

  return (
    <div
      sx={{
        display: 'flex',
        height: '100%',
        maxWidth: ['100%', 'calc(100vw - 200px)']
      }}
    >
      <LocationListPanel
        activePanel={activePanel}
        geonameId={geonameId}
        onGeonamesListLoad={setGeonames}
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
          onEventPinsLoad={setEventPins}
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
          onEventListLoad={setEvents}
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
          onEventDetailsLoad={setSelectedEvent}
          onEventDetailsNotFound={() => navigate(`/location`)}
          onRiskParametersClicked={handleRiskParametersOnSelectEDP}
          isRiskParametersSelected={hasParameters}
          onSelectedRiskParametersChanged={setSelectedRiskType}
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
      {isVisibleTRANSPAR && (
        <TransparencyPanel
          key={`TRANSPAR-${eventId}`}
          activePanel={activePanel}
          event={selectedEvent}
          riskType={selectedRiskType}
          onClose={handleTransparOnClose}
          isMinimized={isMinimizedParametersPanel}
          onMinimize={flag => setIsMinimizedParametersPanel(flag)}
          eventId={eventId}
          geonameId={geonameId}
          locationFullName={locationFullName}
        />
      )}
    </div>
  );
};

export default LocationView;
