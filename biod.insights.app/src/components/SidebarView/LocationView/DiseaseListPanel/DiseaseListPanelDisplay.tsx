/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import React from 'react';
import { Input, List } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { Panels } from 'utils/constants';
import { isMobile } from 'utils/responsive';
import { BdIcon } from 'components/_common/BdIcon';
import { IconButton } from 'components/_controls/IconButton';
import NotFoundMessage from 'components/_controls/Misc/NotFoundMessage';
import { Error } from 'components/Error';
import { MobilePanelSummary } from 'components/MobilePanelSummary';
import { ILoadableProps, IPanelProps, Panel } from 'components/Panel';
import { ISearchTextProps } from 'components/Search';
import { SortBy } from 'components/SortBy';
import { ISortByProps } from 'components/SortBy/SortBy';

import DiseaseCard from './DiseaseCard';

export type DiseaseListPanelDisplayProps = IPanelProps &
  ISortByProps &
  ISearchTextProps &
  ILoadableProps & {
    geonameId;
    diseaseId;
    diseasesList;
    subtitle: string;
    onSelectDisease;
    onSettingsClick;
  };

const DiseaseListPanelDisplay = ({
  activePanel,
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
  subtitleMobile,
  summaryTitle,
  hasError,
  onSelectDisease,

  onSettingsClick,
  onRetryClick,

  isMinimized, // TODO: 633056e0: group panel-related props (and similar)
  onMinimize,
  onClose
}) => {
  const handleOnChange = event => {
    onSearchTextChanged && onSearchTextChanged(event.target.value);
  };

  const reset = () => {
    onSearchTextChanged('');
  };

  const isMobileDevice = isMobile(useBreakpointIndex());
  if (isMobileDevice && activePanel !== Panels.DiseaseListPanel) {
    return null;
  }

  const hasValue = searchText && !!onSearchTextChanged.length;
  const hasVisibleDiseases = !!diseasesList.filter(d => !d.isHidden).length;

  return (
    <Panel
      isAnimated
      isLoading={isLoading}
      title="Diseases"
      subtitle={subtitle}
      subtitleMobile={subtitleMobile}
      toolbar={
        <React.Fragment>
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
            placeholder="Search for diseases"
            fluid
            attached="top"
          >
            <BdIcon name="icon-search" className="prefix" color="sea100" bold />
            <input />
            {hasValue ? (
              <BdIcon
                name="icon-close"
                className="suffix link b5780684"
                color="sea100"
                bold
                onClick={reset}
              />
            ) : null}
          </Input>
        </React.Fragment>
      }
      headerActions={
        !isMobileDevice && (
          <IconButton
            icon="icon-cog"
            color="sea100"
            bold
            tooltipText="Modify the diseases in this list"
            nomargin
            onClick={onSettingsClick}
          />
        )
      }
      summary={<MobilePanelSummary onClick={onClose} summaryTitle={summaryTitle} />}
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      onClose={onClose}
    >
      <List>
        {hasError ? (
          <Error
            title="Something went wrong."
            subtitle="Please check your network connectivity and try again."
            linkText="Click here to retry"
            linkCallback={onRetryClick}
          />
        ) : !diseasesList.length ? (
          <Error
            title="No relevant diseases to your location."
            subtitle="Change your disease settings to Always of Interest to make them relevant to your location."
            linkText="Click here to customize your settings"
            linkCallback={onSettingsClick}
          />
        ) : (
          <React.Fragment>
            {!hasVisibleDiseases && <NotFoundMessage text="Disease not found" />}
            {diseasesList.map(disease => (
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
          </React.Fragment>
        )}
      </List>
    </Panel>
  );
};

export default DiseaseListPanelDisplay;
