export type RiskDirectionType = 'importation' | 'exportation';
export type RiskLikelihood =
  | 'Not calculated'
  | 'Unlikely'
  | 'Low'
  | 'Moderate'
  | 'High'
  | 'Very High';
export type RiskMagnitude =
  | 'Not calculated'
  | 'Negligible'
  | 'Up to 10'
  | '11 to 100'
  | '101 to 1,000'
  | '>1,000';
