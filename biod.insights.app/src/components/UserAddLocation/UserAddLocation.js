/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import LocationApi from 'api/LocationApi';
import LocationType, { City, Province, Country } from 'domainTypes/LocationType';
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
  return [
    {
      name: LocationType[Country],
      values: getCategoryValues(geonames, Country, existingGeonames)
    },
    {
      name: LocationType[Province],
      values: getCategoryValues(geonames, Province, existingGeonames)
    },
    {
      name: LocationType[City],
      values: getCategoryValues(geonames, City, existingGeonames)
    }
  ];
};

const AddUserLocation = ({ onAdd, existingGeonames }) => {
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
      LocationApi.searchLocations({ name: value })
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
    LocationApi.postUserLocation({ geonameId })
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
    />
  );
};

export default AddUserLocation;
