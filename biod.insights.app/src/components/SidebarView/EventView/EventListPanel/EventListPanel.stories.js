import React from 'react';
import { linkTo } from '@storybook/addon-links';
import { action } from '@storybook/addon-actions';
import EventListPanel from './EventListPanel';

export default {
  title: 'DiseaseEvent/EventListPanel'
};

// TODO: 9eae0d15: need to decouple for storybook and pass mock data (no webcalls in storybook!)

const props = {
  geonameId: 2038349,
  diseaseId: 75,
  eventId: 0,
};

export const Test = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <EventListPanel
      {...props}
      onEventListLoad={action('onEventListLoad')}
      onSelect={action('onSelect')}
      onClose={action('onClose')}
      onMinimize={action('onMinimize')}
    />
  </div>
);
