import React, { useState, useEffect, useCallback, useReducer } from 'react';
import LocationApi from 'api/LocationApi';
import { Panel } from 'components/Panel';
import { UserAddLocation } from 'components/UserAddLocation';
import { List } from 'semantic-ui-react';
import LocationListItem from './LocationListItem';
import { SortBy } from 'components/Panel/SortBy';
import { LocationListSortOptions as sortOptions, sort } from 'components/SidebarView/SortByOptions';

function LocationListPanel({ geonameId, onSelect }) {
  const [geonames, setGeonames] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [sortBy, setSortBy] = useState(sortOptions[0].value);

  const handleOnDelete = ({ data: { geonames } }) => {
    setGeonames(geonames);
  };

  const handleOnAdd = ({ data: { geonames } }) => {
    setGeonames(geonames);
  };

  const canDelete = geonames.length > 1;

  useEffect(() => {
    setIsLoading(true);
    LocationApi.getUserLocations()
      .then(({ data: { geonames } }) => {
        setGeonames(geonames);
      })
      .finally(() => {
        setIsLoading(false);
      });
  }, []);

  const sortedGeonames = sort({ items: geonames, sortOptions, sortBy });
  return (
    <Panel
      loading={isLoading}
      header="My Locations"
      toolbar={
        <SortBy
          defaultValue={sortBy}
          options={sortOptions}
          onSelect={sortBy => setSortBy(sortBy)}
          disabled={isLoading}
        />
      }
    >
      <UserAddLocation onAdd={handleOnAdd} existingGeonames={geonames} />
      <List celled relaxed selection style={{ marginTop: 0 }}>
        <LocationListItem
          selected={geonameId}
          key={null}
          name="Global View"
          country="Location-agnostic view"
          canDelete={false}
          onSelect={onSelect}
        />
        {sortedGeonames.map(geoname => (
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
