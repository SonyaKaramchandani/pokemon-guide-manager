export interface SortByOption<T> {
  value: T;
  text: string;
  keys: string[];
  orders: ('asc' | 'desc')[];
}

export const DefaultSortOptionValue = 'last-updated-date';

export type LocationListSortOptionValues = 'name' | 'country';
export const LocationListSortOptions: SortByOption<LocationListSortOptionValues>[] = [
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
    keys: ['importationRisk.maxMagnitude', 'diseaseInformation.name'],
    orders: ['desc', 'asc']
  },
  {
    value: 'likelihood',
    text: 'Likelihood of case importation',
    keys: ['importationRisk.maxProbability', 'diseaseInformation.name'],
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
    keys: ['exportationRisk.maxMagnitude', 'diseaseInformation.name'],
    orders: ['desc', 'asc']
  },
  {
    value: 'likelihood',
    text: 'Likelihood of case exportation',
    keys: ['exportationRisk.maxProbability', 'diseaseInformation.name'],
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
export const EventListSortOptions: SortByOption<EventListSortOptionValues>[] = [
  {
    value: 'event-title',
    text: 'Alphabetical',
    keys: ['eventInformation.title'],
    orders: ['asc']
  },
  {
    value: 'predicted-cases-of',
    text: 'Estimated case exportations',
    keys: ['exportationRisk.maxMagnitude', 'eventInformation.title'],
    orders: ['desc', 'asc']
  },
  {
    value: 'likelihood',
    text: 'Exportation likelihood',
    keys: ['exportationRisk.maxProbability', 'eventInformation.title'],
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
    keys: ['importationRisk.maxMagnitude', 'diseaseInformation.name'],
    orders: ['desc', 'asc']
  },
  {
    value: 'likelihood',
    text: 'Likelihood of case importation',
    keys: ['importationRisk.maxProbability', 'diseaseInformation.name'],
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
    keys: ['exportationRisk.maxMagnitude', 'diseaseInformation.name'],
    orders: ['desc', 'asc']
  },
  {
    value: 'likelihood',
    text: 'Likelihood of case exportation',
    keys: ['exportationRisk.maxProbability', 'diseaseInformation.name'],
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