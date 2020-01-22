import React from 'react';
import { linkTo } from '@storybook/addon-links';
import { action } from '@storybook/addon-actions';
import EventListPanel from './EventListPanel';
import { mockGetEventListModel } from '__mocks__/dtoSamples';

export default {
  title: 'PANELS/EventListPanel'
};

const props = {
  geonameId: 2038349,
  diseaseId: 75,
  eventId: 0,
  events: mockGetEventListModel
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
