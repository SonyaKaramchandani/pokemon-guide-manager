import { RiskLikelihood, RiskMagnitude } from 'models/RiskCategories';
import * as dto from 'client/dto';

// TODO: 513544c4: rename!
export const getInterval = (
  minVal: number,
  maxVal: number,
  isModelNotRun = false
): RiskLikelihood => {
  const avg = (minVal + maxVal) / 2;
  return isModelNotRun ||
    minVal === null ||
    minVal === undefined ||
    maxVal === null ||
    maxVal === undefined
    ? 'Not calculated'
    : avg < 0.01
    ? 'Unlikely'
    : avg <= 0.1
    ? 'Low'
    : avg <= 0.5
    ? 'Moderate'
    : avg <= 0.9
    ? 'High'
    : avg <= 1
    ? 'Very high'
    : 'Not calculated';
};

export const getRiskLikelihood = (risk: dto.RiskModel): RiskLikelihood =>
  risk
    ? getInterval(risk.minProbability, risk.maxProbability, risk.isModelNotRun)
    : 'Not calculated';

// TODO: 513544c4: rename!
export const getTravellerInterval = (
  minVal: number,
  maxVal: number,
  isModelNotRun = false
): RiskMagnitude => {
  const avg = (minVal + maxVal) / 2;
  return isModelNotRun ||
    minVal === null ||
    minVal === undefined ||
    maxVal === null ||
    maxVal === undefined
    ? 'Not calculated'
    : minVal === 0 && maxVal === 0
    ? 'Negligible'
    : avg <= 10
    ? 'Up to 10 cases'
    : avg <= 100
    ? '11 to 100 cases'
    : avg <= 1000
    ? '101 to 1,000 cases'
    : avg > 1000
    ? '>1,000 cases'
    : 'Not calculated';
};

export const getRiskMagnitude = (risk: dto.RiskModel): RiskMagnitude =>
  risk
    ? getTravellerInterval(risk.minMagnitude, risk.maxMagnitude, risk.isModelNotRun)
    : 'Not calculated';
