/** @jsx jsx */
import * as dto from 'client/dto';
import { useDebouncedState } from 'hooks/useDebouncedState';
import React, { useCallback, useEffect, useRef, useState } from 'react';
import { Input } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import {
  AdditiveSearchCategory,
  AdditiveSearchCategoryOption
} from 'components/_controls/AoiSearch/models';
import { locationTypePrint } from 'utils/stringFormatingHelpers';

import { BdIcon } from 'components/_common/BdIcon';
import { AdditiveSearchCategoryMenu } from 'components/_controls/AoiSearch/AdditiveSearchCategoryMenu';

const MapSearchGeonameModel2AdditiveSearchCategories = (
  geonames: dto.SearchGeonameModel[],
  existingGeonames: dto.SearchGeonameModel[]
): AdditiveSearchCategory[] => {
  const Map2SubcategoryOptions = (
    locations: dto.SearchGeonameModel[],
    type: dto.LocationType,
    existingGeonames: dto.SearchGeonameModel[]
  ): AdditiveSearchCategoryOption[] =>
    locations
      .filter(l => l.locationType === type)
      .map(({ geonameId, name }) => ({
        id: geonameId,
        name,
        disabled: existingGeonames.some(e => e.geonameId === geonameId)
      }));

  const catCountries: AdditiveSearchCategory = {
    name: locationTypePrint(dto.LocationType.Country),
    values: Map2SubcategoryOptions(geonames, dto.LocationType.Country, existingGeonames)
  };
  const catProvinces: AdditiveSearchCategory = {
    name: locationTypePrint(dto.LocationType.Province),
    values: Map2SubcategoryOptions(geonames, dto.LocationType.Province, existingGeonames)
  };
  const catCities: AdditiveSearchCategory = {
    name: locationTypePrint(dto.LocationType.City),
    values: Map2SubcategoryOptions(geonames, dto.LocationType.City, existingGeonames)
  };

  return [
    ...(catCountries.values.length ? [catCountries] : []),
    ...(catProvinces.values.length ? [catProvinces] : []),
    ...(catCities.values.length ? [catCities] : [])
  ];
};

//=====================================================================================================================================

interface UserAddLocationProps {
  debounceDelay?: number;
  closeOnSelect?: boolean;
  onLocationAddSuccess: (data: dto.GetUserLocationModel) => void;
  existingGeonames: dto.SearchGeonameModel[];
  onSearchApiCallNeeded: ({ name: string }) => Promise<{ data: dto.SearchGeonameModel[] }>;
  onAddLocationApiCallNeeded: ({
    geonameId: number
  }) => Promise<{ data: dto.GetUserLocationModel }>;
}

const UserAddLocation: React.FC<UserAddLocationProps> = ({
  debounceDelay = 500,
  closeOnSelect = false,
  onLocationAddSuccess,
  existingGeonames,
  onSearchApiCallNeeded,
  onAddLocationApiCallNeeded
}) => {
  const [
    searchText,
    searchTextDebounced,
    setSearchText,
    setSearchTextForceNoProxy
  ] = useDebouncedState('', debounceDelay);

  const [locationCategories, setLocationCategories] = useState<AdditiveSearchCategory[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [isAddInProgress, setIsAddInProgress] = useState(false);
  const [selectedAoiId, setSelectedAoiId] = useState(null);
  const MinLength = 3;

  const reset = () => {
    setLocationCategories([]);
    setIsLoading(false);
    setIsAddInProgress(false);
  };

  useEffect(() => {
    if (searchTextDebounced && searchTextDebounced.length >= MinLength) {
      setIsLoading(true);
      onSearchApiCallNeeded &&
        onSearchApiCallNeeded({ name: searchTextDebounced })
          .then(({ data }) => {
            const mappedCategories = MapSearchGeonameModel2AdditiveSearchCategories(
              data,
              existingGeonames
            );
            setLocationCategories(mappedCategories);
          })
          .finally(() => {
            setIsLoading(false);
          });
    } else if (searchTextDebounced && searchTextDebounced.length) {
      setLocationCategories([]);
    } else {
      setIsLoading(false);
    }
  }, [existingGeonames, onSearchApiCallNeeded, searchTextDebounced]);

  const handleOnSelect = (id: number) => {
    setSelectedAoiId(id);
    closeOnSelect && setSearchTextForceNoProxy('');
  };

  const handleOnAdd = () => {
    setIsAddInProgress(true);
    onAddLocationApiCallNeeded &&
      onAddLocationApiCallNeeded({ geonameId: selectedAoiId })
        .then(({ data }) => {
          onLocationAddSuccess(data);
        })
        .finally(() => {
          reset();
        });
    setSearchTextForceNoProxy('');
    setSelectedAoiId(null);
  };

  const handleOnCancel = () => {
    setSearchTextForceNoProxy('');
    setSelectedAoiId(null);
    reset();
  };

  const hasSearchText = searchText && !!searchText.length;
  const hasMatchingResults = hasSearchText && !!locationCategories.length;
  const noMatchingResults = hasSearchText && !isLoading && !hasMatchingResults;

  return (
    <div sx={{ position: 'relative' }}>
      <Input
        data-testid="searchInput"
        icon={<BdIcon name="icon-maximize" color="sea100" bold />}
        iconPosition="left"
        placeholder="Add a location"
        fluid
        value={searchText}
        onChange={event => setSearchText(event.target.value)}
        attached="top"
        loading={isLoading}
      />

      {(hasMatchingResults || noMatchingResults) && (
        <div
          sx={{
            boxShadow: [null, '0px 4px 4px rgba(0, 0, 0, 0.15)'],
            borderRadius: '4px',
            width: ['100%', '350px'],
            borderRightColor: '@stone20',
            bg: 'seafoam10',
            position: [null, 'absolute']
          }}
        >
          <AdditiveSearchCategoryMenu
            categories={locationCategories}
            selectedId={selectedAoiId}
            trayButtonsState={isAddInProgress ? 'busy' : !selectedAoiId ? 'disabled' : 'enabled'}
            onSelect={handleOnSelect}
            onCancel={handleOnCancel}
            onAdd={handleOnAdd}
          />
        </div>
      )}
    </div>
  );
};

export default UserAddLocation;
