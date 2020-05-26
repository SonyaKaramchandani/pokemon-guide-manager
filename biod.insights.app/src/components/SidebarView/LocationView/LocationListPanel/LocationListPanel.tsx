/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import * as dto from 'client/dto';
import React, { useState } from 'react';
import { List } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { Error } from 'components/Error';
import { ILoadableProps, IPanelProps, Panel } from 'components/Panel';
import { SortBy } from 'components/SortBy';
import { UserAddLocation } from 'components/_controls/AoiSearch';
import { Geoname } from 'utils/constants';
import { sort } from 'utils/arrayHelpers';

import LocationCard from './LocationCard';
import { LocationListSortOptions, LocationListSortOptionValues } from 'models/SortByOptions';

type LocationListPanelDisplayProps = IPanelProps &
  ILoadableProps & {
    geonameId: number;
    geonames: dto.GetGeonameModel[];
    locationFullName: string;
    hasError: boolean;
    onLocationSelected: (geonameId: number, locationName: string) => void;
    onSearchApiCallNeeded: (name: string) => Promise<dto.SearchGeonameModel[]>;
    onLocationAdd: (aoi: dto.SearchGeonameModel) => void;
    onLocationDelete: (data: dto.GetUserLocationModel) => void;
    isAoiAddInProgress: boolean;
    handleRetryOnClick?: () => void;
  };

export const LocationListPanelDisplay: React.FC<LocationListPanelDisplayProps> = ({
  isLoading,
  geonameId,
  geonames,
  locationFullName,
  hasError,
  onLocationSelected,
  onSearchApiCallNeeded,
  onLocationAdd,
  onLocationDelete,
  isAoiAddInProgress,

  // TODO: 633056e0: group panel-related props (and similar)
  isMinimized,
  onMinimize,

  handleRetryOnClick,

  ...props
}) => {
  const [sortBy, setSortBy] = useState(LocationListSortOptions[0].value); // TODO: 597e3adc

  const sortedGeonames = sort({
    items: geonames,
    sortOptions: LocationListSortOptions,
    sortBy
  });
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
            options={LocationListSortOptions}
            // TODO: 597e3adc: this cast is a temp hack until we refactor SortBy
            onSelect={sortKey => setSortBy(sortKey as LocationListSortOptionValues)}
            disabled={isLoading}
          />
          <UserAddLocation
            existingGeonames={geonames}
            onSearchApiCallNeeded={onSearchApiCallNeeded}
            onAddLocation={onLocationAdd}
            isAddInProgress={isAoiAddInProgress}
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
