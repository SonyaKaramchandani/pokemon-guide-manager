import React from 'react';
import { action } from '@storybook/addon-actions';
import DiseaseListPanelDisplay from './DiseaseListPanelDisplay';
import { DebugContainer4BdPanel } from 'components/_debug/StorybookContainer';
import { Geoname } from 'utils/constants';
import {
  DiseaseListLocationViewSortOptions as locationSortOptions,
  DiseaseListGlobalViewSortOptions as globalSortOptions,
} from 'components/SidebarView/SortByOptions';
import { mockDiseaseListProcessed } from '__mocks__/dtoSamples';


export default {
  title: 'PANELS/DiseaseListPanel'
};

const props = {
  geonameId: 0,
  diseasesList: mockDiseaseListProcessed,
  onSelect: action('onSelect'),
  onSettingsClick: action('onSettingsClick'),
  onSelectSortBy: action('onSelectSortBy'),
  onSearchTextChanged: action('onSearchTextChanged'),
  onClose: action('onClose'),
  onMinimize: action('onMinimize'),
};
const sortOptions = (props.geonameId === Geoname.GLOBAL_VIEW)
  ? globalSortOptions
  : locationSortOptions;

export const test =  () => {
  return (
    <DebugContainer4BdPanel>
      <DiseaseListPanelDisplay
        {...props}
        sortOptions={sortOptions}
        sortBy={sortOptions[0].value}
      />
    </DebugContainer4BdPanel>
  );
}

export const loading =  () => {
  return (
    <DebugContainer4BdPanel>
      <DiseaseListPanelDisplay
        {...props}
        sortOptions={sortOptions}
        sortBy={sortOptions[0].value}
        isLoading={true}
      />
    </DebugContainer4BdPanel>
  );
}