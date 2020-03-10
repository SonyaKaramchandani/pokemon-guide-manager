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
import { isMobile } from 'utils/responsive';
import { sort } from 'utils/sort';
import { ActivePanel } from 'components/SidebarView/sidebar-types';

import LocationCard from './LocationCard';

const getLocationFullName = (geonames: dto.GetGeonameModel[], geonameId: number): string => {
  if (geonameId === Geoname.GLOBAL_VIEW) return 'Global View';
  if (geonameId === null) return null;

  let subtitle = null;
  const selectedGeoname = geonames.find(g => g.geonameId === geonameId);
  if (selectedGeoname) {
    const { name, province, country } = selectedGeoname;
    subtitle = [name, province, country].filter(i => !!i).join(', ');
  }
  return subtitle;
};

//=====================================================================================================================================

type LocationListPanelDisplayProps = IPanelProps &
  ISortByProps &
  ILoadableProps & {
    activePanel: ActivePanel;
    geonameId: number;
    geonames: dto.GetGeonameModel[];
    hasError: boolean;
    onLocationSelected: (geonameId: number, name: string, fullName: string) => void;
    onLocationAdd: (data) => void;
    onLocationDelete;
    // TODO: refactor and cleanup
    onSearchApiCallNeeded: ({ name: string }) => Promise<{ data: dto.SearchGeonameModel[] }>;
    onAddLocationApiCallNeeded: ({
      geonameId: number
    }) => Promise<{ data: dto.GetUserLocationModel }>;

    handleRetryOnClick?: () => void;
  };

export const LocationListPanelDisplay: React.FC<LocationListPanelDisplayProps> = ({
  isLoading,
  activePanel,
  geonameId,
  geonames,
  hasError,
  onLocationSelected,
  onLocationAdd,
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
  const isMobileDevice = isMobile(useBreakpointIndex());
  if (isMobileDevice && activePanel !== 'LocationListPanel') {
    return null;
  }

  const handleLocationCardOnSelect = (geonameId: number, name: string) => {
    const fullName = getLocationFullName(geonames, geonameId);
    onLocationSelected(geonameId, name, fullName);
  };

  const subtitle = getLocationFullName(geonames, geonameId);
  const sortedGeonames = sort({ items: geonames, sortOptions, sortBy });
  const canDeleteLocation = sortedGeonames.length > 1;
  return (
    <Panel
      isLoading={isLoading}
      title="My Locations"
      subtitle={subtitle}
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
            onAdd={onLocationAdd}
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
              onSelect={handleLocationCardOnSelect}
            />
            {sortedGeonames.map(geoname => (
              <LocationCard
                selected={geonameId}
                key={geoname.geonameId}
                {...geoname}
                canDelete={canDeleteLocation}
                onSelect={handleLocationCardOnSelect}
                onDelete={onLocationDelete}
              />
            ))}
          </React.Fragment>
        )}
      </List>
    </Panel>
  );
};
