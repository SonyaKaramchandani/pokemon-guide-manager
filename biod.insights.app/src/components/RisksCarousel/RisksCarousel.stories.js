import React from 'react';
import { action } from '@storybook/addon-actions';
import RisksCarousel from './RisksCarousel';
import { Icon } from 'semantic-ui-react';

const importationRisk = {
  minProbability: 1,
  maxProbability: 1,
  minMagnitude: 18.389,
  maxMagnitude: 18.552
};

const exportationRisk = {
  minProbability: 1,
  maxProbability: 1,
  minMagnitude: 19558.793,
  maxMagnitude: 19739.648
};

export default {
  title: 'RisksCarousel'
};

export const text = () => (
  <RisksCarousel
    importationRisk={importationRisk}
    exportationRisk={exportationRisk}
    headerIcons={
      <>
        <Icon name="signal" />
        <Icon name="plane" />
      </>
    }
  />
);
