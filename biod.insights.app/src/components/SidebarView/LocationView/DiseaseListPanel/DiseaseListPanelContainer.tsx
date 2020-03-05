/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import React, { useEffect, useState } from 'react';
import { jsx } from 'theme-ui';

import DiseaseApi from 'api/DiseaseApi';
import { navigateToCustomSettingsUrl } from 'components/Navigationbar';
import { IPanelProps } from 'components/Panel';
import {
  DefaultSortOptionValue,
  DiseaseListGlobalViewSortOptions as globalSortOptions,
  DiseaseListLocationViewSortOptions as locationSortOptions
} from 'components/SidebarView/SortByOptions';
import esriMap from 'map';
import eventsView from 'map/events';
import { Geoname } from 'utils/constants';
import { isNonMobile } from 'utils/responsive';
import { sort } from 'utils/sort';
import { containsNoCaseNoLocale } from 'utils/stringHelpers';
import * as dto from 'client/dto';

import DiseaseListPanelDisplay from './DiseaseListPanelDisplay';
import { DiseaseCardProps } from './DiseaseCard';

type DiseaseListPanelContainerProps = IPanelProps & {
  activePanel: string;
  geonameId: number;
  diseaseId: number;
  onSelect: (diseaseId: number, disease: dto.DiseaseRiskModel) => void;
  summaryTitle: string;
  locationFullName: string;
};

type CaseCountModelVM = dto.CaseCountModel & {
  diseaseId: number;
};

const getSubtitle = (diseases, diseaseId) => {
  if (diseaseId === null || diseases === null) return null;

  let subtitle = null;
  const selectedDisease = diseases.find(d => d.diseaseInformation.id === diseaseId);
  if (selectedDisease) {
    subtitle = selectedDisease.diseaseInformation.name;
  }
  return subtitle;
};

const DiseaseListPanelContainer: React.FC<DiseaseListPanelContainerProps> = ({
  activePanel,
  geonameId,
  diseaseId,
  onSelect,
  onClose,
  isMinimized,
  onMinimize,
  summaryTitle,
  locationFullName
}) => {
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
  const [diseases, setDiseases] = useState([] as dto.DiseaseRiskModel[]);
  const [diseasesCaseCounts, setDiseasesCaseCounts] = useState<CaseCountModelVM[]>([]);
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
          }).then(({ data }) => {
            return { ...data, diseaseId: d.diseaseInformation.id };
          })
        )
      ).then(responses => {
        if (responses && responses.length) {
          setDiseasesCaseCounts(responses || []);
        }
      });
  }, [geonameId, diseases, setDiseasesCaseCounts, setIsLoading]);

  const handleOnSettingsClick = () => {
    navigateToCustomSettingsUrl();
  };

  const loadDiseases = () => {
    setHasError(false);
    setIsLoading(true);
    DiseaseApi.getDiseaseRiskByLocation({ ...(geonameId !== Geoname.GLOBAL_VIEW && { geonameId }) })
      .then(data => {
        const {
          data: { diseaseRisks, countryPins },
          status
        } = data;
        if (status === 200) {
          setDiseases(diseaseRisks);
          isNonMobileDevice && eventsView.updateEventView(countryPins, geonameId);
          isNonMobileDevice && esriMap.showEventsView();
        } else {
          setHasError(true);
          isNonMobileDevice && eventsView.updateEventView([], geonameId);
          isNonMobileDevice && esriMap.showEventsView();
        }
      })
      .catch(() => {
        setHasError(true);
        isNonMobileDevice && eventsView.updateEventView([], geonameId);
        isNonMobileDevice && esriMap.showEventsView();
      })
      .finally(() => {
        setIsLoading(false);
      });
  };
  const processedDiseases: DiseaseCardProps[] = sort({
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
      subtitleMobile={locationFullName}
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
