import React from 'react';
import { Tab } from 'semantic-ui-react';
import { Panel } from 'components/Panel';
import { RisksCarousel } from 'components/RisksCarousel';
import { DiseaseAttributes } from 'components/DiseaseAttributes';
import { EventListPanel } from 'components/SidebarView/EventView/EventListPanel';

function DiseaseEventListPanel({ geonameId, diseaseId, disease }) {
  if (!diseaseId && !disease) {
    return null;
  }

  const { diseaseInformation, importationRisk, exportationRisk, casesInfo = {} } = disease;

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
          <EventListPanel isStandAlone={false} diseaseId={diseaseId} geonameId={geonameId} />
        </Tab.Pane>
      )
    }
  ];

  return (
    <Panel title={diseaseInformation.name}>
      <RisksCarousel importationRisk={importationRisk} exportationRisk={exportationRisk} />
      <Tab panes={panes} />
    </Panel>
  );
}

export default DiseaseEventListPanel;
