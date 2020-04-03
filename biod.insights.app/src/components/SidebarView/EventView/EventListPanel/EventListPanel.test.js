import React from 'react';
import { act, render, renderWithRouter, fireEvent, waitForElement } from 'utils/testUtils';
import { useBreakpointIndex } from '@theme-ui/match-media';
import EventListPanel from './EventListPanel';
import { Geoname } from 'utils/constants';
import {
  EventListSortOptions,
  DiseaseEventListLocationViewSortOptions,
  DiseaseEventListGlobalViewSortOptions,
  DefaultSortOptionValue
} from 'components/SortBy/SortByOptions';

describe('EventListPanel', () => {
  const eventTitle = 'MockEventTitle';
  const eventId = 123;
  const events = {
    localCaseCounts: null,
    importationRisk: null,
    exportationRisk: null,
    eventsList: [
      {
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
        airports: {
          sourceAirports: [],
          destinationAirports: []
        },
        diseaseInformation: {}
      }
    ],
    countryPins: []
  };

  test('stand alone event listing', async () => {
    useBreakpointIndex.mockReturnValue(1);

    const { getByText } = render(<EventListPanel isStandAlone={true} events={events} />);
    expect(getByText('My Events')).toBeVisible();
    expect(getByText(eventTitle)).toBeVisible();
  });

  describe('container', () => {
    test('disease event listing', async () => {
      useBreakpointIndex.mockReturnValue(1);

      const { queryByText, getByText } = render(
        <EventListPanel isStandAlone={false} events={events} />
      );
      expect(queryByText('My Events')).not.toBeInTheDocument();
    });

    test('global geoname', async () => {
      useBreakpointIndex.mockReturnValue(1);

      const { getByText, getAllByText } = render(
        <EventListPanel isStandAlone={false} events={events} geonameId={Geoname.GLOBAL_VIEW} />
      );

      fireEvent.click(getByText('Sort by'));
      DiseaseEventListGlobalViewSortOptions.forEach(option =>
        expect(getAllByText(option.text).pop()).toBeVisible()
      );
    });
  });
});
