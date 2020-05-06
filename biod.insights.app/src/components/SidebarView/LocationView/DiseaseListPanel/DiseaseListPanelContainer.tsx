/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import React, { useEffect, useState } from 'react';
import { jsx } from 'theme-ui';
import { NumericDictionary } from 'lodash';

import DiseaseApi from 'api/DiseaseApi';
import { navigateToCustomSettingsUrl } from 'components/Navigationbar';
import { IPanelProps } from 'components/Panel';

import { Geoname } from 'utils/constants';
import { isNonMobile, isMobile } from 'utils/responsive';
import { sort, mapToNumericDictionary } from 'utils/arrayHelpers';
import { containsNoCaseNoLocale } from 'utils/stringHelpers';
import * as dto from 'client/dto';
import { ActivePanel } from 'components/SidebarView/sidebar-types';

import DiseaseListPanelDisplay from './DiseaseListPanelDisplay';
import { DiseaseCardProps } from './DiseaseCard';
import { CaseCountModelMap } from 'models/DiseaseModels';
import { PromiseAllDictionaryNumeric } from 'utils/promiseUtils';

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
  const [diseasesCaseCounts, setDiseasesCaseCounts] = useState<CaseCountModelMap>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [hasError, setHasError] = useState(false);

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
      PromiseAllDictionaryNumeric<dto.CaseCountModel>(
        mapToNumericDictionary(
          diseases,
          d => d.diseaseInformation.id,
          d =>
            DiseaseApi.getDiseaseCaseCount({
              diseaseId: d.diseaseInformation.id,
              geonameId: geonameId === Geoname.GLOBAL_VIEW ? null : geonameId
            }).then(({ data }) => data)
        )
      ).then(responses => {
        setDiseasesCaseCounts(responses);
      });
  }, [geonameId, diseases, setDiseasesCaseCounts]);

  const isMobileDevice = isMobile(useBreakpointIndex());
  if (isMobileDevice && activePanel !== 'DiseaseListPanel') {
    return null;
  }

  const handleOnSettingsClick = () => {
    navigateToCustomSettingsUrl();
  };

  const subtitle = getSubtitle(diseases, diseaseId);

  return (
    <DiseaseListPanelDisplay
      isLoading={isLoading}
      geonameId={geonameId}
      diseaseId={diseaseId}
      diseases={diseases}
      diseasesCaseCounts={diseasesCaseCounts}
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
