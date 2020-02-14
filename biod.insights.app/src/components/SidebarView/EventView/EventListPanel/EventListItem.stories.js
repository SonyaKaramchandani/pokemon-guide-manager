import React from 'react';
import { linkTo } from '@storybook/addon-links';
import { List } from 'semantic-ui-react';
import EventListItem from './EventListItem';

export default {
  title: 'EventList/EventListItem'
};

const caseInfo = {
    reportedCases: 100,
    deaths: 10
  },
  eventInformation = {
    id: 'eventID',
    title: 'Event title',
    summary: 'Cases of measles are being reported in Slovenia since the beginning of the year. Public health is in the...'
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
  },
  articles = [
    {
      title: '"Combining a vaccine with current methods would allow HIV eradication"',
      url:
        'http://www.rfi.fr/es/salud/20190724-combinar-una-vacuna-con-los-metodos-actuales-permitiria-erradicar-el-vih',
      publishedDate: '2019-08-06T18:15:00',
      originalLanguage: 'es',
      sourceName: 'News Media'
    },
    {
      title: 'This title has no a period from API',
      url:
        'https://navbharattimes.indiatimes.com/lifestyle/health/chikungunya-fever-causes-signs-symptoms-and-prevention/articleshow/70558798.cms',
      publishedDate: '2019-08-06T17:45:00',
      originalLanguage: 'hi',
      sourceName: 'News Media'
    }
  ];

export const testList = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <List>
      <EventListItem
        eventInformation={eventInformation}
        caseCounts={caseInfo}
        articles={articles}
        importationRisk={importationRisk}
      />
      <EventListItem
        eventInformation={eventInformation}
        articles={articles}
        caseCounts={caseInfo}
        importationRisk={importationRisk}
      />
      <EventListItem
        eventInformation={eventInformation}
        articles={articles}
        caseCounts={caseInfo}
        exportationRisk={exportationRisk}
      />
      <EventListItem
        eventInformation={eventInformation}
        articles={articles}
        caseCounts={caseInfo}
        exportationRisk={exportationRisk}
      />
    </List>
  </div>
);

export const standAlone = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <List>
      <EventListItem
        eventInformation={eventInformation}
        articles={articles}
        caseCounts={caseInfo}
        importationRisk={importationRisk}
        isStandAlone={true}
      />
      <EventListItem
        eventInformation={eventInformation}
        articles={articles}
        caseCounts={caseInfo}
        importationRisk={importationRisk}
        isStandAlone={true}
      />
      <EventListItem
        eventInformation={eventInformation}
        articles={articles}
        caseCounts={caseInfo}
        exportationRisk={exportationRisk}
        isStandAlone={true}
      />
      <EventListItem
        eventInformation={eventInformation}
        caseCounts={caseInfo}
        exportationRisk={exportationRisk}
        isStandAlone={true}
      />
    </List>
  </div>
);
