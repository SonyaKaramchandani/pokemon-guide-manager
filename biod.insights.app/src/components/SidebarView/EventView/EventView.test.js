import React from 'react';
import { act, render, renderWithRouter, fireEvent, waitForElement } from 'utils/testUtils';
import { useBreakpointIndex } from '@theme-ui/match-media';
import EventApi from 'api/EventApi';
import EventView from './EventView';

describe('EventView', () => {
  const eventTitle = 'MockEventTitle';

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

  const events = {
    data: {
      localCaseCounts: null,
      importationRisk: null,
      exportationRisk: null,
      eventsList: [
        {
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
      ],
      countryPins: []
    }
  };

  test('minimize event listing panel', async () => {
    useBreakpointIndex.mockReturnValue(1);
    EventApi.getEvent = jest.fn().mockResolvedValue(events);

    const { getByTestId } = render(<EventView />);
    await waitForElement(() => getByTestId('minimizeButton'));
    expect(getByTestId('minimizeButton')).toBeVisible();

    fireEvent.click(getByTestId('minimizeButton'));
    expect(getByTestId('minimizedPanel')).toBeVisible();
  });

  test('select an event on event list panel', async () => {
    EventApi.getEvent = jest.fn(params =>
      !params.eventId ? Promise.resolve(events) : Promise.resolve(event)
    );

    const { getByText, getAllByText } = render(<EventView />);
    await waitForElement(() => getByText(eventTitle));

    // select an event on event list panel
    await act(async () => {
      fireEvent.click(getByText(eventTitle));
    });

    // verify event details panel is showing
    expect(getAllByText(eventTitle)).toHaveLength(2);
    expect(getByText('Risk of Importation')).toBeVisible(); // event details panel
  });

  test('minimize event details panel', async () => {
    const minimizeButtonSelector = `[data-testid="panel-${eventTitle}"] [data-testid="minimizeButton"]`;
    const minimizedPanelSelector = `[data-testid="panel-${eventTitle}"] [data-testid="minimizedPanel"]`;

    useBreakpointIndex.mockReturnValue(1);
    EventApi.getEvent = jest.fn(params =>
      !params.eventId ? Promise.resolve(events) : Promise.resolve(event)
    );

    const { container, getByText } = render(<EventView />);
    await waitForElement(() => getByText(eventTitle));

    // select an event on event list panel
    await act(async () => {
      fireEvent.click(getByText(eventTitle));
    });

    // click minimize button on the event details panel
    fireEvent.click(container.querySelector(minimizeButtonSelector));

    expect(container.querySelector(minimizedPanelSelector)).toBeVisible();
  });

  test('close event details panel', async () => {
    const closeButtonSelector = `[data-testid="panel-${eventTitle}"] [data-testid="closeButton"]`;

    useBreakpointIndex.mockReturnValue(1);
    EventApi.getEvent = jest.fn(params =>
      !params.eventId ? Promise.resolve(events) : Promise.resolve(event)
    );

    const { container, getByText } = render(<EventView />);
    await waitForElement(() => getByText(eventTitle));

    // select an event on event list panel
    await act(async () => {
      fireEvent.click(getByText(eventTitle));
    });

    // click close button on the event details panel
    fireEvent.click(container.querySelector(closeButtonSelector));

    // verify event details panel is removed
    expect(container.querySelector(`[data-testid="panel-${eventTitle}"]`)).not.toBeInTheDocument();
  });
});
