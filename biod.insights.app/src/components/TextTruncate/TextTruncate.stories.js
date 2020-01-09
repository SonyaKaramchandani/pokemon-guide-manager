import React from 'react';
import { action } from '@storybook/addon-actions';
import TextTruncate from './TextTruncate';

export default {
  title: 'TextTruncate'
};

export const text = () => <TextTruncate value="lorem ipsum" />;
