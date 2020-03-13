import React from 'react';
import { act, render, renderWithRouter, fireEvent, waitForElement } from 'utils/testUtils';
import { useBreakpointIndex } from '@theme-ui/match-media';
import EventApi from 'api/EventApi';
import EventsApi from 'api/EventsApi';
import EventView from './EventView';

describe('EventView', () => {
  const eventId = 123;
  const eventTitle = 'MockEventTitle';

  const event = {
    data: {
      isLocal: false,
      eventInformation: {
        id: eventId,
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

  const events = {
    data: {
      localCaseCounts: null,
      importationRisk: null,
      exportationRisk: null,
      eventsList: [event.data],
      countryPins: []
    }
  };

  test('minimize event listing panel', async () => {
    useBreakpointIndex.mockReturnValue(1);
    EventsApi.getEvents = jest.fn().mockResolvedValue(events);

    const { getByTestId } = render(<EventView />);
    await waitForElement(() => getByTestId('minimizeButton'));
    expect(getByTestId('minimizeButton')).toBeVisible();

    fireEvent.click(getByTestId('minimizeButton'));
    expect(getByTestId('minimizedPanel')).toBeVisible();
  });

  test('test event view with details panel open', async () => {
    EventsApi.getEvents = jest.fn().mockResolvedValue(events);
    EventApi.getEvent = jest.fn().mockResolvedValue(event);

    // NOTE: b97fe2b5: setting the eventId prop will automatically open EDP
    const { getByText, getAllByText } = render(<EventView eventId={eventId} />);
    await waitForElement(() => getAllByText(eventTitle));

    // verify event details panel is showing
    expect(getAllByText(eventTitle)).toHaveLength(2);
    expect(getByText('Risk of Importation')).toBeVisible(); // event details panel
  });

  test('minimize event details panel', async () => {
    const minimizeButtonSelector = `[data-testid="panel-${eventTitle}"] [data-testid="minimizeButton"]`;
    const minimizedPanelSelector = `[data-testid="panel-${eventTitle}"] [data-testid="minimizedPanel"]`;

    useBreakpointIndex.mockReturnValue(1);
    EventsApi.getEvents = jest.fn().mockResolvedValue(events);
    EventApi.getEvent = jest.fn().mockResolvedValue(event);

    // NOTE: b97fe2b5: setting the eventId prop will automatically open EDP
    const { container, getByText, getAllByText } = render(<EventView eventId={eventId} />);
    await waitForElement(() => getAllByText(eventTitle));

    // click minimize button on the event details panel that is already open (b97fe2b5)
    fireEvent.click(container.querySelector(minimizeButtonSelector));

    expect(container.querySelector(minimizedPanelSelector)).toBeVisible();
  });
});
