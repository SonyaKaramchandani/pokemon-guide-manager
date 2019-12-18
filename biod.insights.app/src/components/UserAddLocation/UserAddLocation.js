import React, { useState, useRef } from 'react';
import { Menu, Input, Button } from 'semantic-ui-react';
import LocationApi from 'api/LocationApi';
import LocationType, { City, Province, Country } from 'domainTypes/LocationType';
import MenuItemsForOptions from './MenuItemsForOptions';

const getCategories = geonames => {
  return [
    { title: LocationType[Country], values: geonames.filter(l => l.locationType === City) },
    {
      title: LocationType[Province],
      values: geonames.filter(l => l.locationType === Province)
    },
    {
      title: LocationType[City],
      values: geonames.filter(l => l.locationType === Country)
    }
  ];
};

const appendDisabilityToGeonames = (geonames, existingGeonames) => {
  return geonames.map(location => {
    const disabled = existingGeonames.some(
      existingLoc => existingLoc.geonameId === location.geonameId
    );
    return {
      ...location,
      disabled
    };
  });
};

const initialState = {
  value: '',
  selected: '',
  isAddInProgress: false,
  isLoading: false,
  locations: []
};

const AddUserLocation = ({ onAdd, existingGeonames }) => {
  const [value, setValue] = useState('');
  const [selected, setSelected] = useState('');
  const [locations, setLocations] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [isAddInProgress, setIsAddInProgress] = useState(false);

  const inputRef = useRef(null);

  const MIN_LENGTH = 3;

  const handleChange = (_, { value }) => {
    setValue(value);

    if (value.length >= MIN_LENGTH) {
      setIsLoading(true);
      LocationApi.searchLocations({ name: value })
        .then(({ data: locations }) => {
          setLocations(appendDisabilityToGeonames(locations, existingGeonames));
        })
        .finally(() => {
          setIsLoading(false);
        });
    }

    if (value.length === 0) {
      reset();
    }
  };

  const reset = () => {
    setValue('');
    setSelected('');
    setLocations([]);
    setIsLoading(false);
    setIsAddInProgress(false);
  };

  const handleOnAddLocation = () => {
    setIsAddInProgress(true);
    LocationApi.postUserLocation({ geonameId: selected })
      .then(data => {
        onAdd(data);
      })
      .finally(() => {
        reset();
        inputRef.current.focus();
      });
  };

  const hasMatchingLocations = !!locations.length;
  const noMatchingLocations = !!value.length && !isLoading && !hasMatchingLocations;
  const categories = getCategories(locations);

  return (
    <div>
      <Input
        icon="plus"
        iconPosition="left"
        placeholder="Add a location"
        fluid
        value={value}
        onChange={handleChange}
        loading={isLoading}
        attached="top"
        className="no-rounding"
        ref={inputRef}
      />

      {noMatchingLocations && <div>No matching locations</div>}

      {hasMatchingLocations && (
        <>
          <Menu vertical fluid className="m-0 no-rounding">
            {categories.map(({ title, values }) => (
              <MenuItemsForOptions
                key={title}
                selected={selected}
                onSelect={value => setSelected(value)}
                title={title}
                options={values}
              />
            ))}
          </Menu>
          <Button.Group fluid attached>
            <Button basic color="grey" onClick={reset}>
              Cancel
            </Button>
            <Button
              basic
              color="blue"
              disabled={!selected.length || isAddInProgress}
              onClick={handleOnAddLocation}
              loading={isAddInProgress}
            >
              Add Location
            </Button>
          </Button.Group>
        </>
      )}
    </div>
  );
};

export default AddUserLocation;
