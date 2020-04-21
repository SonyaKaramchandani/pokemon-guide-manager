export type RiskDirectionType = 'importation' | 'exportation';
export type RiskLevel = 'None' | 'Low' | 'Medium' | 'High' | 'NotAvailable';
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
  | '11-100'
  | '101-1000'
  | '>1000';
