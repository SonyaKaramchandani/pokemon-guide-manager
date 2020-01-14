/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import EventApi from 'api/EventApi';
import LocationApi from 'api/LocationApi';
import { UserAddLocation } from 'components/UserAddLocation';
import { List, Header } from 'semantic-ui-react';
import LocationCard from './LocationCard';
import { Panel } from 'components/Panel';
import { SortBy } from 'components/SortBy';
import { LocationListSortOptions as sortOptions, sort } from 'components/SidebarView/SortByOptions';
import eventsView from './../../../../map/events';

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

  useEffect(() => {
    setIsLoading(true);
    EventApi.getEvent({})
      .then(({ data: { countryPins, eventsList } }) => {
        eventsView.updateEventView(countryPins, eventsList, true);
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
          selectedValue={sortBy}
          options={sortOptions}
          onSelect={sortBy => setSortBy(sortBy)}
          disabled={isLoading}
        />
      }
      // width={250}
    >
      <UserAddLocation onAdd={handleOnAdd} existingGeonames={geonames} />
      <List>
        <LocationCard
          selected={geonameId}
          key={null}
          name="Global View"
          country="Location-agnostic view"
          canDelete={false}
          onSelect={onSelect}
        />
        {sortedGeonames.map(geoname => (
          <LocationCard
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
