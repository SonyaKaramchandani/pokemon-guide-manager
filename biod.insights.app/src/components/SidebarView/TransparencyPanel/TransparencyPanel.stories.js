import React from 'react';
import { action } from '@storybook/addon-actions';
import TransparencyPanel from './TransparencyPanel';
import { mockGetEventModel } from '__mocks__/dtoSamples';

export default {
  title: 'PANELS/TransparencyPanel'
};

const props = {
  event: mockGetEventModel,
  onClose: action('onClose'),
  onMinimize: action('onMinimize')
};

export const importation = () => (
  <div style={{ width: 370, padding: '10px', minHeight: '1200px' }}>
    <TransparencyPanel {...props} riskType="importation" />
  </div>
);

export const exportation = () => (
  <div style={{ width: 370, padding: '10px', minHeight: '1200px' }}>
    <TransparencyPanel {...props} riskType="exportation" />
  </div>
);
