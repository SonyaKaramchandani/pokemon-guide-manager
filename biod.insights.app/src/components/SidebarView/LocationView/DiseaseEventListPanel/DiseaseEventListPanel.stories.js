import React from 'react';
import { action } from '@storybook/addon-actions';
import DiseaseEventListPanel from './DiseaseEventListPanel';
import { DebugContainer4BdPanel } from 'components/_debug/StorybookContainer';

export default {
  title: 'PANELS/DiseaseEventListPanel'
};

const diseaseId = 110,
  disease = {
    diseaseInformation: {
      agents: 'Bacillus anthracis',
      agentType: 'Bacteria',
      transmissionModes: 'Airborne, Zoonotic Fluid Transmission',
      incubationPeriod: '?',
      preventionMeasure: 'Vaccine',
      biosecurityRisk:
        'Category A: High mortality rate, easily disseminated or transmitted from person to person.'
    },
    importationRisk: {
      minProbability: 1,
      maxProbability: 1,
      minMagnitude: 18.389,
      maxMagnitude: 18.552
    },
    exportationRisk: {
      minProbability: 1,
      maxProbability: 1,
      minMagnitude: 19558.793,
      maxMagnitude: 19739.648
    }
  };

// TODO: 9eae0d15: no webcalls in storybook!
export const test = () => (
  <DebugContainer4BdPanel>
    <DiseaseEventListPanel
      diseaseId={diseaseId}
      disease={disease}
      onEventListLoad={action('onEventListLoad')}
      onSelect={action('onSelect')}
      onClose={action('onClose')}
      onMinimize={action('onMinimize')}
    />
  </DebugContainer4BdPanel>
);
