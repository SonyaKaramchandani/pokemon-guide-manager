import React, { useState, useReducer } from 'react';
import { LocationListPanel } from './LocationListPanel';
import { DiseaseListPanel } from './DiseaseListPanel';
import { EventListPanel } from './EventListPanel';
import { EventDetailPanel } from '../EventDetailPanel';

const initialState = {
  geonameId: '',
  diseaseId: '',
  eventId: '',
  isDiseaseListPanelVisible: false,
  isEventListPanelVisible: false,
  isEventDetailPanelVisible: false
};

const LOCATION_SELECTED = 'LOCATION_SELECTED';
const DISEASE_LIST_PANEL_CLOSED = 'DISEASE_LIST_PANEL_CLOSED';
const DISEASE_SELECTED = 'DISEASE_SELECTED';
const EVENT_LIST_PANEL_CLOSED = 'EVENT_LIST_PANEL_CLOSED';
const EVENT_SELECTED = 'EVENT_SELECTED';
const EVENT_DETAIL_PANEL_CLOSED = 'EVENT_DETAIL_PANEL_CLOSED';

function reducer(state, action) {
  switch (action.type) {
    case LOCATION_SELECTED:
      return {
        ...state,
        geonameId: action.payload.geonameId,
        isDiseaseListPanelVisible: true,
        isEventListPanelVisible: false,
        isEventDetailPanelVisible: false
      };
    case DISEASE_SELECTED:
      return {
        ...state,
        diseaseId: action.payload.diseaseId,
        isEventListPanelVisible: true,
        isEventDetailPanelVisible: false
      };
    case EVENT_SELECTED:
      return { ...state, eventId: action.payload.eventId, isEventDetailPanelVisible: true };
    case DISEASE_LIST_PANEL_CLOSED:
      return {
        ...state,
        isDiseaseListPanelVisible: false,
        isEventListPanelVisible: false,
        isEventDetailPanelVisible: false
      };
    case EVENT_LIST_PANEL_CLOSED:
      return { ...state, isEventListPanelVisible: false, isEventDetailPanelVisible: false };
    case EVENT_DETAIL_PANEL_CLOSED:
      return { ...state, isEventDetailPanelVisible: false };
  }
}

function LocationView({ onViewChange }) {
  const [state, dispatch] = useReducer(reducer, initialState);

  function handleLocationListOnSelect(geonameId) {
    dispatch({ type: LOCATION_SELECTED, payload: { geonameId } });
  }

  function handleDiseaseListOnSelect(diseaseId) {
    dispatch({ type: DISEASE_SELECTED, payload: { diseaseId } });
  }

  function handleEventListOnSelect(eventId) {
    dispatch({ type: EVENT_SELECTED, payload: { eventId } });
  }

  function handleDiseaseListOnClose() {
    dispatch({ type: DISEASE_LIST_PANEL_CLOSED });
  }

  function handleEventListOnClose() {
    dispatch({ type: EVENT_LIST_PANEL_CLOSED });
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
          onSelect={handleDiseaseListOnSelect}
          onClose={handleDiseaseListOnClose}
        />
      )}
      {state.isEventListPanelVisible && (
        <EventListPanel
          geonameId={state.geonameId}
          diseaseId={state.diseaseId}
          onSelect={handleEventListOnSelect}
          onClose={handleEventListOnClose}
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
