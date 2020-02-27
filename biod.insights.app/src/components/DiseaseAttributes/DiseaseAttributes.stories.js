import React from 'react';
import { action } from '@storybook/addon-actions';
import DiseaseAttributes from './DiseaseAttributes';
import { diseaseInformation } from '__mocks__/dtoSamples';

export default {
  title: 'DiseaseEventList/DiseaseAttributes'
};

export const text = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <DiseaseAttributes {...diseaseInformation} />
  </div>
);
