import React from 'react';
import SidebarView from './SidebarView';
import { act, render, renderWithRouter, fireEvent, waitForElement } from 'utils/testUtils';
import EventApi from 'api/EventApi';
import EventsApi from 'api/EventsApi';
import LocationApi from 'api/LocationApi';

describe('SidebarView', () => {
  const eventTitle = 'Mock Event Title';
  const eventListPanelTitle = 'My Events';
  const locationListPanelTitle = 'My Locations';

  const events = {
    data: {
      localCaseCounts: null,
      importationRisk: null,
      exportationRisk: null,
      eventsList: [],
      countryPins: []
    }
  };

  const event = {
    data: {
      isLocal: false,
      eventInformation: {
        id: 123,
        title: eventTitle
      },
      importationRisk: null,
      exportationRisk: { isModelNotRun: true },
      caseCounts: { reportedCases: 0, deaths: 0 },
      eventLocations: [],
      articles: [],
      sourceAirports: [],
      destinationAirports: [],
      diseaseInformation: {}
    }
  };

  const userLocations = {
    data: {
      geonames: []
    }
  };

  test('show event listing view', async () => {
    EventsApi.getEvents = jest.fn().mockResolvedValue(events);

    const { getByText } = renderWithRouter(<SidebarView />, { route: 'event' });
    await waitForElement(() => getByText(eventListPanelTitle));

    expect(getByText(eventListPanelTitle)).toBeVisible();
  });

  test('show event listing view with event details', async () => {
    EventsApi.getEvents = jest.fn().mockResolvedValue(events);
    EventApi.getEvent = jest.fn().mockResolvedValue(event);

    const { getByText } = renderWithRouter(<SidebarView />, { route: 'event/123' });
    await waitForElement(() => getByText(eventListPanelTitle));

    expect(getByText(eventListPanelTitle)).toBeVisible();
    expect(getByText(eventTitle)).toBeVisible();
  });

  test('show location view', async () => {
    LocationApi.getUserLocations = jest.fn().mockResolvedValue(userLocations);

    const { getByText } = renderWithRouter(<SidebarView />, { route: 'location' });
    await waitForElement(() => getByText(locationListPanelTitle));

    expect(getByText(locationListPanelTitle)).toBeVisible();
  });
});
