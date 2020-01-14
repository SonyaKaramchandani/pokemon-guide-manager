import React from 'react';
import { action } from '@storybook/addon-actions';
import { List } from 'semantic-ui-react';
import DiseaseCard from './DiseaseCard';

export default {
  title: 'DiseaseItem/DiseaseCard'
};

const props = {
  caseCounts: {
    reportedCases: 50
  },
  outbreakPotentialCategory: {
    id: 5,
    name: "Malaria",
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
    <List>
      {[1,2,3,4,5,6].map(outbreakCatId => (
        <DiseaseCard {...props} outbreakPotentialCategory={{ id: outbreakCatId }} />
      ))}
    </List>
  </div>
);