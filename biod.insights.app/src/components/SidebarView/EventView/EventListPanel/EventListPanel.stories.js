import React from 'react';
import { linkTo } from '@storybook/addon-links';
import EventListPanel from './EventListPanel';

export default {
  title: 'DiseaseEvent/EventListPanel'
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

export const Test = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <EventListPanel caseCounts={caseInfo} importationRisk={importationRisk} />
  </div>
);
