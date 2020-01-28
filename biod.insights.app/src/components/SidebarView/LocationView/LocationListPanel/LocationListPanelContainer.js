/** @jsx jsx */
import React, { useEffect, useState } from 'react';
import { jsx } from 'theme-ui';

import LocationApi from 'api/LocationApi';
import { Geoname } from 'utils/constants';

import esriMap from 'map';
import aoiLayer from 'map/aoiLayer';
import eventsView from 'map/events';

import { LocationListSortOptions as sortOptions } from 'components/SidebarView/SortByOptions';

import { LocationListPanelDisplay } from './LocationListPanel';


function LocationListPanelContainer({ geonameId, isMinimized, onMinimize, onSelect }) {
  const [geonames, setGeonames] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [sortBy, setSortBy] = useState(sortOptions[0].value);

  const handleOnDelete = ({ data: { geonames } }) => {
    setGeonames(geonames);
  };

  const handleOnAdd = ({ data: { geonames } }) => {
    setGeonames(geonames);
  };

  const renderAois = () => {
    if (geonameId == null && geonames && geonames.length) {
      aoiLayer.renderAois(geonames); // display all user AOIs when no location is selected
    } else if (geonameId === Geoname.GLOBAL_VIEW) {
      aoiLayer.renderAois([]); // clear user AOIs when global view is selected
    } else if (geonameId !== null) {
      aoiLayer.renderAois([{ geonameId }]); // only selected user AOI
    }
  };

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
      eventsView.updateEventView([]); // no event pins when no location is selected
      esriMap.showEventsView(true);
    }

    renderAois();
  }, [geonameId]);

  useEffect(() => {
    renderAois();
  }, [geonames]);

  return (
    <LocationListPanelDisplay
      isLoading={isLoading}
      geonameId={geonameId}
      geonames={geonames}
      onSearchApiCallNeeded={LocationApi.searchLocations}
      onAddLocationApiCallNeeded={LocationApi.postUserLocation}
      onLocationSelected={onSelect}
      onLocationAdd={handleOnAdd}
      onLocationDelete={handleOnDelete}
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      sortBy={sortBy}
      sortOptions={sortOptions}
      onSelectSortBy={setSortBy}
    ></LocationListPanelDisplay>
  );
}

export default LocationListPanelContainer;
