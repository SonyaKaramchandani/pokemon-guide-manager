/** @jsx jsx */
import React, { useState } from 'react';
import { jsx } from 'theme-ui';
import * as dto from 'client/dto';
import { AdditiveSearch } from 'components/AdditiveSearch';
import { locationTypePrint } from 'utils/stringFormatingHelpers';

const getCategoryValues = (
  locations: dto.SearchGeonameModel[],
  type: dto.LocationType,
  existingGeonames: dto.SearchGeonameModel[]
) =>
  locations
    .filter(l => l.locationType === type)
    .map(({ geonameId, name }) => ({
      id: geonameId,
      name,
      disabled: existingGeonames.some(e => e.geonameId === geonameId)
    }));

const getCategories = (
  geonames: dto.SearchGeonameModel[],
  existingGeonames: dto.SearchGeonameModel[]
) => {
  // TODO: 3a34785c: constrain the categories
  const catCountries = {
    name: locationTypePrint(dto.LocationType.Country),
    values: getCategoryValues(geonames, dto.LocationType.Country, existingGeonames)
  };
  const catProvinces = {
    name: locationTypePrint(dto.LocationType.Province),
    values: getCategoryValues(geonames, dto.LocationType.Province, existingGeonames)
  };
  const catCities = {
    name: locationTypePrint(dto.LocationType.City),
    values: getCategoryValues(geonames, dto.LocationType.City, existingGeonames)
  };

  return [
    ...(catCountries.values.length ? [catCountries] : []),
    ...(catProvinces.values.length ? [catProvinces] : []),
    ...(catCities.values.length ? [catCities] : [])
  ];
};

interface UserAddLocationProps {
  onAdd: (data) => void;
  existingGeonames: dto.SearchGeonameModel[];
  onSearchApiCallNeeded: ({ name: string }) => Promise<{ data: dto.SearchGeonameModel[] }>;
  onAddLocationApiCallNeeded: ({
    geonameId: number
  }) => Promise<{ data: dto.GetUserLocationModel }>;
}

const UserAddLocation: React.FC<UserAddLocationProps> = ({
  onAdd,
  existingGeonames,
  onSearchApiCallNeeded,
  onAddLocationApiCallNeeded
}) => {
  const [locations, setLocations] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [isAddInProgress, setIsAddInProgress] = useState(false);

  const reset = () => {
    setLocations([]);
    setIsLoading(false);
    setIsAddInProgress(false);
  };

  const handleOnSearch = value => {
    if (value && value.length) {
      setIsLoading(true);
      onSearchApiCallNeeded &&
        onSearchApiCallNeeded({ name: value })
          .then(({ data }) => {
            setLocations(getCategories(data, existingGeonames));
          })
          .finally(() => {
            setIsLoading(false);
          });
    } else {
      setIsLoading(false);
    }
  };

  const handleOnAddLocation = geonameId => {
    setIsAddInProgress(true);
    // TODO: c9a351b7 ??????
    onAddLocationApiCallNeeded &&
      onAddLocationApiCallNeeded({ geonameId })
        .then(data => {
          onAdd(data);
        })
        .finally(() => {
          reset();
        });
  };

  const handleOnCancel = () => {
    reset();
  };

  return (
    <AdditiveSearch
      isLoading={isLoading}
      isAddInProgress={isAddInProgress}
      categories={locations}
      onSearch={handleOnSearch}
      onAdd={handleOnAddLocation}
      onCancel={handleOnCancel}
      placeholder="Add a location"
      addButtonLabel="Add Location"
      noResultsText="No location found"
    />
  );
};

export default UserAddLocation;
