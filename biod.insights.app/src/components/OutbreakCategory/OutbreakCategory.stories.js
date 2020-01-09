import React from 'react';
import { action } from '@storybook/addon-actions';
import OutbreakCategory from './OutbreakCategory';

export default {
  title: 'OutbreakCategory'
};

export const active = () => <OutbreakCategory>Hello OutbreakCategory</OutbreakCategory>;
