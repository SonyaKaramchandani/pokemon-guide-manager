import React from 'react';
import { action } from '@storybook/addon-actions';
import { LocationListSortOptions } from 'models/SortByOptions';
import SortBy from './SortBy';

export default {
  title: 'Controls/SortBy'
};

export const text = () => (
  <SortBy
    selectedValue={LocationListSortOptions[0].value}
    options={LocationListSortOptions}
    onSelect={action('onSelect')}
  />
);
