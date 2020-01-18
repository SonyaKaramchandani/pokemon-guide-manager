import React from 'react';
import { action } from '@storybook/addon-actions';
import ReferenceList from './ReferenceList';

export default {
  title: 'DiseaseEvent/ReferenceList'
};

const articles = [
  {
    title: '"Combining a vaccine with current methods would allow HIV eradication"',
    url:
      'http://www.rfi.fr/es/salud/20190724-combinar-una-vacuna-con-los-metodos-actuales-permitiria-erradicar-el-vih',
    publishedDate: '2019-08-06T18:15:00',
    originalLanguage: 'es',
    sourceName: 'News Media'
  },
  {
    title:
      'This title has no a period from API',
    url:
      'https://navbharattimes.indiatimes.com/lifestyle/health/chikungunya-fever-causes-signs-symptoms-and-prevention/articleshow/70558798.cms',
    publishedDate: '2019-08-06T17:45:00',
    originalLanguage: 'hi',
    sourceName: 'News Media'
  },
  {
    title:
      'This title ends with a period from API.',
    url:
      'https://navbharattimes.indiatimes.com/lifestyle/health/chikungunya-fever-causes-signs-symptoms-and-prevention/articleshow/70558798.cms',
    publishedDate: '2019-08-06T17:45:00',
    originalLanguage: 'hi',
    sourceName: 'News Media'
  },
  {
    title:
      '2 chikungunya fever: Learn chikungunya fever symptoms, causes and prevention methods - chikungunya fever causes, signs, symptoms and prevention',
    url:
      'https://navbharattimes.indiatimes.com/lifestyle/health/chikungunya-fever-causes-signs-symptoms-and-prevention/articleshow/70558798.cms',
    publishedDate: '2019-08-06T17:45:00',
    originalLanguage: 'hi',
    sourceName: 'News Media'
  },
  {
    title:
      '3 chikungunya fever: Learn chikungunya fever symptoms, causes and prevention methods - chikungunya fever causes, signs, symptoms and prevention',
    url:
      'https://navbharattimes.indiatimes.com/lifestyle/health/chikungunya-fever-causes-signs-symptoms-and-prevention/articleshow/70558798.cms',
    publishedDate: '2019-08-06T17:45:00',
    originalLanguage: 'hi',
    sourceName: 'News Media'
  }
];

export const test =  () => (
  <div style={{ width: 370, padding: '10px' }}>
    <ReferenceList articles={articles} />
  </div>
);