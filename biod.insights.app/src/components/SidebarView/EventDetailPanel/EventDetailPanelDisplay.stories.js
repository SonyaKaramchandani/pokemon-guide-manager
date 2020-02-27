import React from 'react';
import { action } from '@storybook/addon-actions';
import EventDetailPanelDisplay from './EventDetailPanelDisplay';
import OutbreakSurveillanceOverall from './OutbreakSurveillanceOverall';
import { mockGetEventModel } from '__mocks__/dtoSamples';
import { DebugContainer4BdPanel } from 'components/_debug/StorybookContainer';
import { Grid } from 'semantic-ui-react';

export default {
  title: 'PANELS/EventDetailPanel'
};

// TODO: 9eae0d15: need to decouple for storybook and pass mock data (no webcalls in storybook!)

export const test = () => (
  <DebugContainer4BdPanel>
    <EventDetailPanelDisplay
      event={mockGetEventModel}
      isLoading={false}
      onClose={action('closed')}
    />
  </DebugContainer4BdPanel>
);

export const outbreakSurveillance = () => (
  <Grid columns={3} divided="vertically">
    <Grid.Row divided>
      <Grid.Column></Grid.Column>
      <Grid.Column>
        <DebugContainer4BdPanel>
          <OutbreakSurveillanceOverall
            caseCounts={mockGetEventModel.caseCounts}
            eventLocations={mockGetEventModel.eventLocations}
          />
        </DebugContainer4BdPanel>
      </Grid.Column>
    </Grid.Row>
  </Grid>
);
