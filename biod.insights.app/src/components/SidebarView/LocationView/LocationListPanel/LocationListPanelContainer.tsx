/** @jsx jsx */
import { useNonMobileEffect } from 'hooks/useNonMobileEffect';
import React, { useEffect, useState, useCallback } from 'react';
import { jsx } from 'theme-ui';
import { useBreakpointIndex } from '@theme-ui/match-media';
import LocationApi from 'api/LocationApi';
import { Geoname } from 'utils/constants';
import { isNonMobile, isMobile } from 'utils/responsive';
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
  const [geonames, setGeonames] = useState<dto.GetGeonameModel[]>(null);
  const [isLoading, setIsLoading] = useState(false);
  const [sortBy, setSortBy] = useState(sortOptions[0].value);
  const [hasError, setHasError] = useState(false);

  const handleOnDelete = useCallback(
    (data: dto.GetUserLocationModel) => {
      const { geonames } = data;
      if (!new Set(geonames.map(n => n.geonameId)).has(geonameId)) {
        onSelectedGeonameDeleted();
      }
      setGeonames(geonames);
    },
    [onSelectedGeonameDeleted, setGeonames]
  );

  const handleOnAdd = useCallback(
    (data: dto.GetUserLocationModel) => {
      const { geonames } = data;
      setGeonames(geonames);
    },
    [setGeonames]
  );

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

  const isMobileDevice = isMobile(useBreakpointIndex());
  if (isMobileDevice && activePanel !== 'LocationListPanel') {
    return null;
  }

  return (
    <LocationListPanelDisplay
      isLoading={isLoading}
      geonameId={geonameId}
      geonames={geonames || []}
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
