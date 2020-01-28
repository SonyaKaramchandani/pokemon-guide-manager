/** @jsx jsx */
import LocationType, { City, Country, Province } from 'domainTypes/LocationType';
import React, { useState } from 'react';
import { jsx } from 'theme-ui';

import { AdditiveSearch } from 'components/AdditiveSearch';

const getCategoryValues = (locations, type, existingGeonames) =>
  locations
    .filter(l => l.locationType === type)
    .map(({ geonameId, name }) => ({
      id: geonameId,
      name,
      disabled: existingGeonames.some(e => e.geonameId === geonameId)
    }));

const getCategories = (geonames, existingGeonames) => {
  // TODO: 3a34785c: constrain the categories
  const catCountries = {
    name: LocationType[Country],
    values: getCategoryValues(geonames, Country, existingGeonames),
  };
  const catProvinces = {
    name: LocationType[Province],
    values: getCategoryValues(geonames, Province, existingGeonames),
  };
  const catCities = {
    name: LocationType[City],
    values: getCategoryValues(geonames, City, existingGeonames),
  };

  return [
    ...catCountries.values.length ? [catCountries] : [],
    ...catProvinces.values.length ? [catProvinces] : [],
    ...catCities.values.length ? [catCities] : [],
  ];
};

const UserAddLocation = ({
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
      onSearchApiCallNeeded && onSearchApiCallNeeded({ name: value })
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
    onAddLocationApiCallNeeded && onAddLocationApiCallNeeded({ geonameId })
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
