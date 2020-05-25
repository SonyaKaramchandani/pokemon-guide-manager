/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import axios from 'axios';
import * as dto from 'client/dto';
import React, { useContext, useEffect, useMemo, useState } from 'react';
import { Tab } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { AppStateContext } from 'api/AppStateContext';
import EventsApi from 'api/EventsApi';
import locationApi from 'api/LocationApi';
import { ProximalCaseVM } from 'models/EventModels';
import { RiskDirectionType } from 'models/RiskCategories';
import { Geoname } from 'utils/constants';
import { sxtheme } from 'utils/cssHelpers';
import { MapShapesToProximalMapShapes } from 'utils/modelHelpers';
import { isMobile, isNonMobile } from 'utils/responsive';

import { ProximalCaseCard } from 'components/_controls/ProximalCaseCard';
import {
  ProximalCaseLoading,
  ProximalCaseNoResult
} from 'components/_controls/ProximalCaseCard/ProximalCaseCard';
import { DiseaseAttributes } from 'components/DiseaseAttributes';
import { Error } from 'components/Error';
import { MobilePanelSummary } from 'components/MobilePanelSummary';
import { IPanelProps, Panel } from 'components/Panel';
import { RisksProjectionCard } from 'components/RisksProjectionCard';
import { EventListPanel } from 'components/SidebarView/EventView/EventListPanel';
import { ActivePanel } from 'components/SidebarView/sidebar-types';

export type DiseaseEventListPanelProps = IPanelProps & {
  activePanel: ActivePanel;
  isGlobal: boolean;
  geonameId: number;
  diseaseId: number;
  eventId: number;
  disease: dto.DiseaseRiskModel;
  summaryTitle: string;
  locationFullName: string;
  onEventListLoad: (val: dto.GetEventListModel) => void;
  onEventSelected: (eventId: number, title: string) => void;
  closePanelOnEventsLoadError?: boolean;
};

const DiseaseEventListPanel: React.FC<DiseaseEventListPanelProps> = ({
  activePanel,
  isGlobal,
  geonameId,
  diseaseId,
  eventId,
  disease,
  summaryTitle,
  locationFullName,
  onEventSelected,
  onEventListLoad,
  onClose,
  closePanelOnEventsLoadError = false,
  isMinimized,
  onMinimize
}) => {
  const isMobileDevice = isMobile(useBreakpointIndex());
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());

  const [activeTabIndex, setActiveTabIndex] = useState(1);
  const [isLocal, setIsLocal] = useState(false);
  const diseaseInformation = useMemo(() => disease && disease.diseaseInformation, [disease]);
  const importationRisk = useMemo(() => disease && disease.importationRisk, [disease]);
  const exportationRisk = useMemo(() => disease && disease.exportationRisk, [disease]);
  const outbreakPotentialCategory = useMemo(() => disease && disease.outbreakPotentialCategory, [
    disease
  ]);

  const { appState, amendState } = useContext(AppStateContext);
  const [events, setEvents] = useState<dto.GetEventListModel>(null);
  const [isEventListLoading, setIsEventListLoading] = useState(false);
  const [hasError, setHasError] = useState(false);
  const [activeRiskType, setActiveRiskType] = useState<RiskDirectionType>('importation');

  const { proximalData } = appState;

  const loadEventDetailsForDisease = () => {
    setHasError(false);
    setIsEventListLoading(true);
    EventsApi.getEvents({
      diseaseId,
      ...(!isGlobal && { geonameId })
    })
      .then(({ data }) => {
        setIsEventListLoading(false);
        setEvents(data);
        onEventListLoad(data);
        setIsLocal(data.eventsList.some(e => e.isLocal));
      })
      .catch(error => {
        if (!axios.isCancel(error)) {
          setHasError(true);
        }
      })
      .finally(() => {
        setIsEventListLoading(false);
      });
  };

  const handleOnTabChange = (e, { activeIndex }) => setActiveTabIndex(activeIndex);

  // TODO: 9eae0d15: no webcalls in storybook!
  useEffect(() => {
    if (geonameId == null || diseaseId == null) return;
    loadEventDetailsForDisease();
  }, [geonameId, diseaseId, setIsLocal, setHasError]);

  useEffect(() => {
    if (closePanelOnEventsLoadError && hasError) onClose && onClose();
  }, [hasError]);

  const handleProximalDetailsExpanded = (isExpanded: boolean) => {
    amendState({
      isProximalDetailsExpandedDELP: isExpanded
    });
  };
  useEffect(() => {
    amendState({ isProximalDetailsExpandedDELP: false });
  }, []);

  // prettier-ignore
  const proximalVM: ProximalCaseVM = useMemo(
    () => proximalData && proximalData[diseaseId],
    [proximalData, diseaseId]
  );

  useEffect(() => {
    if (!proximalVM) return;
    locationApi.getGeonameShapes(proximalVM.geonameIds, false).then(({ data }) => {
      const proximalShapes = MapShapesToProximalMapShapes(data, proximalVM);
      amendState({
        proximalGeonameShapes: proximalShapes
      });
    });
  }, [amendState, proximalVM]);

  if (isMobileDevice && activePanel !== 'DiseaseEventListPanel') {
    return null;
  }

  if (!diseaseId && !disease) {
    return null;
  }

  const panes = [
    {
      menuItem: 'Disease Details',
      render: () => (
        <Tab.Pane>
          <DiseaseAttributes {...diseaseInformation} />
        </Tab.Pane>
      )
    },
    {
      menuItem: 'Events',
      render: () => (
        <Tab.Pane>
          <EventListPanel
            activePanel={activePanel}
            isStandAlone={false}
            geonameId={geonameId}
            eventId={eventId}
            events={events}
            onEventSelected={onEventSelected}
          />
        </Tab.Pane>
      )
    }
  ];

  return (
    <Panel
      isAnimated
      title={
        (diseaseInformation && diseaseInformation.name) ||
        (hasError ? 'Something went wrong' : 'Loading...')
      }
      onClose={onClose}
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      isLoading={!events || isEventListLoading || !disease}
      subtitleMobile={locationFullName}
      summary={<MobilePanelSummary onClick={onClose} summaryTitle={summaryTitle} />}
    >
      {hasError ? (
        <Error
          title="Something went wrong."
          subtitle="Please check your network connectivity and try again."
          linkText="Click here to retry"
          linkCallback={loadEventDetailsForDisease}
        />
      ) : (
        <React.Fragment>
          <div
            sx={{
              p: '16px',
              bg: sxtheme(t => t.colors.deepSea10)
            }}
          >
            {!isGlobal &&
              (!proximalVM ? (
                <ProximalCaseLoading />
              ) : proximalVM && proximalVM.totalCases > 0 ? (
                <ProximalCaseCard
                  vm={proximalVM}
                  onCardOpenedChanged={handleProximalDetailsExpanded}
                />
              ) : (
                <ProximalCaseNoResult />
              ))}

            <RisksProjectionCard
              importationRisk={importationRisk}
              exportationRisk={exportationRisk}
              outbreakPotentialCategory={outbreakPotentialCategory}
              diseaseInformation={diseaseInformation}
              riskType={activeRiskType}
              onRiskTypeChanged={setActiveRiskType}
            />
          </div>
          <Tab
            menu={{ tabular: true }}
            panes={panes}
            activeIndex={activeTabIndex}
            onTabChange={handleOnTabChange}
          />{' '}
        </React.Fragment>
      )}
    </Panel>
  );
};

export default DiseaseEventListPanel;
