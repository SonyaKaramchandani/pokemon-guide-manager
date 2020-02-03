import React, { useRef } from 'react';
import { action } from '@storybook/addon-actions';
import AdditiveSearch from './AdditiveSearch';
import { Button, ButtonGroup } from 'semantic-ui-react';

export default {
  title: 'Controls/AdditiveSearch'
};

const categories = [
  {
    title: 'Category 1',
    values: [
      { id: '11', name: 'Item 11' },
      { id: '12', name: 'Item 12' },
      { id: '13', name: 'Item 13' },
      { id: '14', name: 'Item 14' }
    ]
  },
  {
    title: 'Category 2',
    values: [
      { id: '21', name: 'Item 21' },
      { id: '22', name: 'Item 22' },
      { id: '23', name: 'Item 23' },
      { id: '24', name: 'Item 24' }
    ]
  }
];

export const search_and_add = () => (
  <AdditiveSearch
    placeholder="Enter search term"
    isLoading={false}
    categories={categories}
    onAdd={action('onAdd')}
    onSelect={action(`onSelect`)}
    onSearch={action(`onSearch`)}
  />
);

export const add_in_progress = () => (
  <AdditiveSearch
    placeholder="Enter search term"
    isLoading={false}
    categories={categories}
    onAdd={action('onAdd')}
    onSelect={action(`onSelect`)}
    onSearch={action(`onSearch`)}
    isAddInProgress={true}
    addButtonLabel="Add"
  />
);
