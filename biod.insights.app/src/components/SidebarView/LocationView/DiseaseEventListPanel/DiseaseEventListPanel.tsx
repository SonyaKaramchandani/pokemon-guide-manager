/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import axios from 'axios';
import * as dto from 'client/dto';
import { useDependentState } from 'hooks/useDependentState';
import React, { useContext, useEffect, useMemo, useState } from 'react';
import { Tab } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { AppStateContext } from 'api/AppStateContext';
import EventsApi from 'api/EventsApi';
import locationApi from 'api/LocationApi';
import { RiskDirectionType } from 'models/RiskCategories';
import { Geoname } from 'utils/constants';
import { sxtheme } from 'utils/cssHelpers';
import { MapProximalLocations2VM, MapShapesToProximalMapShapes } from 'utils/modelHelpers';
import { isMobile, isNonMobile } from 'utils/responsive';

import { ProximalCaseCard } from 'components/_controls/ProximalCaseCard';
import { DiseaseAttributes } from 'components/DiseaseAttributes';
import { Error } from 'components/Error';
import { MobilePanelSummary } from 'components/MobilePanelSummary';
import { IPanelProps, Panel } from 'components/Panel';
import { RisksProjectionCard } from 'components/RisksProjectionCard';
import { EventListPanel } from 'components/SidebarView/EventView/EventListPanel';
import { ActivePanel } from 'components/SidebarView/sidebar-types';

export type DiseaseEventListPanelProps = IPanelProps & {
  activePanel: ActivePanel;
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
  const diseaseInformation = useDependentState(() => disease && disease.diseaseInformation, [
    disease
  ]);
  const importationRisk = useDependentState(() => disease && disease.importationRisk, [disease]);
  const exportationRisk = useDependentState(() => disease && disease.exportationRisk, [disease]);
  const outbreakPotentialCategory = useDependentState(
    () => disease && disease.outbreakPotentialCategory,
    [disease]
  );

  const { appState, amendState } = useContext(AppStateContext);

  const [events, setEvents] = useState<dto.GetEventListModel>({});
  const [isEventListLoading, setIsEventListLoading] = useState(false);
  const [hasError, setHasError] = useState(false);
  const [activeRiskType, setActiveRiskType] = useState<RiskDirectionType>('importation');

  const loadEventDetailsForDisease = () => {
    setHasError(false);
    setIsEventListLoading(true);
    EventsApi.getEvents({
      diseaseId,
      ...(geonameId !== Geoname.GLOBAL_VIEW && { geonameId })
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

  useEffect(() => {
    if (!events || !events.proximalLocations) return;
    locationApi
      .getGeonameShapes(
        events.proximalLocations.map(e => e.locationId),
        false
      )
      .then(({ data }) => {
        const proximalShapes = MapShapesToProximalMapShapes(data, events.proximalLocations);
        amendState({
          proximalGeonameShapes: proximalShapes
        });
      });
  }, [amendState, events]);

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
  const proximalVM = useMemo(() => MapProximalLocations2VM(events.proximalLocations), [
    events && events.proximalLocations
  ]);

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
      isLoading={isEventListLoading || !disease}
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
            {proximalVM && (
              <ProximalCaseCard
                vm={proximalVM}
                onCardOpenedChanged={handleProximalDetailsExpanded}
              />
            )}

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
