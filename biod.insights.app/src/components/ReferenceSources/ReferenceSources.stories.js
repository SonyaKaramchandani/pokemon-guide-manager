import React from 'react';
import { action } from '@storybook/addon-actions';
import ReferenceSources from './ReferenceSources';

export default {
  title: 'EventDetails/ReferenceSources'
};

// dto: ArticleModel
const sources = [
  {
    originalLanguage: 'originalLanguage',
    publishedDate: '2018-01-01T00:00:00',
    sourceName: 'sourceName',
    title: 'title',
    url: 'url'
  },
  {
    originalLanguage: 'originalLanguage2',
    publishedDate: '2018-01-01T00:00:00',
    sourceName: 'sourceName2',
    title: 'title2',
    url: 'url2'
  },
  {
    originalLanguage: 'originalLanguage3',
    publishedDate: '2018-01-01T00:00:00',
    sourceName: 'sourceName3',
    title: 'title3',
    url: 'url3'
  },
  {
    originalLanguage: 'originalLanguage4',
    publishedDate: '2018-01-01T00:00:00',
    sourceName: 'sourceName4',
    title: 'title4',
    url: 'url4'
  }
];

export const mini = () => <ReferenceSources articles={sources} mini={true} />;
export const full = () => <ReferenceSources articles={sources} mini={false} />;
