import React, { useState, useReducer } from 'react';
import SidebarLocationViewLocationListPanel from './SidebarLocationViewLocationListPanel';
import SidebarLocationViewDiseaseListPanel from './SidebarLocationViewDiseaseListPanel';
import SidebarLocationViewEventListPanel from './SidebarLocationViewEventListPanel';
import SidebarEventViewEventDetailPanel from './SidebarEventViewEventDetailPanel';

const initialState = {
  locationId: '',
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
        locationId: action.payload.locationId,
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

function SidebarLocationView({ onViewChange }) {
  const [state, dispatch] = useReducer(reducer, initialState);

  function handleLocationListOnSelect(locationId) {
    dispatch({ type: LOCATION_SELECTED, payload: { locationId } });
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
      <SidebarLocationViewLocationListPanel
        onViewChange={onViewChange}
        onSelect={handleLocationListOnSelect}
      />
      {state.isDiseaseListPanelVisible && (
        <SidebarLocationViewDiseaseListPanel
          locationId={state.locationId}
          onSelect={handleDiseaseListOnSelect}
          onClose={handleDiseaseListOnClose}
        />
      )}
      {state.isEventListPanelVisible && (
        <SidebarLocationViewEventListPanel
          locationId={state.locationId}
          diseaseId={state.diseaseId}
          onSelect={handleEventListOnSelect}
          onClose={handleEventListOnClose}
        />
      )}
      {state.isEventDetailPanelVisible && (
        <SidebarEventViewEventDetailPanel
          eventId={state.eventId}
          locationId={state.locationId}
          diseaseId={state.diseaseId}
          onClose={handleEventDetailOnClose}
        />
      )}
    </>
  );
}

export default SidebarLocationView;
