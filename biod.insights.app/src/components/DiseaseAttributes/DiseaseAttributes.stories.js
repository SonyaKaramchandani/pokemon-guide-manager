import React from 'react';
import { action } from '@storybook/addon-actions';
import DiseaseAttributes from './DiseaseAttributes';

export default {
  title: 'DiseaseAttributes'
};

// TODO: c8c632ef: move mock data to common file
const diseaseInformation = {
  agents: 'Bacillus anthracis',
  agentType: 'Bacteria',
  transmissionModes: 'Airborne, Zoonotic Fluid Transmission',
  incubationPeriod: '?',
  preventionMeasure: 'Vaccine',
  biosecurityRisk:
    'Category A: High mortality rate, easily disseminated or transmitted from person to person.'
};

export const text = () => <DiseaseAttributes {...diseaseInformation} />;
