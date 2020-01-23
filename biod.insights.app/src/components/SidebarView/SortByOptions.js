import orderBy from 'lodash.orderby';

export const LocationListSortOptions = [
  {
    value: 'name',
    text: 'Alphabetical',
    keys: ['name'],
    orders: ['asc']
  },
  {
    value: 'country',
    text: 'Alphabetical by country',
    keys: ['country', 'name'],
    orders: ['asc', 'asc']
  }
];
export const DiseaseListLocationViewSortOptions = [
  {
    value: 'disease-name',
    text: 'Alphabetical',
    keys: ['diseaseInformation.name'],
    orders: ['asc']
  },
  {
    value: 'importation-number',
    text: 'Expected number of importation',
    keys: ['importationRisk.maxMagnitude', 'diseaseInformation.name'],
    orders: ['desc', 'asc']
  },
  {
    value: 'importation-likelyhood',
    text: 'Importation likelihood',
    keys: ['importationRisk.maxProbability', 'diseaseInformation.name'],
    orders: ['desc', 'asc']
  },
  {
    value: 'number-of-nearby-cases',
    text: 'Number of Nearby Cases',
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
export const DiseaseListGlobalViewSortOptions = [
  {
    value: 'disease-name',
    text: 'Alphabetical',
    keys: ['diseaseInformation.name'],
    orders: ['asc']
  },
  {
    value: 'exportation-number',
    text: 'Expected number of exportation',
    keys: ['exportationRisk.maxMagnitude', 'diseaseInformation.name'],
    orders: ['desc', 'asc']
  },
  {
    value: 'exportation-likelyhood',
    text: 'Exportation likelihood',
    keys: ['exportationRisk.maxProbability', 'diseaseInformation.name'],
    orders: ['desc', 'asc']
  },
  {
    value: 'number-of-nearby-cases',
    text: 'Total Number of Cases',
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
export const EventListSortOptions = [
  {
    value: 'last-updated-date',
    text: 'Last updated',
    keys: ['eventInformation.lastUpdatedDate'],
    orders: ['desc']
  },
  {
    value: 'event-title',
    text: 'Alphabetical',
    keys: ['eventInformation.title'],
    orders: ['asc']
  },
  {
    value: 'exportation-number',
    text: 'Expected number of exportations',
    keys: ['exportationRisk.maxMagnitude', 'eventInformation.title'],
    orders: ['desc', 'asc']
  },
  {
    value: 'exportation-likelyhood',
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
  }
];

export const DiseaseEventListLocationViewSortOptions = [
  {
    value: 'last-updated-date',
    text: 'Last updated',
    keys: ['eventInformation.lastUpdatedDate'],
    orders: ['desc']
  },
  {
    value: 'event-title',
    text: 'Alphabetical',
    keys: ['eventInformation.title'],
    orders: ['asc']
  },
  {
    value: 'importation-number',
    text: 'Expected number of importation',
    keys: ['importationRisk.maxMagnitude', 'diseaseInformation.name'],
    orders: ['desc', 'asc']
  },
  {
    value: 'importation-likelyhood',
    text: 'Importation likelihood',
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
    text: 'Number of deaths',
    keys: ['caseCounts.deaths'],
    orders: ['desc']
  }
];

export const DiseaseEventListGlobalViewSortOptions = [
  {
    value: 'last-updated-date',
    text: 'Last updated',
    keys: ['eventInformation.lastUpdatedDate'],
    orders: ['desc']
  },
  {
    value: 'event-title',
    text: 'Alphabetical',
    keys: ['eventInformation.title'],
    orders: ['asc']
  },
  {
    value: 'exportation-number',
    text: 'Expected number of exportation',
    keys: ['exportationRisk.maxMagnitude', 'diseaseInformation.name'],
    orders: ['desc', 'asc']
  },
  {
    value: 'exportation-likelyhood',
    text: 'Exportation likelihood',
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
    text: 'Number of deaths',
    keys: ['caseCounts.deaths'],
    orders: ['desc']
  }
];

export const sort = ({ items, sortOptions, sortBy }) => {
  const sort = sortOptions.find(so => so.value === sortBy);
  return orderBy(items, sort.keys, sort.orders);
};
