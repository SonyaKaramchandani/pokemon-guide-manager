/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import { Tab } from 'semantic-ui-react';
import { Panel } from 'components/Panel';
import { RisksProjectionCard } from 'components/RisksProjectionCard';
import { DiseaseAttributes } from 'components/DiseaseAttributes';
import { EventListPanel } from 'components/SidebarView/EventView/EventListPanel';
import EventApi from 'api/EventApi';


function DiseaseEventListPanel({
  geonameId,
  diseaseId,
  eventId,
  disease,
  onSelect,
  onClose,
  onEventListLoad,
  isMinimized,
  onMinimize
}) {
  const [activeTabIndex, setActiveTabIndex] = useState(1);
  const [diseaseInformation, setDiseaseInformation] = useState(disease.diseaseInformation);
  const [importationRisk, setImportationRisk] = useState(disease.importationRisk);
  const [exportationRisk, setExportationRisk] = useState(disease.exportationRisk);
  const [outbreakPotentialCategory, setOutbreakPotentialCategory] = useState(disease.outbreakPotentialCategory);

  useEffect(() => {
    setDiseaseInformation(disease.diseaseInformation);
    setImportationRisk(disease.importationRisk);
    setExportationRisk(disease.exportationRisk);
    setOutbreakPotentialCategory(disease.outbreakPotentialCategory);
  }, [disease, diseaseId]);

  if (!diseaseId && !disease) {
    return null;
  }

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
            isStandAlone={false}
            diseaseId={diseaseId}
            geonameId={geonameId}
            eventId={eventId}
            onSelect={onSelect}
            // TODO: 9eae0d15: decouple EventApi from story book here too
            onNeedEventListApiCall={EventApi.getEvent}
            onEventListLoad={onEventListLoad}
          />
        </Tab.Pane>
      )
    }
  ];

  return (
    <Panel
      title={diseaseInformation.name}
      onClose={onClose}
      isMinimized={isMinimized}
      onMinimize={onMinimize}
    >
      <div sx={{
        p: '16px',
        bg: t => t.colors.deepSea10,
        borderRight: theme => `1px solid ${theme.colors.stone20}`,
        borderBottom: theme => `1px solid ${theme.colors.stone20}`,
      }}>
        <RisksProjectionCard
          importationRisk={importationRisk}
          exportationRisk={exportationRisk}
          outbreakPotentialCategory={outbreakPotentialCategory}
          diseaseInformation={diseaseInformation}
        />
      </div>
      <Tab menu={{ tabular: true }} panes={panes} activeIndex={activeTabIndex} onTabChange={handleOnTabChange} />
    </Panel>
  );
}

export default DiseaseEventListPanel;
