import { RiskLevel, RiskLikelihood } from 'models/RiskCategories';

// TODO: PT-1301: this is old, need to replace with RiskLikelihood
export const getRiskLevel = (maxProb: number): RiskLevel => {
  if (maxProb !== undefined && maxProb !== null && maxProb >= 0) {
    if (maxProb < 0.01 && maxProb >= 0) {
      return 'None';
    }
    if (maxProb < 0.2) {
      return 'Low';
    }
    if (maxProb >= 0.2 && maxProb <= 0.7) {
      return 'Medium';
    }
    if (maxProb > 0.7) {
      return 'High';
    }
  }
  return 'NotAvailable';
};

/**
 * Formats the min to max values to an interval display text
 * @param {number} minVal - min interval value
 * @param {number} maxVal - max interval value
 * @return {string} Formatted interval string representation
 */
export const getInterval = (
  minVal: number,
  maxVal: number,
  isModelNotRun = false
): RiskLikelihood => {
  const avg = (minVal + maxVal) / 2;
  return isModelNotRun
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
    ? 'Very High'
    : 'Not calculated';
};
