import React from 'react';
import { action } from '@storybook/addon-actions';
import { OutbreakCategory, OutbreakCategoryMessage } from './OutbreakCategory';
import { Message } from 'semantic-ui-react';

export default {
  title: 'Controls/OutbreakCategory'
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

export const list = () => (
  <div style={{ width: 370, padding: '10px' }}>
    {[1,2,4,6].map(outbreakCatId => (
      <OutbreakCategoryMessage
        key={outbreakCatId}
        outbreakPotentialCategory={{ id: outbreakCatId }}
        diseaseInformation={diseaseInformation}
      />
    ))}
  </div>
)
