import { action } from '@storybook/addon-actions';
import React, { useRef } from 'react';

import { DebugContainer350 } from 'components/_debug/StorybookContainer';

import { AdditiveSearchCategoryMenu } from './AdditiveSearchCategoryMenu';
import { AdditiveSearchCategory } from './models';

export default {
  title: 'Controls/AoiSearch'
};

const categories: AdditiveSearchCategory<any>[] = [
  {
    name: 'Category 1',
    values: [
      { id: 11, name: 'Item 11' },
      { id: 12, name: 'Item 12' },
      { id: 13, name: 'Item 13' },
      { id: 14, name: 'Item 14' }
    ]
  },
  {
    name: 'Category 2',
    values: [
      { id: 21, name: 'Item 21' },
      { id: 22, name: 'Item 22' },
      { id: 23, name: 'Item 23', disabled: true },
      { id: 24, name: 'Item 24' }
    ]
  }
];

export const CategoryMenu = () => (
  <DebugContainer350>
    <AdditiveSearchCategoryMenu<any>
      categories={categories}
      onSelect={action('onSelect')}
      onCancel={action('onCancel')}
      onAdd={action('onAdd')}
    />
  </DebugContainer350>
);

export const CategoryMenuDisabled = () => (
  <DebugContainer350>
    <AdditiveSearchCategoryMenu<any>
      categories={categories}
      selectedId={11}
      trayButtonsState="disabled"
      onSelect={action('onSelect')}
      onCancel={action('onCancel')}
      onAdd={action('onAdd')}
    />
  </DebugContainer350>
);

export const CategoryMenuBusy = () => (
  <DebugContainer350>
    <AdditiveSearchCategoryMenu<any>
      categories={categories}
      selectedId={11}
      trayButtonsState="busy"
      onSelect={action('onSelect')}
      onCancel={action('onCancel')}
      onAdd={action('onAdd')}
    />
  </DebugContainer350>
);
