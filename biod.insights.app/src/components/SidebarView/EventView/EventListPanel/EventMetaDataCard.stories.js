import React from 'react';
import { linkTo } from '@storybook/addon-links';
import EventMetaDataCard from './EventMetaDataCard';

export default {
  title: 'DiseaseEvent/EventMetaDataCard'
};

const caseInfo = {
    reportedCases: 100,
    deaths: 10
  },
  importationRisk = { // TODO: 6116adf1
    minMagnitude: 1,
    maxMagnitude: 2,
    minProbability: 5,
    maxProbability: 50
  },
  // TODO: b1a90ae0: dto: Biod.Insights.Api.Models.RiskModel
  exportationRisk = {
    minMagnitude: 10,
    maxMagnitude: 25,
    minProbability: 15,
    maxProbability: 51
  };

// TODO: 6116adf1
export const ImportationRisk = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <EventMetaDataCard caseCounts={caseInfo} importationRisk={importationRisk} />
  </div>
);
export const ExportationRisk = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <EventMetaDataCard caseCounts={caseInfo} exportationRisk={exportationRisk} />
  </div>
);
