/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import * as dto from 'client/dto';
import React from 'react';
import { List } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { Error } from 'components/Error';
import { ILoadableProps, IPanelProps, Panel } from 'components/Panel';
import { ISortByProps, SortBy } from 'components/SortBy';
import { UserAddLocation } from 'components/UserAddLocation';
import { Geoname } from 'utils/constants';
import { sort } from 'utils/sort';

import LocationCard from './LocationCard';

type LocationListPanelDisplayProps = IPanelProps &
  ISortByProps &
  ILoadableProps & {
    geonameId: number;
    geonames: dto.GetGeonameModel[];
    locationFullName: string;
    hasError: boolean;
    onLocationSelected: (geonameId: number, locationName: string) => void;
    onLocationAddSuccess: (data: dto.GetUserLocationModel) => void;
    onLocationDelete: (data: dto.GetUserLocationModel) => void;
    // TODO: refactor and cleanup
    onSearchApiCallNeeded: ({ name: string }) => Promise<{ data: dto.SearchGeonameModel[] }>;
    onAddLocationApiCallNeeded: ({
      geonameId: number
    }) => Promise<{ data: dto.GetUserLocationModel }>;

    handleRetryOnClick?: () => void;
  };

export const LocationListPanelDisplay: React.FC<LocationListPanelDisplayProps> = ({
  isLoading,
  geonameId,
  geonames,
  locationFullName,
  hasError,
  onLocationSelected,
  onLocationAddSuccess,
  onLocationDelete,
  onSearchApiCallNeeded,
  onAddLocationApiCallNeeded,

  // TODO: 633056e0: group panel-related props (and similar)
  isMinimized,
  onMinimize,
  sortBy,
  sortOptions,
  onSelectSortBy,

  handleRetryOnClick,

  ...props
}) => {
  const sortedGeonames = sort({ items: geonames, sortOptions, sortBy });
  const canDeleteLocation = sortedGeonames.length > 1;
  return (
    <Panel
      isLoading={isLoading}
      title="My Locations"
      subtitle={locationFullName}
      canClose={false}
      canMinimize
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      toolbar={
        <React.Fragment>
          <SortBy
            selectedValue={sortBy}
            options={sortOptions}
            onSelect={onSelectSortBy}
            disabled={isLoading}
          />
          <UserAddLocation
            onLocationAddSuccess={onLocationAddSuccess}
            existingGeonames={geonames}
            onSearchApiCallNeeded={onSearchApiCallNeeded}
            onAddLocationApiCallNeeded={onAddLocationApiCallNeeded}
          />
        </React.Fragment>
      }
    >
      <List>
        {hasError ? (
          <Error
            title="Something went wrong."
            subtitle="Please check your network connectivity and try again."
            linkText="Click here to retry"
            linkCallback={handleRetryOnClick}
          />
        ) : (
          <React.Fragment>
            <LocationCard
              selected={geonameId}
              geonameId={Geoname.GLOBAL_VIEW}
              key={Geoname.GLOBAL_VIEW}
              name="Global"
              country="Location-agnostic view"
              canDelete={false}
              onSelect={onLocationSelected}
            />
            {sortedGeonames.map(geoname => (
              <LocationCard
                selected={geonameId}
                key={geoname.geonameId}
                {...geoname}
                canDelete={canDeleteLocation}
                onSelect={onLocationSelected}
                onDelete={onLocationDelete}
              />
            ))}
          </React.Fragment>
        )}
      </List>
    </Panel>
  );
};
