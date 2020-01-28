/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Input, List } from 'semantic-ui-react';
import { Panel } from 'components/Panel';
import { SortBy } from 'components/SortBy';
import { Typography } from 'components/_common/Typography';
import { IconButton } from 'components/_controls/IconButton';
import DiseaseCard from './DiseaseCard';
import { BdIcon } from 'components/_common/BdIcon';
import { Error } from 'components/Error';

//=====================================================================================================================================

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
  hasError,
  onSelectDisease,

  onSettingsClick,
  onRetryClick,

  isMinimized, // TODO: 633056e0: group panel-related props (and similar)
  onMinimize,
  onClose
}) => {
  const EmptyDiseasesList = (title, subtitle, linkText, linkCallback) => (
    <>
      <Typography
        variant="subtitle2"
        color="deepSea50"
        sx={{
          padding: '60px 0px 0px 0px',
          textAlign: 'center'
        }}
      >
        {title}
      </Typography>
      <Typography
        variant="caption"
        color="deepSea50"
        sx={{
          padding: '2px 40px',
          textAlign: 'center'
        }}
      >
        {subtitle}
      </Typography>
      <div onClick={linkCallback} sx={{ mt: '5px' }}>
        <Typography
          variant="body2"
          color="sea90"
          sx={{
            paddingTop: '12px',
            textAlign: 'center',
            textDecoration: 'underline',
            cursor: 'pointer',
            '&:hover': {
              color: t => t.colors.sea60,
              textDecoration: 'underline',
              transition: 'ease .3s'
            }
          }}
        >
          {linkText}
        </Typography>
      </div>
    </>
  );

  const handleOnChange = event => {
    onSearchTextChanged && onSearchTextChanged(event.target.value);
  };

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
      headerActions={<IconButton icon="icon-cog" color="sea100" bold onClick={onSettingsClick} />}
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
        ) : diseasesList.length ? (
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
          ))
        ) : (
          <Error
            title="No relevant diseases to your location."
            subtitle="Change your disease settings to Always of Interest to make them relevant to your location."
            linkText="Click here to customize your settings"
            linkCallback={onSettingsClick}
          />
        )}
      </List>
    </Panel>
  );
};

export default DiseaseListPanelDisplay;
