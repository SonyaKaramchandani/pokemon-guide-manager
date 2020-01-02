import React from 'react';
import { action } from '@storybook/addon-actions';
import List from './List';

export default {
  title: 'List'
};

export const active = () => <List>Hello List</List>;
