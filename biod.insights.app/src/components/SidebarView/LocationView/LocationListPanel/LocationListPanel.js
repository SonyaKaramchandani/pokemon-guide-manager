import React, { useState, useEffect, useCallback, useReducer } from 'react';
import LocationApi from 'api/LocationApi';
import { Panel } from 'components/Panel';
import { UserAddLocation } from 'components/UserAddLocation';
import { List } from 'semantic-ui-react';
import LocationListItem from './LocationListItem';
import { SortBy } from 'components/Panel/SortBy';
import { SortOptions } from './SortByOptions';

const initialState = {
  isLoading: false,
  sortBy: SortOptions[0].value,
  geonames: []
};

const FETCH_LOCATIONS_START = 'FETCH_LOCATIONS_START';
const FETCH_LOCATIONS_DONE = 'FETCH_LOCATIONS_DONE';
const SET_LOCATIONS = 'SET_LOCATIONS';
const SORT_BY = 'SORT_BY';

function reducer(state, action) {
  switch (action.type) {
    case FETCH_LOCATIONS_START:
      return {
        ...state,
        isLoading: true
      };
    case FETCH_LOCATIONS_DONE:
      return {
        ...state,
        isLoading: false
      };
    case SET_LOCATIONS:
      return {
        ...state,
        geonames: action.payload.geonames
      };
    case SORT_BY:
      return {
        ...state,
        sortBy: action.payload.sortBy
      };
  }
}

function LocationListPanel({ geonameId, onSelect }) {
  const [state, dispatch] = useReducer(reducer, initialState);

  const handleOnDelete = ({ data: { geonames } }) => {
    dispatch({ type: SET_LOCATIONS, payload: { geonames } });
  };

  const handleOnAdd = ({ data: { geonames } }) => {
    dispatch({ type: SET_LOCATIONS, payload: { geonames } });
  };

  const handleOnSortBy = sortBy => {
    dispatch({ type: SORT_BY, payload: { sortBy } });
  };

  const { isLoading, geonames, sortBy } = state;
  const canDelete = geonames.length > 1;

  useEffect(() => {
    dispatch({ type: FETCH_LOCATIONS_START });
    LocationApi.getUserLocations()
      .then(({ data: { geonames } }) => {
        dispatch({ type: SET_LOCATIONS, payload: { geonames } });
      })
      .finally(() => {
        dispatch({ type: FETCH_LOCATIONS_DONE });
      });
  }, []);

  return (
    <Panel
      loading={isLoading}
      header="My Locations"
      toolbar={
        <SortBy
          defaultValue={sortBy}
          options={SortOptions}
          onSelect={handleOnSortBy}
          disabled={isLoading}
        />
      }
    >
      <UserAddLocation onAdd={handleOnAdd} />
      <List divided relaxed selection>
        <LocationListItem
          selected={geonameId}
          key={0}
          name="No Location"
          country="Nowhere"
          canDelete={false}
          onSelect={() => onSelect(0)}
        />
        {geonames.map(geoname => (
          <LocationListItem
            selected={geonameId}
            key={geoname.geonameId}
            {...geoname}
            canDelete={canDelete}
            onSelect={onSelect}
            onDelete={handleOnDelete}
          />
        ))}
      </List>
    </Panel>
  );
}

export default LocationListPanel;
