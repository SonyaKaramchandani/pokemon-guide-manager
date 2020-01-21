import React from 'react';
import { action } from '@storybook/addon-actions';
import RisksProjectionCard, { RiskOfImportation, RiskOfExportation } from './RisksProjectionCard';
import { Accordian } from 'components/Accordian';
import { Card } from 'semantic-ui-react';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';


export default {
  title: 'EventDetails/RisksProjectionCard'
};

const props = {
  outbreakPotentialCategory: {
    id: 5,
    name: 'Malaria'
  },
  diseaseInformation: {
    id: 'diseaseId',
    name: 'Yellow Fever',
    agents: 'Bacillus anthracis',
    agentType: 'Bacteria',
    transmissionModes: 'Airborne, Zoonotic Fluid Transmission',
    incubationPeriod: '?',
    preventionMeasure: 'Vaccine',
    biosecurityRisk:
      'Category A: High mortality rate, easily disseminated or transmitted from person to person.'
  },
  importationRisk: {
    minProbability: 0.01,
    maxProbability: 0.1,
    minMagnitude: 1.389,
    maxMagnitude: 1.552
  },
  exportationRisk: {
    minProbability: 70,
    maxProbability: 100,
    minMagnitude: 19558.793,
    maxMagnitude: 19739.648
  }
};

export const BothRisks = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <RisksProjectionCard {...props} />
  </div>
);

export const noImportation = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <RisksProjectionCard {...props} importationRisk={null} />
  </div>
);

export const noExportation = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <RisksProjectionCard {...props} exportationRisk={null} />
  </div>
);

export const Cardless = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <h1>This is an immitation of a section of EventDetailsPanel</h1>
    <Accordian expanded={true} title="II. Risk of Importation">
      <Card fluid className="borderless">
        <Card.Content>
          <FlexGroup suffix={<BdIcon name="icon-plane-departure" />}>
            <Typography variant="subtitle2" color="stone90">
              Overall
            </Typography>
          </FlexGroup>
        </Card.Content>
        <RiskOfImportation risk={props.importationRisk} />
      </Card>
    </Accordian>
    <Accordian expanded={true} title="III. Risk of Exportation">
      <Card fluid className="borderless">
        <Card.Content>
          <FlexGroup suffix={<BdIcon name="icon-plane-departure" />}>
            <Typography variant="subtitle2" color="stone90">
              Overall
            </Typography>
          </FlexGroup>
        </Card.Content>
        <RiskOfExportation risk={props.exportationRisk} />
      </Card>
    </Accordian>
  </div>
);
