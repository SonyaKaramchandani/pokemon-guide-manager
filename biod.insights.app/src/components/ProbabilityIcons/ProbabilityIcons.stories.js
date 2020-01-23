import React from 'react';
import { action } from '@storybook/addon-actions';
import ProbabilityIcons from './ProbabilityIcons';
import { FlexGroup } from 'components/_common/FlexGroup';

export default {
  title: 'Controls/ProbabilityIcons'
};

const importationRisk = {
  minProbability: 100.01,
  maxProbability: 100.1,
  minMagnitude: 100.389,
  maxMagnitude: 100.552
};

const exportationRisk = {
  minProbability: 70,
  maxProbability: 100,
  minMagnitude: 19558.793,
  maxMagnitude: 19739.648
};

export const Importation = () => (
  <div style={{ width: 370, padding: '100px 10px' }}>
    <FlexGroup suffix={
      <ProbabilityIcons importationRisk={importationRisk} />
    }>
      some text
    </FlexGroup>
  </div>
);
export const Exportation = () => <ProbabilityIcons exportationRisk={exportationRisk} />;


