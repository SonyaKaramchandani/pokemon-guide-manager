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
    text: 'Country',
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
    value: 'predicted-cases-of',
    text: 'Predicted case importations',
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
export const DiseaseListGlobalViewSortOptions = [
  {
    value: 'disease-name',
    text: 'Alphabetical',
    keys: ['diseaseInformation.name'],
    orders: ['asc']
  },
  {
    value: 'predicted-cases-of',
    text: 'Predicted case exportations',
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
    text: 'Total number of cases',
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
    value: 'event-title',
    text: 'Alphabetical',
    keys: ['eventInformation.title'],
    orders: ['asc']
  },
  {
    value: 'predicted-cases-of',
    text: 'Predicted case exportations',
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

export const DiseaseEventListLocationViewSortOptions = [
  {
    value: 'event-title',
    text: 'Alphabetical',
    keys: ['eventInformation.title'],
    orders: ['asc']
  },
  {
    value: 'predicted-cases-of',
    text: 'Predicted case importations',
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

export const DiseaseEventListGlobalViewSortOptions = [
  {
    value: 'event-title',
    text: 'Alphabetical',
    keys: ['eventInformation.title'],
    orders: ['asc']
  },
  {
    value: 'predicted-cases-of',
    text: 'Predicted case exportations',
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

export const sort = ({ items, sortOptions, sortBy }) => {
  const sort = sortOptions.find(so => so.value === sortBy);
  return orderBy(items, sort.keys, sort.orders);
};
