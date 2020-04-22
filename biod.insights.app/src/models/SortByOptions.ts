import { Many, ListIteratee } from 'lodash';
import * as dto from 'client/dto';

/**
 * @template T - Type of array elements
 * @template V - Option value type, i.e. the literal key by which to sort
 */
export interface SortByOption<T, V> {
  value: V;
  text: string;
  keys: Many<ListIteratee<T>>;
  orders: ('asc' | 'desc')[];
}

export const DefaultSortOptionValue = 'last-updated-date';

export type LocationListSortOptionValues = 'name' | 'country';
export const LocationListSortOptions: SortByOption<
  dto.GetGeonameModel,
  LocationListSortOptionValues
>[] = [
  {
    value: 'name',
    text: 'Alphabetical',
    keys: ['name'],
    orders: ['asc']
  },
  {
    value: 'country',
    text: 'Country',
    keys: ['country', 'name'],
    orders: ['asc', 'asc']
  }
];

export type DiseaseListLocationViewSortOptionValues =
  | 'disease-name'
  | 'predicted-cases-of'
  | 'likelihood'
  | 'number-of-nearby-cases'
  | 'last-updated-date';
export const DiseaseListLocationViewSortOptions: SortByOption<
  dto.DiseaseRiskModel,
  DiseaseListLocationViewSortOptionValues
>[] = [
  {
    value: 'disease-name',
    text: 'Alphabetical',
    keys: ['diseaseInformation.name'],
    orders: ['asc']
  },
  {
    value: 'predicted-cases-of',
    text: 'Estimated case importations',
    keys: [
      x => (x.importationRisk.minMagnitude + x.importationRisk.maxMagnitude) / 2,
      x => x.diseaseInformation.name
    ],
    orders: ['desc', 'asc']
  },
  {
    value: 'likelihood',
    text: 'Likelihood of case importation',
    keys: [
      x => (x.importationRisk.minProbability + x.importationRisk.maxProbability) / 2,
      x => x.diseaseInformation.name
    ],
    orders: ['desc', 'asc']
  },
  {
    value: 'number-of-nearby-cases',
    text: 'Number of nearby cases',
    keys: ['caseCounts.reportedCases', 'diseaseInformation.name'],
    orders: ['desc', 'asc']
  },
  {
    value: 'last-updated-date',
    text: 'Last updated',
    keys: ['lastUpdatedEventDate'],
    orders: ['desc']
  }
];

export type DiseaseListGlobalViewSortOptionValues =
  | 'disease-name'
  | 'predicted-cases-of'
  | 'likelihood'
  | 'number-of-nearby-cases'
  | 'last-updated-date';
export const DiseaseListGlobalViewSortOptions: SortByOption<
  dto.DiseaseRiskModel,
  DiseaseListGlobalViewSortOptionValues
>[] = [
  {
    value: 'disease-name',
    text: 'Alphabetical',
    keys: ['diseaseInformation.name'],
    orders: ['asc']
  },
  {
    value: 'predicted-cases-of',
    text: 'Estimated case exportations',
    keys: [
      x => (x.exportationRisk.minMagnitude + x.exportationRisk.maxMagnitude) / 2,
      x => x.diseaseInformation.name
    ],
    orders: ['desc', 'asc']
  },
  {
    value: 'likelihood',
    text: 'Likelihood of case exportation',
    keys: [
      x => (x.exportationRisk.minProbability + x.exportationRisk.maxProbability) / 2,
      x => x.diseaseInformation.name
    ],
    orders: ['desc', 'asc']
  },
  {
    value: 'number-of-nearby-cases',
    text: 'Number of reported cases',
    keys: ['caseCounts.reportedCases', 'diseaseInformation.name'],
    orders: ['desc', 'asc']
  },
  {
    value: 'last-updated-date',
    text: 'Last updated',
    keys: ['lastUpdatedEventDate'],
    orders: ['desc']
  }
];

export type EventListSortOptionValues =
  | 'event-title'
  | 'predicted-cases-of'
  | 'likelihood'
  | 'reported-cases'
  | 'reported-deaths'
  | 'last-updated-date';
export const EventListSortOptions: SortByOption<dto.GetEventModel, EventListSortOptionValues>[] = [
  {
    value: 'event-title',
    text: 'Alphabetical',
    keys: ['eventInformation.title'],
    orders: ['asc']
  },
  {
    value: 'predicted-cases-of',
    text: 'Estimated case exportations',
    keys: [
      x => (x.exportationRisk.minMagnitude + x.exportationRisk.maxMagnitude) / 2,
      x => x.eventInformation.title
    ],
    orders: ['desc', 'asc']
  },
  {
    value: 'likelihood',
    text: 'Exportation likelihood',
    keys: [
      x => (x.exportationRisk.minProbability + x.exportationRisk.maxProbability) / 2,
      x => x.eventInformation.title
    ],
    orders: ['desc', 'asc']
  },
  {
    value: 'reported-cases',
    text: 'Number of reported cases',
    keys: ['caseCounts.reportedCases'],
    orders: ['desc']
  },
  {
    value: 'reported-deaths',
    text: 'Number of deaths',
    keys: ['caseCounts.deaths'],
    orders: ['desc']
  },
  {
    value: 'last-updated-date',
    text: 'Last updated',
    keys: ['eventInformation.lastUpdatedDate'],
    orders: ['desc']
  }
];

export type DiseaseEventListLocationViewSortOptionValues =
  | 'event-title'
  | 'predicted-cases-of'
  | 'likelihood'
  | 'reported-cases'
  | 'reported-deaths'
  | 'last-updated-date';
export const DiseaseEventListLocationViewSortOptions: SortByOption<
  dto.GetEventModel,
  DiseaseEventListLocationViewSortOptionValues
>[] = [
  {
    value: 'event-title',
    text: 'Alphabetical',
    keys: ['eventInformation.title'],
    orders: ['asc']
  },
  {
    value: 'predicted-cases-of',
    text: 'Estimated case importations',
    keys: [
      x => (x.importationRisk.minMagnitude + x.importationRisk.maxMagnitude) / 2,
      x => x.eventInformation.title
    ],
    orders: ['desc', 'asc']
  },
  {
    value: 'likelihood',
    text: 'Likelihood of case importation',
    keys: [
      x => (x.importationRisk.minProbability + x.importationRisk.maxProbability) / 2,
      x => x.eventInformation.title
    ],
    orders: ['desc', 'asc']
  },
  {
    value: 'reported-cases',
    text: 'Number of reported cases',
    keys: ['caseCounts.reportedCases'],
    orders: ['desc']
  },
  {
    value: 'reported-deaths',
    text: 'Number of reported deaths',
    keys: ['caseCounts.deaths'],
    orders: ['desc']
  },
  {
    value: 'last-updated-date',
    text: 'Last updated',
    keys: ['eventInformation.lastUpdatedDate'],
    orders: ['desc']
  }
];

export type DiseaseEventListGlobalViewSortOptionValues =
  | 'event-title'
  | 'predicted-cases-of'
  | 'likelihood'
  | 'reported-cases'
  | 'reported-deaths'
  | 'last-updated-date';
export const DiseaseEventListGlobalViewSortOptions: SortByOption<
  dto.GetEventModel,
  DiseaseEventListGlobalViewSortOptionValues
>[] = [
  {
    value: 'event-title',
    text: 'Alphabetical',
    keys: ['eventInformation.title'],
    orders: ['asc']
  },
  {
    value: 'predicted-cases-of',
    text: 'Estimated case exportations',
    keys: [
      x => (x.exportationRisk.minMagnitude + x.exportationRisk.maxMagnitude) / 2,
      x => x.eventInformation.title
    ],
    orders: ['desc', 'asc']
  },
  {
    value: 'likelihood',
    text: 'Likelihood of case exportation',
    keys: [
      x => (x.exportationRisk.minProbability + x.exportationRisk.maxProbability) / 2,
      x => x.eventInformation.title
    ],
    orders: ['desc', 'asc']
  },
  {
    value: 'reported-cases',
    text: 'Number of reported cases',
    keys: ['caseCounts.reportedCases'],
    orders: ['desc']
  },
  {
    value: 'reported-deaths',
    text: 'Number of reported deaths',
    keys: ['caseCounts.deaths'],
    orders: ['desc']
  },
  {
    value: 'last-updated-date',
    text: 'Last updated',
    keys: ['eventInformation.lastUpdatedDate'],
    orders: ['desc']
  }
];
