import orderBy from 'lodash.orderby';

export const LocationListSortOptions = [
  { value: 'name', text: 'Alphabetical', keys: ['name'], orders: ['asc'] },
  {
    value: 'country',
    text: 'Alphabetical by country',
    keys: ['country', 'name'],
    orders: ['asc', 'asc']
  }
];
export const DiseaseListSortOptions = [
  {
    value: 'importation-risk',
    text: 'Risk of importation',
    keys: ['importationRisk.maxMagnitude', 'diseaseInformation.name'],
    orders: ['desc', 'asc']
  },
  {
    value: 'disease-name',
    text: 'Alphabetical',
    keys: ['diseaseInformation.name'],
    orders: ['asc']
  }
];
export const EventListSortOptions = [
  {
    value: 'event-title',
    text: 'Alphabetical',
    keys: ['eventInformation.title'],
    orders: ['asc']
  }
];

export const sort = ({ items, sortOptions, sortBy }) => {
  const sort = sortOptions.find(so => so.value === sortBy);
  return orderBy(items, sort.keys, sort.orders);
};
