import React from 'react';
import { action } from '@storybook/addon-actions';
import DiseaseEventListPanel from './DiseaseEventListPanel';

export default {
  title: 'DiseaseEvent/DiseaseEventListPanel'
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

export const text = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <DiseaseEventListPanel
      diseaseId={diseaseId}
      disease={disease}
      onEventListLoad={action('onEventListLoad')}
      onSelect={action('onSelect')}
      onClose={action('onClose')}
      onMinimize={action('onMinimize')}

    />
  </div>
);
