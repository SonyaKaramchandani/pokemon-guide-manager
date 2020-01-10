import React from 'react';
import { action } from '@storybook/addon-actions';
import DiseaseListItem from './DiseaseListItem';

export default {
  title: 'DiseaseItem/DiseaseListItem'
};

const props = {
  casesInfo: {
    reportedCases: 50
  },
  diseaseInformation: {
    id: 'diseaseId',
    name: 'diseaseName',
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
  },
};

export const text = () => (
  <div style={{ width: 350, padding: '1rem' }}>
    <DiseaseListItem {...props} />
  </div>
);