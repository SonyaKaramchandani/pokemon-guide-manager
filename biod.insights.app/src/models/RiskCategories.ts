export type RiskDirectionType = 'importation' | 'exportation';
export type RiskLikelihood =
  | 'Not calculated'
  | 'Unlikely'
  | 'Low'
  | 'Moderate'
  | 'High'
  | 'Very high';
export type RiskMagnitude =
  | 'Not calculated'
  | 'Negligible'
  | 'Up to 10 cases'
  | '11 to 100 cases'
  | '101 to 1,000 cases'
  | '>1,000 cases';
