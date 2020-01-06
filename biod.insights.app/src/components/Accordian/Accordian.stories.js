import React from 'react';
import { action } from '@storybook/addon-actions';
import Accordian from './Accordian';

export default {
  title: 'Accordian'
};

export const text = () => (
  <div style={{ width: 350 }}>
    <Accordian
      title="Disease Information"
      content="Category A: High mortality rate, easily disseminated or transmitted from person to person."
    />
  </div>
);
