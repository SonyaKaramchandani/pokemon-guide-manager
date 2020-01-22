import React from 'react';
import { action } from '@storybook/addon-actions';
import DiseaseListPanel from './DiseaseListPanel';

export default {
  title: 'PANELS/DiseaseListPanel'
};

const props = {

};

export const test =  () => (
  <div style={{ width: 370, padding: '10px' }}>
    <h1>TODO!</h1>
    <DiseaseListPanel {...props} />
  </div>
);