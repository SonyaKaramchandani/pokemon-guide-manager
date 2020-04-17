// TODO: 620d250c: maybe need to designate a common place for all shared TS model types?
export type RiskLevel = 'None' | 'Low' | 'Medium' | 'High' | 'NotAvailable';

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
