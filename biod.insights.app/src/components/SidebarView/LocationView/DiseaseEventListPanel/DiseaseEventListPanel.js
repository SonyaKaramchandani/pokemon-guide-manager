/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Tab } from 'semantic-ui-react';
import { Panel } from 'components/Panel';
import { RisksProjectionCard } from 'components/RisksProjectionCard';
import { DiseaseAttributes } from 'components/DiseaseAttributes';
import { EventListPanel } from 'components/SidebarView/EventView/EventListPanel';
import EventApi from 'api/EventApi';
import { Geoname } from 'utils/constants';
import { Error } from 'components/Error';
import eventDetailsView from 'map/eventDetails';
import { ProximalCasesSection } from 'components/ProximalCasesSection';
import { MobilePanelSummary } from 'components/MobilePanelSummary';
import { Panels } from 'utils/constants';
import { useBreakpointIndex } from '@theme-ui/match-media';
import { isMobile, isNonMobile } from 'utils/responsive';

function DiseaseEventListPanel({
  activePanel,
  geonameId,
  diseaseId,
  eventId,
  disease,
  summaryTitle,
  onSelect,
  onClose,
  onEventListLoad,
  isMinimized,
  onMinimize
}) {
  const [activeTabIndex, setActiveTabIndex] = useState(1);
  const [isLocal, setIsLocal] = useState(false);
  const [diseaseInformation, setDiseaseInformation] = useState(disease.diseaseInformation);
  const [importationRisk, setImportationRisk] = useState(disease.importationRisk);
  const [exportationRisk, setExportationRisk] = useState(disease.exportationRisk);
  const [outbreakPotentialCategory, setOutbreakPotentialCategory] = useState(
    disease.outbreakPotentialCategory
  );
  const [events, setEvents] = useState([]);
  const [isEventListLoading, setIsEventListLoading] = useState(false);
  const [hasError, setHasError] = useState(false);

  useEffect(() => {
    setDiseaseInformation(disease.diseaseInformation);
    setImportationRisk(disease.importationRisk);
    setExportationRisk(disease.exportationRisk);
    setOutbreakPotentialCategory(disease.outbreakPotentialCategory);
  }, [disease, diseaseId]);

  // TODO: 9eae0d15: no webcalls in storybook!
  useEffect(() => {
    loadEventDetailsForDisease();
  }, [geonameId, diseaseId, setIsLocal, setHasError]);

  const isMobileDevice = isMobile(useBreakpointIndex());
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
  if (isMobileDevice && activePanel !== Panels.DiseaseEventListPanel) {
    return null;
  }

  if (!diseaseId && !disease) {
    return null;
  }

  const loadEventDetailsForDisease = () => {
    setHasError(false);
    setIsEventListLoading(true);
    EventApi.getEvent(geonameId === Geoname.GLOBAL_VIEW ? { diseaseId } : { diseaseId, geonameId })
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
            onSelect={onSelect}
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
      title={diseaseInformation.name}
      onClose={onClose}
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      isLoading={isEventListLoading}
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
        <>
          <div
            sx={{
              p: '16px',
              bg: t => t.colors.deepSea10
            }}
          >
            {!!localCaseCounts && <ProximalCasesSection localCaseCounts={localCaseCounts} />}

            <RisksProjectionCard
              isLocal={isLocal}
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
        </>
      )}
    </Panel>
  );
}

export default DiseaseEventListPanel;
