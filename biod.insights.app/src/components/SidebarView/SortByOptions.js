import orderBy from 'lodash.orderby';

export const LocationListSortOptions = [{ order: 'asc', value: 'name', text: 'Alphabetic' }];
export const DiseaseListSortOptions = [
  { order: 'asc', value: 'diseaseInformation.name', text: 'Alphabetic' }
];

export const sort = ({ items, sortOptions, sortBy }) => {
  const sort = sortOptions.find(o => o.value === sortBy);
  return orderBy(items, [sort.value], [sort.order]);
};
