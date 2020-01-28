/** @jsx jsx */
import React from 'react';
import { List } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { Geoname } from 'utils/constants';

import { Panel } from 'components/Panel';
import { LocationListSortOptions as sortOptions, sort } from 'components/SidebarView/SortByOptions';
import { SortBy } from 'components/SortBy';
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

  ...props
}) => {

  const subtitle = getSubtitle(geonames, geonameId);
  const sortedGeonames = sort({ items: geonames, sortOptions, sortBy });
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
            sortBy={sortBy}
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
            canDelete={true}
            onSelect={onLocationSelected}
            onDelete={onLocationDelete}
          />
        ))}
      </List>
    </Panel>
  );
}
