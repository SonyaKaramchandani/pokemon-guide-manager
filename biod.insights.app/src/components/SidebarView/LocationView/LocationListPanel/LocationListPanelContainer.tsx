/** @jsx jsx */
import { useNonMobileEffect } from 'hooks/useNonMobileEffect';
import React, { useEffect, useState } from 'react';
import { jsx } from 'theme-ui';
import { useBreakpointIndex } from '@theme-ui/match-media';
import LocationApi from 'api/LocationApi';
import { Geoname } from 'utils/constants';
import { isNonMobile, isMobile } from 'utils/responsive';
import esriMap from 'map';
import aoiLayer from 'map/aoiLayer';
import eventsView from 'map/events';
import { IPanelProps } from 'components/Panel';
import { LocationListSortOptions as sortOptions } from 'components/SortBy/SortByOptions';
import { ActivePanel } from 'components/SidebarView/sidebar-types';
import * as dto from 'client/dto';
import { LocationListPanelDisplay } from './LocationListPanel';

type LocationListPanelContainerProps = IPanelProps & {
  activePanel: ActivePanel;
  geonameId: number;
  locationFullName: string;
  onGeonamesListLoad: (val: dto.GetGeonameModel[]) => void;
  onSelect: (geonameId: number, locationName: string) => void;
  onSelectedGeonameDeleted: () => void;
};

const LocationListPanelContainer: React.FC<LocationListPanelContainerProps> = ({
  activePanel,
  geonameId,
  locationFullName,
  isMinimized,
  onMinimize,
  onGeonamesListLoad,
  onSelect,
  onSelectedGeonameDeleted
}) => {
  const [geonames, setGeonames] = useState<dto.GetGeonameModel[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [sortBy, setSortBy] = useState(sortOptions[0].value);
  const [hasError, setHasError] = useState(false);

  const handleOnDelete = (data: dto.GetUserLocationModel) => {
    const { geonames } = data;
    if (!new Set(geonames.map(n => n.geonameId)).has(geonameId)) {
      onSelectedGeonameDeleted();
    }
    setGeonames(geonames);
  };

  const handleOnAdd = (data: dto.GetUserLocationModel) => {
    const { geonames } = data;
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
      })
      .catch(() => setHasError(true))
      .finally(() => {
        setIsLoading(false);
      });
  };

  useEffect(() => {
    loadGeonames();
  }, [setGeonames, setHasError, setIsLoading]);

  useEffect(() => {
    onGeonamesListLoad && onGeonamesListLoad(geonames);
  }, [geonames]);

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

  const isMobileDevice = isMobile(useBreakpointIndex());
  if (isMobileDevice && activePanel !== 'LocationListPanel') {
    return null;
  }

  return (
    <LocationListPanelDisplay
      isLoading={isLoading}
      geonameId={geonameId}
      geonames={geonames}
      locationFullName={locationFullName}
      hasError={hasError}
      onSearchApiCallNeeded={LocationApi.searchLocations}
      onAddLocationApiCallNeeded={LocationApi.postUserLocation}
      onLocationSelected={onSelect}
      onLocationAddSuccess={handleOnAdd}
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
