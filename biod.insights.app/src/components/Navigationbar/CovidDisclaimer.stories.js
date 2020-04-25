import React from 'react';
import { action } from '@storybook/addon-actions';
import { CovidDisclaimer } from './CovidDisclaimer';
export default {
  title: 'Transpar/CovidDisclaimer'
};

export const test = () => <CovidDisclaimer onClose={null} />;
