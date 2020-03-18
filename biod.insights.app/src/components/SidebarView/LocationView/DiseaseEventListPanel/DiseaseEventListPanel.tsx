/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Tab } from 'semantic-ui-react';
import { Panel, IPanelProps } from 'components/Panel';
import { RisksProjectionCard } from 'components/RisksProjectionCard';
import { DiseaseAttributes } from 'components/DiseaseAttributes';
import { EventListPanel } from 'components/SidebarView/EventView/EventListPanel';
import EventsApi from 'api/EventsApi';
import { Geoname } from 'utils/constants';
import { Error } from 'components/Error';
import eventDetailsView from 'map/eventDetails';
import { ProximalCasesSection } from 'components/ProximalCasesSection';
import { MobilePanelSummary } from 'components/MobilePanelSummary';
import { useBreakpointIndex } from '@theme-ui/match-media';
import { isMobile, isNonMobile } from 'utils/responsive';
import * as dto from 'client/dto';
import { ActivePanel } from 'components/SidebarView/sidebar-types';
import { useDependentState } from 'hooks/useDependentState';

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
  onClose,
  onEventListLoad,
  closePanelOnEventsLoadError = false,
  isMinimized,
  onMinimize
}) => {
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

  const [events, setEvents] = useState<dto.GetEventListModel>({});
  const [isEventListLoading, setIsEventListLoading] = useState(false);
  const [hasError, setHasError] = useState(false);

  const loadEventDetailsForDisease = () => {
    setHasError(false);
    setIsEventListLoading(true);
    EventsApi.getEvents({
      diseaseId,
      ...(geonameId !== Geoname.GLOBAL_VIEW && { geonameId })
    })
      .then(({ data }) => {
        setIsEventListLoading(false);
        isNonMobileDevice && eventDetailsView.clear();
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

  const isMobileDevice = isMobile(useBreakpointIndex());
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
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

  const hasLocalEvents = disease && disease.hasLocalEvents;
  const localCaseCounts = events && events.localCaseCounts;

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
              bg: t => t.colors.deepSea10
            }}
          >
            {!!localCaseCounts && <ProximalCasesSection localCaseCounts={localCaseCounts} />}

            <RisksProjectionCard
              importationRisk={importationRisk}
              exportationRisk={exportationRisk}
              outbreakPotentialCategory={outbreakPotentialCategory}
              diseaseInformation={diseaseInformation}
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
