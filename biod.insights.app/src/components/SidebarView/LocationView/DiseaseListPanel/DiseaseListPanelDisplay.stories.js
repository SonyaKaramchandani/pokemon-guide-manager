import React, { useState } from 'react';
import { action } from '@storybook/addon-actions';
import DiseaseListPanelDisplay from './DiseaseListPanelDisplay';
import { DebugContainer4BdPanel } from 'components/_debug/StorybookContainer';
import { Geoname } from 'utils/constants';
import {
  DiseaseListLocationViewSortOptions as locationSortOptions,
  DiseaseListGlobalViewSortOptions as globalSortOptions
} from 'models/SortByOptions';
import { mockDiseaseListProcessed } from '__mocks__/dtoSamples';
import { containsNoCaseNoLocale } from 'utils/stringHelpers';

export default {
  title: 'PANELS/DiseaseListPanel'
};

const geonameId = 0;
const sortOptions = geonameId === Geoname.GLOBAL_VIEW ? globalSortOptions : locationSortOptions;
const props = {
  geonameId: geonameId,
  diseasesList: mockDiseaseListProcessed,
  sortOptions: sortOptions,
  sortBy: sortOptions[0].value,
  onSelect: action('onSelect'),
  onSettingsClick: action('onSettingsClick'),
  onSelectSortBy: action('onSelectSortBy'),
  onSearchTextChanged: action('onSearchTextChanged'),
  onClose: action('onClose'),
  onMinimize: action('onMinimize')
};

export const test = () => {
  return (
    <DebugContainer4BdPanel>
      <DiseaseListPanelDisplay {...props} />
    </DebugContainer4BdPanel>
  );
};

export const noResults = () => {
  return (
    <DebugContainer4BdPanel>
      <DiseaseListPanelDisplay {...props} diseasesList={[]} />
    </DebugContainer4BdPanel>
  );
};

export const loading = () => {
  return (
    <DebugContainer4BdPanel>
      <DiseaseListPanelDisplay {...props} isLoading={true} />
    </DebugContainer4BdPanel>
  );
};

export const TestE2E = () => {
  const [searchText, setSearchText] = useState('');
  const processedDiseases = props.diseasesList.map(d => ({
    ...d,
    isHidden: !containsNoCaseNoLocale(d.diseaseInformation.name, searchText)
  }));

  return (
    <DebugContainer4BdPanel>
      <DiseaseListPanelDisplay
        {...props}
        diseasesList={processedDiseases}
        searchText={searchText}
        onSearchTextChanged={setSearchText}
      />
    </DebugContainer4BdPanel>
  );
};
