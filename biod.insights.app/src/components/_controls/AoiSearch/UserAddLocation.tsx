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

type GeonameCategory = AdditiveSearchCategory<dto.SearchGeonameModel>;
type GeonameOption = AdditiveSearchCategoryOption<dto.SearchGeonameModel>;

const MapSearchGeonameModel2AdditiveSearchCategories = (
  geonames: dto.SearchGeonameModel[],
  existingGeonames: dto.SearchGeonameModel[]
): GeonameCategory[] => {
  const Map2SubcategoryOptions = (
    locations: dto.SearchGeonameModel[],
    type: dto.LocationType
  ): GeonameOption[] =>
    locations
      .filter(l => l.locationType === type)
      .map(aoi => ({
        id: aoi.geonameId,
        name: aoi.name,
        disabled: existingGeonames.some(e => e.geonameId === aoi.geonameId),
        data: aoi
      }));

  const catCountries: GeonameCategory = {
    name: locationTypePrint(dto.LocationType.Country),
    values: Map2SubcategoryOptions(geonames, dto.LocationType.Country)
  };
  const catProvinces: GeonameCategory = {
    name: locationTypePrint(dto.LocationType.Province),
    values: Map2SubcategoryOptions(geonames, dto.LocationType.Province)
  };
  const catCities: GeonameCategory = {
    name: locationTypePrint(dto.LocationType.City),
    values: Map2SubcategoryOptions(geonames, dto.LocationType.City)
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
  existingGeonames: dto.SearchGeonameModel[];
  onSearchApiCallNeeded: (name: string) => Promise<dto.SearchGeonameModel[]>;
  onAddLocation: (aoi: dto.SearchGeonameModel) => void;
  isAddInProgress?: boolean;
}

export const UserAddLocation: React.FC<UserAddLocationProps> = ({
  debounceDelay = 500,
  closeOnSelect = false,
  existingGeonames,
  onSearchApiCallNeeded,
  onAddLocation,
  isAddInProgress
}) => {
  const [
    searchText,
    searchTextDebounced,
    setSearchText,
    setSearchTextForceNoProxy
  ] = useDebouncedState('', debounceDelay);

  const [locationCategories, setLocationCategories] = useState<GeonameCategory[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [selectedAoi, setSelectedAoi] = useState<dto.SearchGeonameModel>(null);
  const MinLength = 3;

  const reset = () => {
    setLocationCategories([]);
    setIsLoading(false);
  };

  useEffect(() => {
    if (searchTextDebounced && searchTextDebounced.length >= MinLength) {
      setIsLoading(true);
      onSearchApiCallNeeded &&
        onSearchApiCallNeeded(searchTextDebounced)
          .then(data => {
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

  const handleOnSelect = (aoi: dto.SearchGeonameModel) => {
    setSelectedAoi(aoi);
    closeOnSelect && setSearchTextForceNoProxy('');
  };

  const handleOnAdd = () => {
    onAddLocation && onAddLocation(selectedAoi);
    setSearchTextForceNoProxy('');
    setSelectedAoi(null);
    setLocationCategories([]);
  };

  const handleOnCancel = () => {
    setSearchTextForceNoProxy('');
    setSelectedAoi(null);
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
            position: [null, 'absolute'],
            zIndex: ['initial', 100]
          }}
        >
          <AdditiveSearchCategoryMenu<dto.SearchGeonameModel>
            categories={locationCategories}
            selectedId={selectedAoi && selectedAoi.geonameId}
            trayButtonsState={isAddInProgress ? 'busy' : !selectedAoi ? 'disabled' : 'enabled'}
            onSelect={handleOnSelect}
            onCancel={handleOnCancel}
            onAdd={handleOnAdd}
          />
        </div>
      )}
    </div>
  );
};
