import React from 'react';
import { linkTo } from '@storybook/addon-links';
import { action } from '@storybook/addon-actions';
import EventListPanel from './EventListPanel';
import { mockGetEventListModel } from '__mocks__/dtoSamples';
import { DebugContainer4BdPanel } from 'components/_debug/StorybookContainer';

export default {
  title: 'PANELS/EventListPanel'
};

const props = {
  geonameId: 2038349,
  diseaseId: 75,
  eventId: 0,
  events: mockGetEventListModel
};

export const test = () => (
  <DebugContainer4BdPanel>
    <EventListPanel
      {...props}
      onEventSelected={action('onSelect')}
      onClose={action('onClose')}
      onMinimize={action('onMinimize')}
    />
  </DebugContainer4BdPanel>
);

export const loading = () => (
  <DebugContainer4BdPanel>
    <EventListPanel
      {...props}
      // events={{}}
      isEventListLoading={true}
      onEventSelected={action('onSelect')}
      onClose={action('onClose')}
      onMinimize={action('onMinimize')}
    />
  </DebugContainer4BdPanel>
);
