/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import DiseaseApi from 'api/DiseaseApi';
import { Input, List } from 'semantic-ui-react';
import { Panel } from 'components/Panel';
import { SortBy } from 'components/SortBy';
import { IconButton } from 'components/_controls/IconButton';

import DiseaseCard from './DiseaseCard';
import { BdIcon } from 'components/_common/BdIcon';

const DiseaseListPanelDisplay = ({
  sortBy,
  sortOptions,
  onSelectSortBy,

  searchText,
  onSearchTextChanged,

  isLoading=false,
  geonameId,
  diseaseId,
  diseasesList,
  subtitle,
  onSelectDisease,

  onSettingsClick,

  isMinimized, // TODO: 633056e0: group panel-related props (and similar)
  onMinimize,
  onClose,
}) => {

  return (
    <Panel
      isLoading={isLoading}
      title="Diseases"
      subtitle={subtitle}
      toolbar={
        <>
          <SortBy
            selectedValue={sortBy}
            options={sortOptions}
            onSelect={onSelectSortBy}
            disabled={isLoading}
          />
          <Input
            value={searchText}
            onChange={event => onSearchTextChanged && onSearchTextChanged(event.target.value)}
            icon={<BdIcon name="icon-search" color="sea100" bold />}
            iconPosition="left"
            placeholder="Search for disease"
            fluid
            attached="top"
          />
        </>
      }
      headerActions={
        <IconButton icon="icon-cog" color="sea100" bold onClick={onSettingsClick} />
      }
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      onClose={onClose}
    >
      <List>
        {diseasesList && diseasesList.map(disease => (
          <DiseaseCard
            key={disease.diseaseInformation.id}
            selected={diseaseId}
            geonameId={geonameId}
            {...disease}
            onSelect={() => onSelectDisease && onSelectDisease(disease.diseaseInformation.id, disease)}
          />
        ))}
      </List>
    </Panel>
  );
};

export default DiseaseListPanelDisplay;
