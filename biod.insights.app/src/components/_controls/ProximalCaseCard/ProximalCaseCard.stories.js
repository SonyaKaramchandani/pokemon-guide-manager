import React from 'react';
import { action } from '@storybook/addon-actions';
import ProximalCaseCard from './ProximalCaseCard';

export default {
  title: 'EventDetails/ProximalCaseCard'
};

export const Test = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <ProximalCaseCard />
  </div>
);

export const TestOpen = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <ProximalCaseCard isOpen />
  </div>
);
