/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import DiseaseApi from 'api/DiseaseApi';
import { Input, List } from 'semantic-ui-react';
import { Panel } from 'components/Panel';
import { SortBy } from 'components/SortBy';
import { IconButton } from 'components/_controls/IconButton';

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
import DiseaseListPanelDisplay from './DiseaseListPanelDisplay';


const filterDiseases = (searchText, diseases) => {
  const searchRegExp = new RegExp(searchText, 'i');
  return searchText.length
    ? diseases.filter(({ diseaseInformation: { name } }) => searchRegExp.test(name))
    : diseases;
};

const DiseaseListPanelContainer = ({
  geonameId,
  diseaseId,
  onSelect,
  onClose, // TODO: 633056e0: group panel-related props (and similar) by combining them in an interface
  isMinimized,
  onMinimize
}) => {
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
    <DiseaseListPanelDisplay
      sortBy={sortBy}
      sortOptions={sortOptions}
      onSelectSortBy={setSortBy}
      searchText={searchText}
      onSearchTextChanged={setSearchText}
      isLoading={isLoading}
      diseaseId={diseaseId}
      diseasesList={processedDiseases}
      onSelectDisease={onSelect}
      onSettingsClick={handleOnSettingsClick}
      // TODO: 633056e0
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      onClose={onClose}
    />
  );
};

export default DiseaseListPanelContainer;
