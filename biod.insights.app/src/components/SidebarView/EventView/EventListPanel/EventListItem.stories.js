import React from 'react';
import { linkTo } from '@storybook/addon-links';
import { List } from 'semantic-ui-react';
import EventListItem from './EventListItem';

export default {
  title: 'DiseaseEvent/EventListItem'
};

const caseInfo = {
    reportedCases: 100,
    deaths: 10
  },
  eventInformation = {
    id: 'eventID',
    title: 'Event title',
    summary: 'event summary.'
  },
  importationRisk = {
    minMagnitude: 1,
    maxMagnitude: 2,
    minProbability: 5,
    maxProbability: 50
  },
  exportationRisk = {
    minMagnitude: 10,
    maxMagnitude: 25,
    minProbability: 15,
    maxProbability: 51
  };

export const Test = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <List>
      <EventListItem eventInformation={eventInformation} caseCounts={caseInfo} importationRisk={importationRisk} />
      <EventListItem eventInformation={eventInformation} caseCounts={caseInfo} importationRisk={importationRisk} />
      <EventListItem eventInformation={eventInformation} caseCounts={caseInfo} exportationRisk={exportationRisk} />
      <EventListItem eventInformation={eventInformation} caseCounts={caseInfo} exportationRisk={exportationRisk} />
    </List>
  </div>
);
