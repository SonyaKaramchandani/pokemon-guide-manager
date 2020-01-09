import React from 'react';
import { action } from '@storybook/addon-actions';
import ReferenceSources from './ReferenceSources';

export default {
  title: 'ReferenceSources'
};

const sources = [];

export const mini = () => <ReferenceSources sources={sources} mini={true} />;
export const full = () => <ReferenceSources sources={sources} label="Sources from" mini={false} />;
