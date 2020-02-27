import React, { useState } from 'react';
import { action } from '@storybook/addon-actions';
import MobilePanelSummary from './MobilePanelSummary';

export default {
  title: 'Controls/Mobile Panel Summary'
};

export const loading = () => (
  <div style={{ width: 350, height: '60vh' }}>
    <MobilePanelSummary summaryTitle="Mobile Panel Summary" onClick={action('onClick')} />
  </div>
);
