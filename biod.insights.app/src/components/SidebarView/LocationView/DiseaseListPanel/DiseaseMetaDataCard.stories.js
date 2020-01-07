import React from 'react';
import { action } from '@storybook/addon-actions';
import DiseaseMetaDataCard from './DiseaseMetaDataCard';

const props = {
  casesInfo: {
    reportedCases: 50
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

export default {
  title: 'DiseaseMetaDataCard'
};

export const text =  () => (
  <div style={{ width: 350, padding: '1rem' }}>
    <DiseaseMetaDataCard {...props} />
  </div>
);
