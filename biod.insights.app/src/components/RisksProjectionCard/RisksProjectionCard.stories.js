import React from 'react';
import { action } from '@storybook/addon-actions';
import RisksProjectionCard from './RisksProjectionCard';
import { Icon } from 'semantic-ui-react';

export default {
  title: 'DiseaseEvent/RisksProjectionCard'
};

const importationRisk = {
  minProbability: 0.01,
  maxProbability: 0.1,
  minMagnitude: 1.389,
  maxMagnitude: 1.552
};

const exportationRisk = {
  minProbability: 70,
  maxProbability: 100,
  minMagnitude: 19558.793,
  maxMagnitude: 19739.648
};

export const text = () => (
  <div style={{ width: 318, padding: '1rem' }}>
    <RisksProjectionCard
      importationRisk={importationRisk}
      exportationRisk={exportationRisk}
      headerIcons={
        <>
          <Icon name="signal" />
          <Icon name="plane" />
        </>
      }
    />
  </div>
);
