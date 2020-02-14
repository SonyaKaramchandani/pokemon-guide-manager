import React from 'react';
import { action } from '@storybook/addon-actions';
import ProximalCasesSection from './ProximalCasesSection';

export default {
  title: 'Controls/ProximalCasesSection'
};

export const text = () => <ProximalCasesSection localCaseCounts={{ reportedCases: 1 }} />;
