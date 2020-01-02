import React from 'react';
import { action } from '@storybook/addon-actions';
import ListItem from './ListItem';

export default {
  title: 'ListItem'
};

export const active = () => <ListItem>Hello ListItem</ListItem>;
