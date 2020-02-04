/** @jsx jsx */
import React from 'react';
import { List } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { Geoname } from 'utils/constants';

import { Panel } from 'components/Panel';
import { LocationListSortOptions as sortOptions, sort } from 'components/SidebarView/SortByOptions';
import { SortBy } from 'components/SortBy';
import { Error } from 'components/Error';
import { UserAddLocation } from 'components/UserAddLocation';

import LocationCard from './LocationCard';

const getSubtitle = (geonames, geonameId) => {
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

export const LocationListPanelDisplay = ({
  isLoading,
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
  const subtitle = getSubtitle(geonames, geonameId);
  const sortedGeonames = sort({ items: geonames, sortOptions, sortBy });
  const canDeleteLocation = sortedGeonames.length > 1;
  return (
    <Panel
      isLoading={isLoading}
      title={'My Locations'}
      subtitle={subtitle}
      canClose={false}
      canMinimize={true}
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      toolbar={
        <>
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
        </>
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
          <>
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
          </>
        )}
      </List>
    </Panel>
  );
};
