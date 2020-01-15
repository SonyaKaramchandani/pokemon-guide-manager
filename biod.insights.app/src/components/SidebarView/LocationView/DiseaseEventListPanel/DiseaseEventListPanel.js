import React, { useState } from 'react';
import { Tab } from 'semantic-ui-react';
import { Panel } from 'components/Panel';
import { RisksProjectionCard } from 'components/RisksProjectionCard';
import { DiseaseAttributes } from 'components/DiseaseAttributes';
import { EventListPanel } from 'components/SidebarView/EventView/EventListPanel';

function DiseaseEventListPanel({ geonameId, diseaseId, eventId, disease, onSelect, onClose, onEventListLoad }) {
  const [activeTabIndex, setActiveTabIndex] = useState(1);

  if (!diseaseId && !disease) {
    return null;
  }

  const handleOnTabChange = (e, { activeIndex }) => setActiveTabIndex(activeIndex);

  const {
    diseaseInformation,
    importationRisk,
    exportationRisk,
    outbreakPotentialCategory
  } = disease;

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
            onEventListLoad={onEventListLoad}
          />
        </Tab.Pane>
      )
    }
  ];

  return (
    <Panel title={diseaseInformation.name} onClose={onClose}>
      <RisksProjectionCard
        importationRisk={importationRisk}
        exportationRisk={exportationRisk}
        outbreakPotentialCategory={outbreakPotentialCategory}
        diseaseInformation={diseaseInformation}
      />
      <Tab menu={{ tabular: true }} panes={panes} activeIndex={activeTabIndex} onTabChange={handleOnTabChange} />
    </Panel>
  );
}

export default DiseaseEventListPanel;
