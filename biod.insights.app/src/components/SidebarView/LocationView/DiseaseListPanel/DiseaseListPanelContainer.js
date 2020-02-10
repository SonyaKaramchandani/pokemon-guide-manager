/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import DiseaseApi from 'api/DiseaseApi';
import { containsNoCaseNoLocale } from 'utils/stringHelpers';

import {
  DiseaseListLocationViewSortOptions as locationSortOptions,
  DiseaseListGlobalViewSortOptions as globalSortOptions,
  DefaultSortOptionValue,
  sort
} from 'components/SidebarView/SortByOptions';
import { navigateToCustomSettingsUrl } from 'components/Navigationbar';
import { Geoname } from 'utils/constants';
import esriMap from 'map';
import eventsView from 'map/events';
import DiseaseListPanelDisplay from './DiseaseListPanelDisplay';

const getSubtitle = (diseases, diseaseId) => {
  if (diseaseId === null || diseases === null) return null;

  let subtitle = null;
  const selectedDisease = diseases.find(d => d.diseaseInformation.id === diseaseId);
  if (selectedDisease) {
    subtitle = selectedDisease.diseaseInformation.name;
  }
  return subtitle;
};

const DiseaseListPanelContainer = ({
  activePanel,
  geonameId,
  diseaseId,
  onSelect,
  onClose, // TODO: 633056e0: group panel-related props (and similar) by combining them in an interface
  isMinimized,
  onMinimize,
  summaryTitle
}) => {
  const [diseases, setDiseases] = useState([]);
  const [diseasesCaseCounts, setDiseasesCaseCounts] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [sortBy, setSortBy] = useState(DefaultSortOptionValue);
  const [sortOptions, setSortOptions] = useState(locationSortOptions);
  const [searchText, setSearchText] = useState('');
  const [hasError, setHasError] = useState(false);

  useEffect(() => {
    if (geonameId === Geoname.GLOBAL_VIEW) {
      setSortOptions(globalSortOptions);
    } else {
      setSortOptions(locationSortOptions);
    }
  }, [geonameId]);

  useEffect(() => {
    loadDiseases();
  }, [geonameId, setIsLoading, setDiseases, setHasError]);

  useEffect(() => {
    diseases &&
      Promise.all(
        diseases.map(d =>
          DiseaseApi.getDiseaseCaseCount({
            diseaseId: d.diseaseInformation.id,
            geonameId: geonameId === Geoname.GLOBAL_VIEW ? null : geonameId
          })
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

  const loadDiseases = () => {
    setHasError(false);
    setIsLoading(true);
    DiseaseApi.getDiseaseRiskByLocation(geonameId === Geoname.GLOBAL_VIEW ? {} : { geonameId })
      .then(data => {
        const {
          data: { diseaseRisks, countryPins },
          status
        } = data;
        if (status === 200) {
          setDiseases(diseaseRisks);
          eventsView.updateEventView(countryPins, geonameId);
          esriMap.showEventsView();
        } else {
          setHasError(true);
          eventsView.updateEventView([], geonameId);
          esriMap.showEventsView();
        }
      })
      .catch(() => {
        setHasError(true);
        eventsView.updateEventView([], geonameId);
        esriMap.showEventsView();
      })
      .finally(() => {
        setIsLoading(false);
      });
  };
  const processedDiseases = sort({
    items: diseases
      .map(d => ({
        ...d,
        isHidden: !containsNoCaseNoLocale(d.diseaseInformation.name, searchText)
      })) // set isHidden for those records that do not match the `searchText`
      .map(s =>
        geonameId === Geoname.GLOBAL_VIEW
          ? s
          : {
              ...s,
              caseCounts: diseasesCaseCounts.find(d => d.diseaseId === s.diseaseInformation.id)
            }
      ),
    sortOptions,
    sortBy
  });

  const subtitle = getSubtitle(diseases, diseaseId);

  return (
    <DiseaseListPanelDisplay
      activePanel={activePanel}
      sortBy={sortBy}
      sortOptions={sortOptions}
      onSelectSortBy={setSortBy}
      searchText={searchText}
      onSearchTextChanged={setSearchText}
      isLoading={isLoading}
      geonameId={geonameId}
      diseaseId={diseaseId}
      diseasesList={processedDiseases}
      subtitle={subtitle}
      summaryTitle={summaryTitle}
      hasError={hasError}
      onSelectDisease={onSelect}
      onSettingsClick={handleOnSettingsClick}
      onRetryClick={loadDiseases}
      // TODO: 633056e0
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      onClose={onClose}
    />
  );
};

export default DiseaseListPanelContainer;
