/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Input, List } from 'semantic-ui-react';
import { Panel } from 'components/Panel';
import { SortBy } from 'components/SortBy';
import { IconButton } from 'components/_controls/IconButton';
import DiseaseCard from './DiseaseCard';
import { BdIcon } from 'components/_common/BdIcon';
import { Error } from 'components/Error';
import { NotFoundMessage } from 'components/_controls/Misc/NotFoundMessage';
import { MobilePanelSummary } from 'components/MobilePanelSummary';
import { Panels } from 'utils/constants';
import { useBreakpointIndex } from '@theme-ui/match-media';
import { isMobile } from 'utils/responsive';
//=====================================================================================================================================

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
        </>
      }
      headerActions={
        !isMobileDevice && <IconButton
          icon="icon-cog"
          color="sea100"
          bold
          tooltipText="Modify the diseases in this list"
          nomargin
          onClick={onSettingsClick}
        />
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
          <>
            {!hasVisibleDiseases && <NotFoundMessage text="Disease not found"></NotFoundMessage>}
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
          </>
        )}
      </List>
    </Panel>
  );
};

export default DiseaseListPanelDisplay;
