import React from 'react';
import { linkTo } from '@storybook/addon-links';
import EventMetaDataCard from './EventMetaDataCard';

export default {
  title: 'EventMetaDataCard'
};

const caseInfo = {
    reportedCases: 100,
    deaths: 10
  },
  importationRisk = {
    minMagnitude: 1,
    maxMagnitude: 2,
    minProbability: 5,
    maxProbability: 50
  },
  exportationRisk = {
    minMagnitude: 1,
    maxMagnitude: 2,
    minProbability: 5,
    maxProbability: 50
  };

export const ImportationRisk = () => (
  <div style={{ padding: '1rem' }}>
    <EventMetaDataCard casesInfo={caseInfo} importationRisk={importationRisk} />
  </div>
);
export const ExportationRisk = () => (
  <div style={{ padding: '1rem' }}>
    <EventMetaDataCard casesInfo={caseInfo} exportationRisk={exportationRisk} />
  </div>
);
