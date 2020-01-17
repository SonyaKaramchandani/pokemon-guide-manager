/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import LocationApi from 'api/LocationApi';
import { UserAddLocation } from 'components/UserAddLocation';
import { List, Header } from 'semantic-ui-react';
import LocationCard from './LocationCard';
import { Panel } from 'components/Panel';
import { SortBy } from 'components/SortBy';
import { LocationListSortOptions as sortOptions, sort } from 'components/SidebarView/SortByOptions';
import esriMap from 'map';
import eventsView from 'map/events';
import aoiLayer from 'map/aoiLayer';
import { Geoname } from 'utils/constants';
import { BdIcon } from 'components/_common/BdIcon';

function LocationListPanel({ geonameId, isMinimized, onMinimize, onSelect }) {
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
        aoiLayer.renderAois(geonames); // display all user AOIs on page load
      })
      .finally(() => {
        setIsLoading(false);
      });
  }, []);

  useEffect(() => {
    if (geonameId == null) {
      eventsView.updateEventView([]);  // no event pins when no location is selected
      esriMap.showEventsView(true);
      if (geonames && geonames.length) {
        aoiLayer.renderAois(geonames);  // display all user AOIs when no location is selected
      }
    } else if (geonameId === Geoname.GLOBAL_VIEW) {
      aoiLayer.renderAois([]);  // clear user AOIs when global view is selected
    } else {
      aoiLayer.renderAois([{ geonameId }]);  // only selected user AOI      
    }
  }, [geonameId]);

  const sortedGeonames = sort({ items: geonames, sortOptions, sortBy });
  return (
    <Panel
    isLoading={isLoading}
    title={
    "My Locations"
    }
      canClose={false}
      canMinimize={true}
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      toolbar={
        <>
          <SortBy
          selectedValue={sortBy}
          options={sortOptions}
          onSelect={sortBy => setSortBy(sortBy)}
          disabled={isLoading}
        />
        <UserAddLocation onAdd={handleOnAdd} existingGeonames={geonames} />
        </>
      }
    >
      <List>
        <LocationCard
          selected={geonameId}
          geonameId={Geoname.GLOBAL_VIEW}
          key={Geoname.GLOBAL_VIEW}
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
