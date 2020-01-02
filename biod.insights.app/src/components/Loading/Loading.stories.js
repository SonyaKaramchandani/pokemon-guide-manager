import React from 'react';
import { action } from '@storybook/addon-actions';
import Loading from './Loading';

export default {
  title: 'Loading'
};

export const active = () => <Loading>Hello Loading</Loading>;
