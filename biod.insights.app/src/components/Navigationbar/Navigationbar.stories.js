import React from 'react';
import { action } from '@storybook/addon-actions';
import { Navigationbar } from './Navigationbar';
export default {
  title: 'PANELS/Navigationbar'
};
export const test =  () => (
  <div sx={{ width: '100vw', height: '100vh', p: '10px' }}>
    <Navigationbar />
  </div>
);