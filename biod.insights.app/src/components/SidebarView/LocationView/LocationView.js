import React, { useReducer } from 'react';
import { LocationListPanel } from './LocationListPanel';
import { DiseaseListPanel } from './DiseaseListPanel';
import { DiseaseEventListPanel } from './DiseaseEventListPanel';
import { EventDetailPanel } from '../EventDetailPanel';

const initialState = {
  geonameId: null,
  diseaseId: null,
  disease: null,
  eventId: null,
  isDiseaseListPanelVisible: false,
  isDiseaseEventListPanelVisible: false,
  isEventDetailPanelVisible: false
};

const LOCATION_SELECTED = 'LOCATION_SELECTED';
const DISEASE_LIST_PANEL_CLOSED = 'DISEASE_LIST_PANEL_CLOSED';
const DISEASE_SELECTED = 'DISEASE_SELECTED';
const DISEASE_EVENT_LIST_PANEL_CLOSED = 'DISEASE_EVENT_LIST_PANEL_CLOSED';
const EVENT_SELECTED = 'EVENT_SELECTED';
const EVENT_DETAIL_PANEL_CLOSED = 'EVENT_DETAIL_PANEL_CLOSED';

function reducer(state, action) {
  switch (action.type) {
    case LOCATION_SELECTED:
      return {
        ...state,
        geonameId: action.payload.geonameId,
        isDiseaseListPanelVisible: true,
        isDiseaseEventListPanelVisible: false,
        isEventDetailPanelVisible: false
      };
    case DISEASE_SELECTED:
      return {
        ...state,
        diseaseId: action.payload.diseaseId,
        disease: action.payload.disease,
        isDiseaseEventListPanelVisible: true,
        isEventDetailPanelVisible: false
      };
    case EVENT_SELECTED:
      return { ...state, eventId: action.payload.eventId, isEventDetailPanelVisible: true };
    case DISEASE_LIST_PANEL_CLOSED:
      return {
        ...state,
        isDiseaseListPanelVisible: false,
        isDiseaseEventListPanelVisible: false,
        isEventDetailPanelVisible: false
      };
    case DISEASE_EVENT_LIST_PANEL_CLOSED:
      return { ...state, isDiseaseEventListPanelVisible: false, isEventDetailPanelVisible: false };
    case EVENT_DETAIL_PANEL_CLOSED:
      return { ...state, isEventDetailPanelVisible: false };
    default:
      return state;
  }
}

function LocationView({ onViewChange }) {
  const [state, dispatch] = useReducer(reducer, initialState);

  function handleLocationListOnSelect(geonameId) {
    dispatch({ type: LOCATION_SELECTED, payload: { geonameId } });
  }

  function handleDiseaseListOnSelect(diseaseId, disease) {
    dispatch({ type: DISEASE_SELECTED, payload: { diseaseId, disease } });
  }

  function handleDiseaseEventListOnSelect(eventId) {
    dispatch({ type: EVENT_SELECTED, payload: { eventId } });
  }

  function handleDiseaseListOnClose() {
    dispatch({ type: DISEASE_LIST_PANEL_CLOSED });
  }

  function handleDiseaseEventListOnClose() {
    dispatch({ type: DISEASE_EVENT_LIST_PANEL_CLOSED });
  }

  function handleEventDetailOnClose() {
    dispatch({ type: EVENT_DETAIL_PANEL_CLOSED });
  }

  return (
    <>
      <LocationListPanel
        geonameId={state.geonameId}
        onViewChange={onViewChange}
        onSelect={handleLocationListOnSelect}
      />
      {state.isDiseaseListPanelVisible && (
        <DiseaseListPanel
          geonameId={state.geonameId}
          diseaseId={state.diseaseId}
          onSelect={handleDiseaseListOnSelect}
          onClose={handleDiseaseListOnClose}
        />
      )}
      {state.isDiseaseEventListPanelVisible && (
        <DiseaseEventListPanel
          geonameId={state.geonameId}
          diseaseId={state.diseaseId}
          eventId={state.eventId}
          disease={state.disease}
          onSelect={handleDiseaseEventListOnSelect}
          onClose={handleDiseaseEventListOnClose}
        />
      )}
      {state.isEventDetailPanelVisible && (
        <EventDetailPanel
          eventId={state.eventId}
          geonameId={state.geonameId}
          diseaseId={state.diseaseId}
          onClose={handleEventDetailOnClose}
        />
      )}
    </>
  );
}

export default LocationView;
