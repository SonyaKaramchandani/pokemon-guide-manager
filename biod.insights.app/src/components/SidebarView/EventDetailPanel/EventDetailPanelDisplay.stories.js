import React from 'react';
import { action } from '@storybook/addon-actions';
import EventDetailPanelDisplay from './EventDetailPanelDisplay';
import OutbreakSurveillanceOverall from './OutbreakSurveillanceOverall';
import { mockGetEventModel } from '__mocks__/dtoSamples';

export default {
  title: 'PANELS/EventDetailPanel'
};

// TODO: 9eae0d15: need to decouple for storybook and pass mock data (no webcalls in storybook!)

export const test = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <EventDetailPanelDisplay
      event={mockGetEventModel}
      isLoading={false}
      onClose={action('closed')} />
  </div>
);

export const outbreakSurveillance = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <OutbreakSurveillanceOverall
      caseCounts={mockGetEventModel.caseCounts}
      eventLocations={mockGetEventModel.eventLocations}
    />
  </div>
);

