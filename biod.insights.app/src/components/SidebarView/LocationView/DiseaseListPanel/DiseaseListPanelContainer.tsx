/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import React, { useEffect, useState } from 'react';
import { jsx } from 'theme-ui';

import DiseaseApi from 'api/DiseaseApi';
import { navigateToCustomSettingsUrl } from 'components/Navigationbar';
import { IPanelProps } from 'components/Panel';
import {
  DefaultSortOptionValue,
  DiseaseListGlobalViewSortOptions,
  DiseaseListLocationViewSortOptions
} from 'models/SortByOptions';
import { Geoname } from 'utils/constants';
import { isNonMobile, isMobile } from 'utils/responsive';
import { sort } from 'utils/arrayHelpers';
import { containsNoCaseNoLocale } from 'utils/stringHelpers';
import * as dto from 'client/dto';
import { ActivePanel } from 'components/SidebarView/sidebar-types';

import DiseaseListPanelDisplay from './DiseaseListPanelDisplay';
import { DiseaseCardProps } from './DiseaseCard';

type DiseaseListPanelContainerProps = IPanelProps & {
  activePanel: ActivePanel;
  geonameId: number;
  diseaseId: number;
  onDiseasesLoad: (diseases: dto.DiseaseRiskModel[]) => void;
  onDiseaseSelect: (disease: dto.DiseaseRiskModel) => void;
  onEventPinsLoad: (diseases: dto.EventsPinModel[]) => void;
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
  onDiseasesLoad,
  onDiseaseSelect,
  onEventPinsLoad,
  onClose,
  isMinimized,
  onMinimize,
  summaryTitle,
  locationFullName
}) => {
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
  const [diseases, setDiseases] = useState<dto.DiseaseRiskModel[]>(null);
  const [eventPins, setEventPins] = useState<dto.EventsPinModel[]>([]);
  const [diseasesCaseCounts, setDiseasesCaseCounts] = useState<CaseCountModelVM[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  // TODO: 3b381eba: move sorting login into DiseaseListPanelDisplay
  const [sortBy, setSortBy] = useState(DefaultSortOptionValue); // TODO: 597e3adc: right now we get away with the setter cast, because it thinks its a string
  const [sortOptions, setSortOptions] = useState(DiseaseListLocationViewSortOptions);
  const [searchText, setSearchText] = useState('');
  const [hasError, setHasError] = useState(false);

  useEffect(() => {
    if (geonameId === Geoname.GLOBAL_VIEW) {
      setSortOptions(DiseaseListGlobalViewSortOptions);
    } else {
      setSortOptions(DiseaseListLocationViewSortOptions);
    }
  }, [geonameId]);

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
          setEventPins(countryPins);
        } else {
          setDiseases([]);
          setEventPins([]);
          setHasError(true);
        }
      })
      .catch(() => {
        setDiseases([]);
        setEventPins([]);
        setHasError(true);
      })
      .finally(() => {
        setIsLoading(false);
      });
  };
  useEffect(() => {
    if (geonameId == null) return;
    loadDiseases();
  }, [geonameId, setIsLoading, setDiseases, setHasError]); // TODO: 4a0a4e90: why are these dependencies: setIsLoading, setDiseases, setHasError

  useEffect(() => {
    onDiseasesLoad && onDiseasesLoad(diseases);
  }, [diseases, onDiseasesLoad]);

  useEffect(() => {
    onEventPinsLoad && onEventPinsLoad(eventPins);
  }, [eventPins, onEventPinsLoad]);

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

  const isMobileDevice = isMobile(useBreakpointIndex());
  if (isMobileDevice && activePanel !== 'DiseaseListPanel') {
    return null;
  }

  const handleOnSettingsClick = () => {
    navigateToCustomSettingsUrl();
  };

  const processedDiseases: DiseaseCardProps[] = sort({
    items: (diseases || [])
      .map(d => ({
        ...d,
        isHidden: !containsNoCaseNoLocale(d.diseaseInformation.name, searchText) // TODO: 75727d4c: remove isHidden!
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
      // TODO: 3b381eba: move sorting login into DiseaseListPanelDisplay
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
      onDiseaseSelect={onDiseaseSelect}
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
