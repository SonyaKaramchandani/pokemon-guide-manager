/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import LocationApi from 'api/LocationApi';
import { UserAddLocation } from 'components/UserAddLocation';
import { List, Header } from 'semantic-ui-react';
import LocationListItem from './LocationListItem';
import { Panel } from 'components/Panel';
import { SortBy } from 'components/SortBy';
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
    isLoading={isLoading}
    title={
    <Header as='h2'>My Locations</Header>
    }
      canClose={false}
      canMinimize={false}
      toolbar={
        <SortBy
          defaultValue={sortBy}
          options={sortOptions}
          onSelect={sortBy => setSortBy(sortBy)}
          disabled={isLoading}
        />
      }
      // width={250}
    >
      <UserAddLocation onAdd={handleOnAdd} existingGeonames={geonames} />
      <List>
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
