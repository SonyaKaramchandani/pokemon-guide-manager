import React from 'react';
import { linkTo } from '@storybook/addon-links';
import { action } from '@storybook/addon-actions';
import EventListPanel from './EventListPanel';
import { mockApiCallPromise } from 'components/_debug/mockApiCallPromise'

export default {
  title: 'PANELS/EventListPanel'
};

// TODO: c8c632ef: move mock data to common file
const mockGetEventListModel = {"importationRisk":null,"exportationRisk":null,"eventsList":[{"isLocal":false,"eventInformation":{"id":482,"title":"Measles in Toronto","startDate":"2018-09-20T00:00:00","endDate":null,"lastUpdatedDate":"2019-01-04T17:40:50","summary":"Testing fgfdfrervdfrte","diseaseId":10},"importationRisk":null,"exportationRisk":{"isModelNotRun":false,"minProbability":1,"maxProbability":1,"minMagnitude":18979.787,"maxMagnitude":19107.598},"caseCounts":{"confirmedCases":0,"reportedCases":555555,"suspectedCases":1,"deaths":0,"hasConfirmedCasesNesting":false,"hasReportedCasesNesting":false,"hasSuspectedCasesNesting":false,"hasDeathsNesting":false},"eventLocations":[{"geonameId":6167865,"locationName":"Toronto, Ontario, Canada","provinceName":"Ontario","countryName":"Canada","locationType":2,"caseCounts":{"confirmedCases":0,"reportedCases":555555,"suspectedCases":1,"deaths":0,"hasConfirmedCasesNesting":false,"hasReportedCasesNesting":false,"hasSuspectedCasesNesting":false,"hasDeathsNesting":false}}],"articles":[],"diseaseInformation":{"id":10,"name":null,"agents":null,"agentTypes":null,"transmissionModes":null,"incubationPeriod":null,"preventionMeasure":null,"biosecurityRisk":null}},{"isLocal":false,"eventInformation":{"id":514,"title":"Oct 09 Test event","startDate":"2018-10-01T00:00:00","endDate":null,"lastUpdatedDate":"2019-01-02T10:09:09","summary":"Testing for chagas","diseaseId":52},"importationRisk":null,"exportationRisk":{"isModelNotRun":false,"minProbability":1,"maxProbability":1,"minMagnitude":9775.144,"maxMagnitude":9842.862},"caseCounts":{"confirmedCases":4,"reportedCases":555555,"suspectedCases":5,"deaths":22,"hasConfirmedCasesNesting":false,"hasReportedCasesNesting":false,"hasSuspectedCasesNesting":false,"hasDeathsNesting":false},"eventLocations":[{"geonameId":1816670,"locationName":"Beijing, Beijing Shi, China","provinceName":"Beijing Shi","countryName":"China","locationType":2,"caseCounts":{"confirmedCases":4,"reportedCases":555555,"suspectedCases":5,"deaths":22,"hasConfirmedCasesNesting":false,"hasReportedCasesNesting":false,"hasSuspectedCasesNesting":false,"hasDeathsNesting":false}}],"articles":[],"diseaseInformation":{"id":52,"name":null,"agents":null,"agentTypes":null,"transmissionModes":null,"incubationPeriod":null,"preventionMeasure":null,"biosecurityRisk":null}},{"isLocal":false,"eventInformation":{"id":2129,"title":"Swine Influenza in Mexico","startDate":"2019-11-26T00:00:00","endDate":null,"lastUpdatedDate":"2019-11-28T12:00:43","summary":"uh oh!","diseaseId":112},"importationRisk":null,"exportationRisk":{"isModelNotRun":false,"minProbability":1,"maxProbability":1,"minMagnitude":5464.428,"maxMagnitude":5511.338},"caseCounts":{"confirmedCases":0,"reportedCases":345347,"suspectedCases":0,"deaths":0,"hasConfirmedCasesNesting":false,"hasReportedCasesNesting":false,"hasSuspectedCasesNesting":false,"hasDeathsNesting":false},"eventLocations":[{"geonameId":3816697,"locationName":"Baja California, Estado de Chiapas, Mexico","provinceName":"Estado de Chiapas","countryName":"Mexico","locationType":2,"caseCounts":{"confirmedCases":0,"reportedCases":1,"suspectedCases":0,"deaths":0,"hasConfirmedCasesNesting":false,"hasReportedCasesNesting":false,"hasSuspectedCasesNesting":false,"hasDeathsNesting":false}},{"geonameId":8581941,"locationName":"Acapulco de Juarez, Estado de Guerrero, Mexico","provinceName":"Estado de Guerrero","countryName":"Mexico","locationType":2,"caseCounts":{"confirmedCases":0,"reportedCases":345345,"suspectedCases":0,"deaths":0,"hasConfirmedCasesNesting":false,"hasReportedCasesNesting":false,"hasSuspectedCasesNesting":false,"hasDeathsNesting":false}},{"geonameId":8583659,"locationName":"Aculco, Estado de Mexico, Mexico","provinceName":"Estado de Mexico","countryName":"Mexico","locationType":2,"caseCounts":{"confirmedCases":0,"reportedCases":1,"suspectedCases":0,"deaths":0,"hasConfirmedCasesNesting":false,"hasReportedCasesNesting":false,"hasSuspectedCasesNesting":false,"hasDeathsNesting":false}}],"articles":[{"title":"The first death due to influenza in BC is recorded","url":"https://www.debate.com.mx/salud/Se-registra-la-primera-muerte-por-influenza-en-BC-20181228-0130.html","publishedDate":"2018-12-29T05:00:00","originalLanguage":"es","sourceName":"News Media"},{"title":"Influenza AH1N1 collects its first victim in BC","url":"https://www.lacronica.com/Noticias/2018/12/29/1397918-Cobra-influenza-AH1N1-su-primera-victima-en-BC.html","publishedDate":"2018-12-29T05:00:00","originalLanguage":"es","sourceName":"News Media"}],"diseaseInformation":{"id":112,"name":null,"agents":null,"agentTypes":null,"transmissionModes":null,"incubationPeriod":null,"preventionMeasure":null,"biosecurityRisk":null}}]};

const props = {
  geonameId: 2038349,
  diseaseId: 75,
  eventId: 0,
};

export const Test = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <EventListPanel
      {...props}
      onSelect={action('onSelect')}
      onClose={action('onClose')}
      onMinimize={action('onMinimize')}
    />
  </div>
);
