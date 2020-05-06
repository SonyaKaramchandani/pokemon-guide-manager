import React, { useRef } from 'react';
import { action } from '@storybook/addon-actions';
import { BdSearch } from './BdSearch';
import { Button, ButtonGroup } from 'semantic-ui-react';

export default {
  title: 'Controls/Search'
};

export const BdSearchTest = () => (
  <BdSearch
    placeholder="Enter search term"
    isLoading={false}
    onSearchTextChange={x => action('onSearchTextChange', x)}
  />
);
