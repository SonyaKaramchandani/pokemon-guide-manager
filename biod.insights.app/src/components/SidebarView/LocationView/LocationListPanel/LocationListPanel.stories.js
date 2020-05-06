import { action } from '@storybook/addon-actions';
import React from 'react';
import { List } from 'semantic-ui-react';

import LocationCard from './LocationCard';
import { LocationListPanelDisplay } from './LocationListPanel';
import { mockApiCallPromise } from 'utils/promiseUtils';
import { DebugContainer4BdPanel } from 'components/_debug/StorybookContainer';

export default {
  title: 'PANELS/LocationListPanel'
};

const sampleSearchResults = [
  {
    geonameId: 1850147,
    name: 'Tokyo, Tokyo, Japan',
    locationType: 2,
    latitude: 35.6895,
    longitude: 139.69171
  },
  {
    geonameId: 8334583,
    name: 'City of Cape Town, Western Cape, South Africa',
    locationType: 2,
    latitude: -33.91667,
    longitude: 18.41667
  },
  {
    geonameId: 1850144,
    name: 'Tokyo Prefecture, Japan',
    locationType: 4,
    latitude: 35.6895,
    longitude: 139.69171
  },
  {
    geonameId: 1484838,
    name: 'Toshkent Viloyati, Uzbekistan',
    locationType: 4,
    latitude: 41.23333,
    longitude: 69.8
  },
  {
    geonameId: 3165361,
    name: 'Toscana, Italy',
    locationType: 4,
    latitude: 43.41667,
    longitude: 11
  },
  { geonameId: 2363686, name: 'Togo', locationType: 6, latitude: 8.66667, longitude: 1.08333 },
  {
    geonameId: 3573591,
    name: 'Trinidad and Tobago',
    locationType: 6,
    latitude: 11,
    longitude: -61
  },
  { geonameId: 2410758, name: 'Sao Tome and Principe', locationType: 6, latitude: 1, longitude: 7 }
];

const sharedProps = {
  isLoading: false,
  geonameId: null,
  geonames: [
    {
      geonameId: 1791247,
      locationType: 2,
      name: 'Wuhan',
      country: 'China',
      province: 'Hubei Sheng',
      latitude: 30.58333,
      longitude: 114.26667
    },
    {
      geonameId: 1880251,
      locationType: 6,
      name: 'Republic of Singapore',
      country: 'Singapore',
      province: null,
      latitude: 1.36667,
      longitude: 103.8
    },
    {
      geonameId: 5551752,
      locationType: 4,
      name: 'Arizona',
      country: 'United States',
      province: 'Arizona',
      latitude: 34.5003,
      longitude: -111.50098
    },
    {
      geonameId: 6167865,
      locationType: 2,
      name: 'Toronto',
      country: 'Canada',
      province: 'Ontario',
      latitude: 43.70011,
      longitude: -79.4163
    }
  ],
  isMinimized: false,
  sortBy: 'name',
  sortOptions: [
    { value: 'name', text: 'Alphabetical', keys: ['name'], orders: ['asc'] },
    { value: 'country', text: 'Country', keys: ['country', 'name'], orders: ['asc', 'asc'] }
  ],
  onLocationSelected: action('onLocationSelected'),
  onLocationAdd: action('onLocationAdd(c9a351b7)'),
  onLocationDelete: action('onLocationDelete'),
  onAddLocationApiCallNeeded: action('onLocationAdd(c9a351b7)'),
  onSelectSortBy: action('onSelectSortBy')
};

export const test = () => (
  <DebugContainer4BdPanel>
    <LocationListPanelDisplay
      {...sharedProps}
      onSearchApiCallNeeded={({ name }) => mockApiCallPromise(sampleSearchResults)}
      onAddLocationApiCallNeeded={({ geonameId }) => mockApiCallPromise(null)}
    ></LocationListPanelDisplay>
  </DebugContainer4BdPanel>
);

export const testNoLocation = () => (
  <DebugContainer4BdPanel>
    <LocationListPanelDisplay
      {...sharedProps}
      onSearchApiCallNeeded={({ name }) => mockApiCallPromise([])}
    ></LocationListPanelDisplay>
  </DebugContainer4BdPanel>
);

const sample = {
  geonames: [
    {
      geonameId: 1172451,
      locationType: 2,
      name: 'Lahore',
      country: 'Pakistan',
      latitude: 31.558,
      longitude: 74.35071
    },
    {
      geonameId: 1255082,
      locationType: 2,
      name: 'Talwandi Bhai',
      country: 'India',
      latitude: 30.85584,
      longitude: 74.92979
    },
    {
      geonameId: 1257951,
      locationType: 2,
      name: 'Ropar',
      country: 'India',
      latitude: 30.96896,
      longitude: 76.52695
    },
    {
      geonameId: 1260108,
      locationType: 2,
      name: 'Patiala',
      country: 'India',
      latitude: 30.35,
      longitude: 76.4
    },
    {
      geonameId: 1261481,
      locationType: 2,
      name: 'New Delhi',
      country: 'India',
      latitude: 28.63576,
      longitude: 77.22445
    },
    {
      geonameId: 1269217,
      locationType: 2,
      name: 'Jaora',
      country: 'India',
      latitude: 23.63783,
      longitude: 75.12711
    },
    {
      geonameId: 1269488,
      locationType: 2,
      name: 'Jaito',
      country: 'India',
      latitude: 30.45126,
      longitude: 74.89189
    },
    {
      geonameId: 1273294,
      locationType: 2,
      name: 'Delhi',
      country: 'India',
      latitude: 28.65195,
      longitude: 77.23149
    },
    {
      geonameId: 1331235,
      locationType: 2,
      name: 'Jaitoke',
      country: 'Pakistan',
      latitude: 31.68361,
      longitude: 73.87278
    },
    {
      geonameId: 1580578,
      locationType: 4,
      name: 'Ho Chi Minh City',
      country: 'Vietnam',
      latitude: 10.82327,
      longitude: 106.62978
    },
    {
      geonameId: 1642911,
      locationType: 2,
      name: 'Jakarta',
      country: 'Indonesia',
      latitude: -6.21462,
      longitude: 106.84513
    },
    {
      geonameId: 1694008,
      locationType: 6,
      name: 'Republic of the Philippines',
      country: 'Philippines',
      latitude: 13.0,
      longitude: 122.0
    },
    {
      geonameId: 1814991,
      locationType: 6,
      name: "People's Republic of China",
      country: 'China',
      latitude: 35.0,
      longitude: 105.0
    },
    {
      geonameId: 1835848,
      locationType: 2,
      name: 'Seoul',
      country: 'South Korea',
      latitude: 37.566,
      longitude: 126.9784
    },
    {
      geonameId: 2643743,
      locationType: 2,
      name: 'London',
      country: 'United Kingdom',
      latitude: 51.50853,
      longitude: -0.12574
    },
    {
      geonameId: 3668097,
      locationType: 2,
      name: 'Siachoque',
      country: 'Colombia',
      latitude: 5.46903,
      longitude: -73.23448
    },
    {
      geonameId: 4436296,
      locationType: 4,
      name: 'Mississippi',
      country: 'United States',
      latitude: 32.75041,
      longitude: -89.75036
    },
    {
      geonameId: 5128638,
      locationType: 4,
      name: 'New York',
      country: 'United States',
      latitude: 43.00035,
      longitude: -75.4999
    },
    {
      geonameId: 6091732,
      locationType: 4,
      name: 'Nunavut',
      country: 'Canada',
      latitude: 66.03478,
      longitude: -100.07813
    },
    {
      geonameId: 6167865,
      locationType: 2,
      name: 'Toronto',
      country: 'Canada',
      latitude: 43.70011,
      longitude: -79.4163
    },
    {
      geonameId: 6251999,
      locationType: 6,
      name: 'Canada',
      country: 'Canada',
      latitude: 60.10867,
      longitude: -113.64258
    },
    {
      geonameId: 6941775,
      locationType: 2,
      name: 'Kings County',
      country: 'United States',
      latitude: 40.63439,
      longitude: -73.95027
    },
    {
      geonameId: 6995610,
      locationType: 2,
      name: 'Jaitupur Khuajji',
      country: 'India',
      latitude: 26.79432,
      longitude: 79.2117
    },
    {
      geonameId: 7391316,
      locationType: 2,
      name: 'Lahorewala Khu',
      country: 'Pakistan',
      latitude: 29.14178,
      longitude: 71.18093
    },
    {
      geonameId: 7467416,
      locationType: 2,
      name: 'Jaitogala',
      country: 'Pakistan',
      latitude: 31.9017,
      longitude: 74.53722
    },
    {
      geonameId: 8133394,
      locationType: 2,
      name: 'Toronto county',
      country: 'Canada',
      latitude: 43.69655,
      longitude: -79.42909
    },
    {
      geonameId: 8335427,
      locationType: 2,
      name: 'North West Delhi',
      country: 'India',
      latitude: 28.70113,
      longitude: 77.10154
    },
    {
      geonameId: 10517304,
      locationType: 2,
      name: 'Ropar',
      country: 'India',
      latitude: 24.91517,
      longitude: 83.58116
    },
    {
      geonameId: 12031873,
      locationType: 2,
      name: 'Vancouver',
      country: 'Canada',
      latitude: 49.24861,
      longitude: -123.10784
    }
  ]
};
var selectedGeonameId = sample.geonames[0].geonameId;
export const testLocationCards = () => (
  <List style={{ width: 350 }}>
    <LocationCard
      selected={selectedGeonameId}
      key={null}
      name="Global"
      country="Location-agnostic view"
      canDelete={false}
      onSelect={() => (selectedGeonameId = null)}
    />
    {sample.geonames.map(geoname => (
      <LocationCard
        selected={selectedGeonameId}
        key={geoname.geonameId}
        {...geoname}
        canDelete={true}
        onSelect={() => (selectedGeonameId = geoname.geonameId)}
        onDelete={action('onDelete')}
      />
    ))}
  </List>
);
