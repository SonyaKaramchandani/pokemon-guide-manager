import React from 'react';
import { action } from '@storybook/addon-actions';
import ProbabilityIcons from './ProbabilityIcons';

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

export const Importation = () => <ProbabilityIcons importationRisk={importationRisk} />;
export const Exportation = () => <ProbabilityIcons exportationRisk={exportationRisk} />;
