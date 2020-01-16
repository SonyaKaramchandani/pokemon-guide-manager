/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import DiseaseApi from 'api/DiseaseApi';
import { Input, List } from 'semantic-ui-react';
import { Panel } from 'components/Panel';
import { SortBy } from 'components/SortBy';
import { SvgButton } from 'components/_controls/SvgButton';
import SettingsSvg from 'assets/settings.svg';
import { DiseaseListSortOptions as sortOptions, sort } from 'components/SidebarView/SortByOptions';
import DiseaseCard from './DiseaseCard';
import { navigateToCustomSettingsUrl } from 'components/Navigationbar';
import { Geoname } from 'utils/constants';
import { BdIcon } from 'components/_common/BdIcon';

const filterDiseases = (searchText, diseases) => {
  return searchText.length
    ? diseases.filter(({ diseaseInformation: { name } }) => name.toLowerCase().includes(searchText))
    : diseases;
};

const DiseaseListPanel = ({ geonameId, diseaseId, onSelect, onClose }) => {
  const [diseases, setDiseases] = useState([]);
  const [diseasesCaseCounts, setDiseasesCaseCounts] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [sortBy, setSortBy] = useState(sortOptions[0].value);
  const [searchText, setSearchText] = useState('');

  useEffect(() => {
    setIsLoading(true);
    DiseaseApi.getDiseaseRiskByLocation({ geonameId: geonameId === Geoname.GLOBAL_VIEW ? null : geonameId })
      .then(({ data: diseases }) => {
        setDiseases(diseases);
      })
      .finally(() => {
        setIsLoading(false);
      });
  }, [geonameId, setIsLoading, setDiseases]);

  useEffect(() => {
    Promise.all(
      diseases.map(d =>
        DiseaseApi.getDiseaseCaseCount({ diseaseId: d.diseaseInformation.id, geonameId: geonameId === Geoname.GLOBAL_VIEW ? null : geonameId }).catch(
          e => e
        )
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
        <SortBy
          selectedValue={sortBy}
          options={sortOptions}
          onSelect={sortBy => setSortBy(sortBy)}
          disabled={isLoading}
        />
      }
      headerActions={<SvgButton src={SettingsSvg} onClick={handleOnSettingsClick} />}
      // width={350}
    >
      <Input
        value={searchText}
        onChange={event => setSearchText(event.target.value)}
        icon= {<BdIcon name="icon-search" color="sea100" bold />}
        iconPosition="left"
        placeholder="Search for disease"
        fluid
        loading={isLoading}
        attached="top"
      />
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
