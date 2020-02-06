/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useReducer, useState } from 'react';
import { LocationListPanel } from './LocationListPanel';
import { DiseaseListPanel } from './DiseaseListPanel';
import { DiseaseEventListPanel } from './DiseaseEventListPanel';
import { EventDetailPanel } from '../EventDetailPanel';
import esriMap from 'map';
import { notifyEvent } from 'utils/analytics';
import constants from 'ga/constants';

const initialState = {
  geonameId: null,
  diseaseId: null,
  disease: null,
  eventId: null,
  isDiseaseListPanelVisible: false,
  isDiseaseEventListPanelVisible: false,
  isEventDetailPanelVisible: false,
  isLocationListPanelMinimized: false,
  isDiseaseListPanelMinimized: false,
  isDiseaseEventListPanelMinimized: false,
  isEventDetailPanelMinimized: false
};

const LOCATION_SELECTED = 'LOCATION_SELECTED';
const LOCATION_CLEARED = 'LOCATION_CLEARED';
const DISEASE_LIST_PANEL_CLOSED = 'DISEASE_LIST_PANEL_CLOSED';
const DISEASE_SELECTED = 'DISEASE_SELECTED';
const DISEASE_EVENT_LIST_PANEL_CLOSED = 'DISEASE_EVENT_LIST_PANEL_CLOSED';
const EVENT_SELECTED = 'EVENT_SELECTED';
const EVENT_DETAIL_PANEL_CLOSED = 'EVENT_DETAIL_PANEL_CLOSED';
const LOCATION_LIST_PANEL_MINIMIZED = 'LOCATION_LIST_PANEL_MINIMIZED';
const DISEASE_LIST_PANEL_MINIMIZED = 'DISEASE_LIST_PANEL_MINIMIZED';
const DISEASE_EVENT_LIST_PANEL_MINIMIZED = 'DISEASE_EVENT_LIST_PANEL_MINIMIZED';
const EVENT_DETAIL_PANEL_MINIMIZED = 'EVENT_DETAIL_PANEL_MINIMIZED';

const reducer = (state, action) => {
  switch (action.type) {
    case LOCATION_SELECTED:
      return {
        ...state,
        geonameId: action.payload.geonameId,
        isDiseaseListPanelVisible: !!action.payload.geonameId,
        isDiseaseEventListPanelVisible: false,
        isDiseaseListPanelMinimized: false,
        isEventDetailPanelVisible: false,
        diseaseId: null,
        disease: null,
        eventId: null
      };
    case DISEASE_SELECTED:
      return {
        ...state,
        diseaseId: action.payload.diseaseId,
        disease: action.payload.disease,
        eventId: null,
        isDiseaseEventListPanelVisible: true,
        isEventDetailPanelVisible: false,
        isLocationListPanelMinimized: true
      };
    case EVENT_SELECTED:
      return {
        ...state,
        eventId: action.payload.eventId,
        isEventDetailPanelVisible: true,
        isLocationListPanelMinimized: true,
        isDiseaseListPanelMinimized: true
      };
    case LOCATION_CLEARED:
    case DISEASE_LIST_PANEL_CLOSED:
      return {
        ...state,
        isDiseaseListPanelVisible: false,
        isDiseaseEventListPanelVisible: false,
        isEventDetailPanelVisible: false,
        geonameId: null,
        diseaseId: null,
        disease: null,
        eventId: null
      };
    case DISEASE_EVENT_LIST_PANEL_CLOSED:
      return {
        ...state,
        isDiseaseEventListPanelVisible: false,
        isEventDetailPanelVisible: false,
        diseaseId: null,
        disease: null,
        eventId: null
      };
    case EVENT_DETAIL_PANEL_CLOSED:
      return {
        ...state,
        isEventDetailPanelVisible: false,
        eventId: null
      };
    case LOCATION_LIST_PANEL_MINIMIZED:
      return {
        ...state,
        isLocationListPanelMinimized: action.payload
      };
    case DISEASE_LIST_PANEL_MINIMIZED:
      return {
        ...state,
        isDiseaseListPanelMinimized: action.payload
      };
    case DISEASE_EVENT_LIST_PANEL_MINIMIZED:
      return {
        ...state,
        isDiseaseEventListPanelMinimized: action.payload
      };
    case EVENT_DETAIL_PANEL_MINIMIZED:
      return {
        ...state,
        isEventDetailPanelMinimized: action.payload
      };
    default:
      return state;
  }
};

const LocationView = ({ onViewChange }) => {
  const [state, dispatch] = useReducer(reducer, initialState);
  const [events, setEvents] = useState([]);

  const handleLocationListOnSelect = (geonameId, locationName) => {
    dispatch({ type: LOCATION_SELECTED, payload: { geonameId } });
    notifyEvent({
      action: constants.Action.OPEN_LOCATION_RISK_DETAILS,
      category: constants.Category.LOCATIONS,
      label: `Open from location panel: ${geonameId || '(none)'} | ${locationName || 'Global'}`,
      value: geonameId || null
    });
  };

  const handleDiseaseListOnSelect = (diseaseId, disease) => {
    dispatch({ type: DISEASE_SELECTED, payload: { diseaseId, disease } });
    notifyEvent({
      action: constants.Action.OPEN_DISEASE_RISK_DETAILS,
      category: constants.Category.DISEASES,
      label: `Open from disease panel: ${diseaseId} | ${disease.diseaseInformation.name}`,
      value: diseaseId
    });
  };

  const handleDiseaseEventListOnSelect = (eventId, title) => {
    dispatch({ type: EVENT_SELECTED, payload: { eventId } });
    notifyEvent({
      action: constants.Action.OPEN_EVENT_DETAILS,
      category: constants.Category.EVENTS,
      label: `Open from list: ${eventId} | ${title}`,
      value: eventId
    });
  };

  const handleDiseaseListOnClose = () => {
    dispatch({ type: DISEASE_LIST_PANEL_CLOSED });
    esriMap.hideEventInfo();
  };

  const handleDiseaseEventListOnClose = () => {
    dispatch({ type: DISEASE_EVENT_LIST_PANEL_CLOSED });
    esriMap.showEventsView();
  };

  const handleEventDetailOnClose = () => {
    dispatch({ type: EVENT_DETAIL_PANEL_CLOSED });
    showOutbreakExtent(events);
  };

  const handleOnEventListLoad = ({ eventsList }) => {
    setEvents(eventsList);
    showOutbreakExtent(eventsList);
  };

  const handleLocationListOnMinimize = value => {
    dispatch({ type: LOCATION_LIST_PANEL_MINIMIZED, payload: value });
  };

  const handleDiseaseListOnMinimize = value => {
    dispatch({ type: DISEASE_LIST_PANEL_MINIMIZED, payload: value });
  };

  const handleDiseaseEventListOnMinimize = value => {
    dispatch({ type: DISEASE_EVENT_LIST_PANEL_MINIMIZED, payload: value });
  };

  const handleEventDetailOnMinimize = value => {
    dispatch({ type: EVENT_DETAIL_PANEL_MINIMIZED, payload: value });
  };

  const handleLocationListOnClear = () => {
    dispatch({ type: LOCATION_CLEARED });
  };

  const showOutbreakExtent = eventsList => {
    const eventLocations = eventsList.reduce((a, b) => [...a, ...b.eventLocations], []);
    esriMap.showEventDetailView({ eventLocations });
  };

  return (
    <div
      sx={{
        display: 'flex',
        overflowX: 'auto',
        maxWidth: 'calc(100vw - 200px)'
      }}
    >
      <LocationListPanel
        geonameId={state.geonameId}
        onViewChange={onViewChange}
        onSelect={handleLocationListOnSelect}
        onClear={handleLocationListOnClear}
        isMinimized={state.isLocationListPanelMinimized}
        onMinimize={handleLocationListOnMinimize}
      />
      {state.isDiseaseListPanelVisible && (
        <DiseaseListPanel
          key={state.geonameId}
          geonameId={state.geonameId}
          diseaseId={state.diseaseId}
          onSelect={handleDiseaseListOnSelect}
          onClose={handleDiseaseListOnClose}
          isMinimized={state.isDiseaseListPanelMinimized}
          onMinimize={handleDiseaseListOnMinimize}
        />
      )}
      {state.isDiseaseEventListPanelVisible && (
        <DiseaseEventListPanel
          key={state.diseaseId}
          geonameId={state.geonameId}
          diseaseId={state.diseaseId}
          eventId={state.eventId}
          disease={state.disease}
          onSelect={handleDiseaseEventListOnSelect}
          onClose={handleDiseaseEventListOnClose}
          onEventListLoad={handleOnEventListLoad}
          isMinimized={state.isDiseaseEventListPanelMinimized}
          onMinimize={handleDiseaseEventListOnMinimize}
        />
      )}
      {state.isEventDetailPanelVisible && (
        <EventDetailPanel
          key={state.eventId}
          eventId={state.eventId}
          geonameId={state.geonameId}
          diseaseId={state.diseaseId}
          onClose={handleEventDetailOnClose}
          isMinimized={state.isEventDetailPanelMinimized}
          onMinimize={handleEventDetailOnMinimize}
        />
      )}
    </div>
  );
};

export default LocationView;
