/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import { useNonMobileEffect } from 'hooks/useNonMobileEffect';
import React, { useEffect, useState } from 'react';
import { jsx } from 'theme-ui';
import LocationApi from 'api/LocationApi';
import { Geoname } from 'utils/constants';
import { isNonMobile } from 'utils/responsive';
import esriMap from 'map';
import aoiLayer from 'map/aoiLayer';
import eventsView from 'map/events';
import { IPanelProps } from 'components/Panel';
import { LocationListSortOptions as sortOptions } from 'components/SortBy/SortByOptions';
import { ActivePanel } from 'components/SidebarView/sidebar-types';
import { LocationListPanelDisplay } from './LocationListPanel';

type LocationListPanelContainerProps = IPanelProps & {
  activePanel: ActivePanel;
  geonameId: number;
  onSelect: (geonameId: number, name: string, fullName: string) => void;
  onClear: () => void;
};

const LocationListPanelContainer: React.FC<LocationListPanelContainerProps> = ({
  activePanel,
  geonameId,
  isMinimized,
  onMinimize,
  onSelect,
  onClear
}) => {
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
  const [geonames, setGeonames] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [sortBy, setSortBy] = useState(sortOptions[0].value);
  const [hasError, setHasError] = useState(false);

  const handleOnDelete = ({ data: { geonames } }) => {
    if (!new Set(geonames.map(n => n.geonameId)).has(geonameId)) {
      onClear();
    }
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

  const loadGeonames = () => {
    setHasError(false);
    setIsLoading(true);
    LocationApi.getUserLocations()
      .then(({ data: { geonames } }) => {
        setGeonames(geonames);
        isNonMobileDevice && aoiLayer.renderAois(geonames); // display all user AOIs on page load
      })
      .catch(() => setHasError(true))
      .finally(() => {
        setIsLoading(false);
      });
  };

  useEffect(() => {
    loadGeonames();
  }, [setGeonames, setHasError, setIsLoading]);

  useNonMobileEffect(() => {
    if (geonameId == null) {
      eventsView.updateEventView([]); // no event pins when no location is selected
      esriMap.showEventsView(true);
    }

    renderAois();
  }, [geonameId]);

  useNonMobileEffect(() => {
    renderAois();
  }, [geonames]);

  return (
    <LocationListPanelDisplay
      isLoading={isLoading}
      activePanel={activePanel}
      geonameId={geonameId}
      geonames={geonames}
      hasError={hasError}
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
      handleRetryOnClick={loadGeonames}
    />
  );
};

export default LocationListPanelContainer;
