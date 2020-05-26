import { action } from '@storybook/addon-actions';
import React, { useRef } from 'react';

import { DebugContainer350 } from 'components/_debug/StorybookContainer';

import { BdSearch } from './BdSearch';

export default {
  title: 'Controls/BdSearch'
};

export const BdSearchTest = () => (
  <DebugContainer350>
    <BdSearch
      placeholder="Enter search term"
      onSearchTextChange={x => action('onSearchTextChange', x)}
    />
  </DebugContainer350>
);
