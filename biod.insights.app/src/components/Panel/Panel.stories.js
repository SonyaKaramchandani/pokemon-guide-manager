import React from 'react';
import { action } from '@storybook/addon-actions';
import Panel from './Panel';

export default {
  title: 'Panel'
};

export const text = () => (
  <div style={{ width: 350 }}>
    <Panel title="Panel">
      <div>
        Hello Panel
        <ul>
          <li>Item11</li>
          <li>Item12</li>
          <li>Item13</li>
          <li>Item14</li>
        </ul>
      </div>
    </Panel>
  </div>
);
