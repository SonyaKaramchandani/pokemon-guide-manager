import { Many, ListIteratee } from 'lodash';
import * as dto from 'client/dto';
import { DiseaseAndProximalRiskVM } from './DiseaseModels';

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
    keys: [x => x.name],
    orders: ['asc']
  },
  {
    value: 'country',
    text: 'Country',
    keys: [x => x.country, x => x.name],
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
  DiseaseAndProximalRiskVM,
  DiseaseListLocationViewSortOptionValues
>[] = [
  {
    value: 'disease-name',
    text: 'Alphabetical',
    keys: [x => x.disease.diseaseInformation.name],
    orders: ['asc']
  },
  {
    value: 'predicted-cases-of',
    text: 'Estimated case importations',
    keys: [
      x => RiskModel2AvgMagnitudeForSorting(x.disease.importationRisk),
      x => x.disease.diseaseInformation.name
    ],
    orders: ['desc', 'asc']
  },
  {
    value: 'likelihood',
    text: 'Likelihood of case importation',
    keys: [
      x => RiskModel2AvgProbabilityForSorting(x.disease.importationRisk),
      x => x.disease.diseaseInformation.name
    ],
    orders: ['desc', 'asc']
  },
  {
    value: 'number-of-nearby-cases',
    text: 'Number of nearby cases',
    keys: [x => x.proximalVM && x.proximalVM.totalCases, x => x.disease.diseaseInformation.name],
    orders: ['desc', 'asc']
  },
  {
    value: 'last-updated-date',
    text: 'Last updated',
    keys: [x => x.disease.lastUpdatedEventDate],
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
  DiseaseAndProximalRiskVM,
  DiseaseListGlobalViewSortOptionValues
>[] = [
  {
    value: 'disease-name',
    text: 'Alphabetical',
    keys: [x => x.disease.diseaseInformation.name],
    orders: ['asc']
  },
  {
    value: 'predicted-cases-of',
    text: 'Estimated case exportations',
    keys: [
      x => RiskModel2AvgMagnitudeForSorting(x.disease.exportationRisk),
      x => x.disease.diseaseInformation.name
    ],
    orders: ['desc', 'asc']
  },
  {
    value: 'likelihood',
    text: 'Likelihood of case exportation',
    keys: [
      x => RiskModel2AvgProbabilityForSorting(x.disease.exportationRisk),
      x => x.disease.diseaseInformation.name
    ],
    orders: ['desc', 'asc']
  },
  {
    value: 'number-of-nearby-cases',
    text: 'Number of reported cases',
    keys: [x => x.proximalVM && x.proximalVM.totalCases, x => x.disease.diseaseInformation.name],
    orders: ['desc', 'asc']
  },
  {
    value: 'last-updated-date',
    text: 'Last updated',
    keys: [x => x.disease.lastUpdatedEventDate],
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
    keys: [x => x.eventInformation.title],
    orders: ['asc']
  },
  {
    value: 'predicted-cases-of',
    text: 'Estimated case exportations',
    keys: [x => RiskModel2AvgMagnitudeForSorting(x.exportationRisk), x => x.eventInformation.title],
    orders: ['desc', 'asc']
  },
  {
    value: 'likelihood',
    text: 'Exportation likelihood',
    keys: [
      x => RiskModel2AvgProbabilityForSorting(x.exportationRisk),
      x => x.eventInformation.title
    ],
    orders: ['desc', 'asc']
  },
  {
    value: 'reported-cases',
    text: 'Number of reported cases',
    keys: [x => x.caseCounts.reportedCases],
    orders: ['desc']
  },
  {
    value: 'reported-deaths',
    text: 'Number of deaths',
    keys: [x => x.caseCounts.deaths],
    orders: ['desc']
  },
  {
    value: 'last-updated-date',
    text: 'Last updated',
    keys: [x => x.eventInformation.lastUpdatedDate],
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
    keys: [x => x.eventInformation.title],
    orders: ['asc']
  },
  {
    value: 'predicted-cases-of',
    text: 'Estimated case importations',
    keys: [x => RiskModel2AvgMagnitudeForSorting(x.importationRisk), x => x.eventInformation.title],
    orders: ['desc', 'asc']
  },
  {
    value: 'likelihood',
    text: 'Likelihood of case importation',
    keys: [
      x => RiskModel2AvgProbabilityForSorting(x.importationRisk),
      x => x.eventInformation.title
    ],
    orders: ['desc', 'asc']
  },
  {
    value: 'reported-cases',
    text: 'Number of reported cases',
    keys: [x => x.caseCounts.reportedCases],
    orders: ['desc']
  },
  {
    value: 'reported-deaths',
    text: 'Number of reported deaths',
    keys: [x => x.caseCounts.deaths],
    orders: ['desc']
  },
  {
    value: 'last-updated-date',
    text: 'Last updated',
    keys: [x => x.eventInformation.lastUpdatedDate],
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
    keys: [x => x.eventInformation.title],
    orders: ['asc']
  },
  {
    value: 'predicted-cases-of',
    text: 'Estimated case exportations',
    keys: [x => RiskModel2AvgMagnitudeForSorting(x.exportationRisk), x => x.eventInformation.title],
    orders: ['desc', 'asc']
  },
  {
    value: 'likelihood',
    text: 'Likelihood of case exportation',
    keys: [
      x => RiskModel2AvgProbabilityForSorting(x.exportationRisk),
      x => x.eventInformation.title
    ],
    orders: ['desc', 'asc']
  },
  {
    value: 'reported-cases',
    text: 'Number of reported cases',
    keys: [x => x.caseCounts.reportedCases],
    orders: ['desc']
  },
  {
    value: 'reported-deaths',
    text: 'Number of reported deaths',
    keys: [x => x.caseCounts.deaths],
    orders: ['desc']
  },
  {
    value: 'last-updated-date',
    text: 'Last updated',
    keys: [x => x.eventInformation.lastUpdatedDate],
    orders: ['desc']
  }
];

function RiskModel2AvgMagnitudeForSorting(risk: dto.RiskModel) {
  return risk.isModelNotRun ? -1 : (risk.minMagnitude + risk.maxMagnitude) / 2;
}

function RiskModel2AvgProbabilityForSorting(risk: dto.RiskModel) {
  return risk.isModelNotRun ? -1 : (risk.minProbability + risk.maxProbability) / 2;
}
