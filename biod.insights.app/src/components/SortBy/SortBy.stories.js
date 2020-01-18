import React from 'react';
import { action } from '@storybook/addon-actions';
import SortBy from './SortBy';
import { LocationListSortOptions } from 'components/SidebarView/SortByOptions';

export default {
  title: 'Controls/SortBy'
};

export const text = () => <SortBy
  selectedValue={LocationListSortOptions[0].value}
  options={LocationListSortOptions}
  onSelect={action('onSelect')}
/>;
