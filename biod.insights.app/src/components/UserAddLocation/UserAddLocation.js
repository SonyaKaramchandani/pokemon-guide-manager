import React, { useState } from 'react';
import { Menu, Input, Button } from 'semantic-ui-react';
import LocationApi from 'api/LocationApi';
import LocationType, { City, Province, Country } from 'domainTypes/LocationType';
import MenuItemsForOptions from './MenuItemsForOptions';

const getCategories = locations => {
  return [
    { title: LocationType[Country], values: locations.filter(l => l.locationType === City) },
    {
      title: LocationType[Province],
      values: locations.filter(l => l.locationType === Province)
    },
    {
      title: LocationType[City],
      values: locations.filter(l => l.locationType === Country)
    }
  ];
};

const AddUserLocation = ({ onAdd }) => {
  const [state, setState] = useState({
    value: '',
    selected: '',
    isAddInProgress: false,
    isLoading: false,
    locations: []
  });

  const MIN_LENGTH = 3;

  const handleChange = (_, { value }) => {
    setState(s => ({ ...s, value }));

    if (value.length >= MIN_LENGTH) {
      setState(s => ({ ...s, isLoading: true }));
      LocationApi.searchLocations({ name: value })
        .then(({ data: locations }) => {
          setState(s => ({ ...s, locations }));
        })
        .finally(() => {
          setState(s => ({ ...s, isLoading: false }));
        });
    }

    if (value.length === 0) {
      handleOnCancel();
    }
  };

  const handleOnCancel = () => {
    setState(s => ({
      ...s,
      value: '',
      isLoading: false,
      locations: []
    }));
  };

  const handleOnAddLocation = () => {
    setState(s => ({ ...s, isAddInProgress: true }));
    LocationApi.postUserLocation({ geonameId: selected })
      .then(data => {
        onAdd(data);
      })
      .finally(() => {
        setState(s => ({ ...s, isAddInProgress: false }));
      });
  };

  const { value, isLoading, locations, selected, isAddInProgress } = state;
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
      />

      {noMatchingLocations && <div>No matching locations</div>}

      {hasMatchingLocations && (
        <>
          <Menu vertical fluid className="m-0 no-rounding">
            {categories.map(({ title, values }) => (
              <MenuItemsForOptions
                key={title}
                selected={selected}
                onSelect={value => setState(s => ({ ...s, selected: value }))}
                title={title}
                options={values}
              />
            ))}
          </Menu>
          <Button.Group fluid attached>
            <Button basic color="grey" onClick={handleOnCancel}>
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
