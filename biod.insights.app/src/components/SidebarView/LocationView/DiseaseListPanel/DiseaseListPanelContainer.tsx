/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import * as dto from 'client/dto';
import React, { useCallback, useContext, useEffect, useState } from 'react';
import { jsx } from 'theme-ui';

import { AppStateContext } from 'api/AppStateContext';
import DiseaseApi from 'api/DiseaseApi';
import { ProximalCaseVM } from 'models/EventModels';
import { mapToNumericDictionary } from 'utils/arrayHelpers';
import { Geoname } from 'utils/constants';
import { MapProximalLocations2VM } from 'utils/modelHelpers';
import { PromiseAllDictionaryNumeric } from 'utils/promiseUtils';
import { isMobile, isNonMobile } from 'utils/responsive';

import { navigateToCustomSettingsUrl } from 'components/Navigationbar';
import { IPanelProps } from 'components/Panel';
import { ActivePanel } from 'components/SidebarView/sidebar-types';

import DiseaseListPanelDisplay from './DiseaseListPanelDisplay';

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
  const [isLoading, setIsLoading] = useState(true);
  const [hasError, setHasError] = useState(false);
  const { appState, amendState } = useContext(AppStateContext);
  const isGlobal = geonameId === Geoname.GLOBAL_VIEW;

  const loadDiseases = useCallback(() => {
    setHasError(false);
    setIsLoading(true);
    DiseaseApi.getDiseaseRiskByLocation({ ...(!isGlobal && { geonameId }) })
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
  }, [geonameId, isGlobal]);

  useEffect(() => {
    if (geonameId == null) return;
    loadDiseases();
  }, [isGlobal, geonameId, setIsLoading, setDiseases, setHasError, loadDiseases]); // TODO: 4a0a4e90: why are these dependencies: setIsLoading, setDiseases, setHasError

  useEffect(() => {
    onDiseasesLoad && onDiseasesLoad(diseases);
  }, [diseases, onDiseasesLoad]);

  useEffect(() => {
    onEventPinsLoad && onEventPinsLoad(eventPins);
  }, [eventPins, onEventPinsLoad]);

  useEffect(() => {
    diseases &&
      !isGlobal &&
      PromiseAllDictionaryNumeric<ProximalCaseVM>(
        mapToNumericDictionary(
          diseases,
          d => d.diseaseInformation.id,
          d =>
            DiseaseApi.getDiseaseCaseCount({
              diseaseId: d.diseaseInformation.id,
              geonameId: geonameId
            }).then(({ data }) => MapProximalLocations2VM(data))
        )
      ).then(finalProximalCasesNumnericMap => {
        amendState({ proximalData: finalProximalCasesNumnericMap });
      });

    return () => {
      // TODO: 4e9e1e68: example of useEffect unsubscribe
      amendState({ proximalData: null });
    };
  }, [geonameId, diseases, amendState, isGlobal]);

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
      isGlobal={isGlobal}
      diseaseId={diseaseId}
      diseases={diseases}
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
