/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
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

  isLoading = false,
  geonameId,
  diseaseId,
  diseasesList,
  subtitle,
  onSelectDisease,

  onSettingsClick,

  isMinimized, // TODO: 633056e0: group panel-related props (and similar)
  onMinimize,
  onClose
}) => {

  const handleOnChange = event => {
    onSearchTextChanged && onSearchTextChanged(event.target.value)};

  const reset = () => {
    onSearchTextChanged('');
  };

  const hasValue = searchText && !!onSearchTextChanged.length;

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
            icon
            className="bd-2-icons"
            value={searchText}
            onChange={handleOnChange}
            placeholder="Search for disease"
            fluid
            attached="top"
          >
            <BdIcon name="icon-search" className="prefix" color="sea100" bold />
            <input />
            { hasValue ? (
              <BdIcon name="icon-close" className="suffix link b5780684" color="sea100" bold onClick={reset} />
            ) : null }
          </Input>
        </>
      }
      headerActions={<IconButton icon="icon-cog" color="sea100" bold onClick={onSettingsClick} />}
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      onClose={onClose}
    >
      <List>
        {diseasesList &&
          diseasesList.map(disease => (
            <DiseaseCard
              isHidden={disease.isHidden}
              key={disease.diseaseInformation.id}
              selected={diseaseId}
              geonameId={geonameId}
              {...disease}
              onSelect={() =>
                onSelectDisease && onSelectDisease(disease.diseaseInformation.id, disease)
              }
            />
          ))}
      </List>
    </Panel>
  );
};

export default DiseaseListPanelDisplay;
