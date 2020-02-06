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

const mockRisk = prob => {
  return {
    minProbability: prob * 0.7,
    maxProbability: prob,
    minMagnitude: 10,
    maxMagnitude: 10
  };
};

//=====================================================================================================================================

export const Importation = () => (
  <div style={{ width: 370, padding: '200px 10px' }}>
    <FlexGroup suffix={<ProbabilityIcons importationRisk={importationRisk} />}>some text</FlexGroup>
  </div>
);

export const Exportation = () => <ProbabilityIcons exportationRisk={exportationRisk} />;

export const Varia = () => (
  <div style={{ width: 370, padding: '200px 10px' }}>
    <FlexGroup suffix={<ProbabilityIcons importationRisk={mockRisk(0.001)} />}>
      {'maxProb < 0.01'}
    </FlexGroup>
    <FlexGroup suffix={<ProbabilityIcons importationRisk={mockRisk(0.1)} />}>
      {'maxProb < 0.2'}
    </FlexGroup>
    <FlexGroup suffix={<ProbabilityIcons importationRisk={mockRisk(0.5)} />}>
      {'maxProb >= 0.2 && maxProb <= 0.7'}
    </FlexGroup>
    <FlexGroup suffix={<ProbabilityIcons importationRisk={mockRisk(5)} />}>
      {'maxProb > 0.7'}
    </FlexGroup>
  </div>
);
