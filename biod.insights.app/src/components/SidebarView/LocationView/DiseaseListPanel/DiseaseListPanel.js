/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import DiseaseApi from 'api/DiseaseApi';
import { Input, List } from 'semantic-ui-react';
import { Panel } from 'components/Panel';
import { SortBy } from 'components/SortBy';
import { SvgButton } from 'components/_controls/SvgButton';
import SettingsSvg from 'assets/settings.svg';
import {
  DiseaseListLocationViewSortOptions as locationSortOptions,
  DiseaseListGlobalViewSortOptions as globalSortOptions,
  sort
} from 'components/SidebarView/SortByOptions';
import DiseaseCard from './DiseaseCard';
import { navigateToCustomSettingsUrl } from 'components/Navigationbar';
import { Geoname } from 'utils/constants';
import { BdIcon } from 'components/_common/BdIcon';
import esriMap from 'map';
import eventsView from 'map/events';

const filterDiseases = (searchText, diseases) => {
  const searchRegExp = new RegExp(searchText, 'i');
  return searchText.length
    ? diseases.filter(({ diseaseInformation: { name } }) => searchRegExp.test(name))
    : diseases;
};

const DiseaseListPanel = ({ geonameId, diseaseId, onSelect, onClose, isMinimized, onMinimize }) => {
  const [diseases, setDiseases] = useState([]);
  const [diseasesCaseCounts, setDiseasesCaseCounts] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [sortBy, setSortBy] = useState(locationSortOptions[0].value);
  const [sortOptions, setSortOptions] = useState(locationSortOptions);
  const [searchText, setSearchText] = useState('');

  useEffect(() => {
    if (geonameId === Geoname.GLOBAL_VIEW) {
      setSortOptions(globalSortOptions);
    } else {
      setSortOptions(locationSortOptions);
    }
  }, [geonameId]);

  useEffect(() => {
    setIsLoading(true);
    DiseaseApi.getDiseaseRiskByLocation(geonameId === Geoname.GLOBAL_VIEW ? {} : { geonameId })
      .then(({ data: { diseaseRisks, countryPins } }) => {
        setDiseases(diseaseRisks);
        eventsView.updateEventView(countryPins, geonameId);
        esriMap.showEventsView();
      })
      .finally(() => {
        setIsLoading(false);
      });
  }, [geonameId, setIsLoading, setDiseases]);

  useEffect(() => {
    Promise.all(
      diseases.map(d =>
        DiseaseApi.getDiseaseCaseCount({
          diseaseId: d.diseaseInformation.id,
          geonameId: geonameId === Geoname.GLOBAL_VIEW ? null : geonameId
        }).catch(e => e)
      )
    ).then(responses => {
      if (responses.length) {
        setDiseasesCaseCounts(
          responses.map(r => {
            const diseaseId = r.config.params.diseaseId;
            return { ...r.data, diseaseId };
          })
        );
      }
    });
  }, [geonameId, diseases, setDiseasesCaseCounts, setIsLoading]);

  const handleOnSettingsClick = () => {
    navigateToCustomSettingsUrl();
  };

  const processedDiseases = sort({
    items: filterDiseases(searchText, diseases).map(s => ({
      ...s,
      caseCounts: diseasesCaseCounts.find(d => d.diseaseId === s.diseaseInformation.id)
    })),
    sortOptions,
    sortBy
  });

  return (
    <Panel
      isLoading={isLoading}
      title="Diseases"
      onClose={onClose}
      toolbar={
        <>
          <SortBy
            selectedValue={sortBy}
            options={sortOptions}
            onSelect={sortBy => setSortBy(sortBy)}
            disabled={isLoading}
          />
          <Input
            value={searchText}
            onChange={event => setSearchText(event.target.value)}
            icon={<BdIcon name="icon-search" color="sea100" bold />}
            iconPosition="left"
            placeholder="Search for disease"
            fluid
            loading={isLoading}
            attached="top"
          />
        </>
      }
      headerActions={
        <BdIcon
          name="icon-cog"
          color="sea100"
          bold
          sx={{ cursor: 'pointer' }}
          onClick={handleOnSettingsClick}
        />
      }
      isMinimized={isMinimized}
      onMinimize={onMinimize}
    >
      <List>
        {processedDiseases.map(disease => (
          <DiseaseCard
            key={disease.diseaseInformation.id}
            selected={diseaseId}
            {...disease}
            onSelect={() => onSelect(disease.diseaseInformation.id, disease)}
          />
        ))}
      </List>
    </Panel>
  );
};

export default DiseaseListPanel;
