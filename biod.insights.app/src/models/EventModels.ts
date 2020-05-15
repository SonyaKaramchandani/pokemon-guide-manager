import * as dto from 'client/dto';

export type ProximalCaseEntryVM = dto.ProximalCaseCountModel & {
  locationTypeSubtitle?: string;
};

export type ProximalCaseVM = {
  totalCasesIn: number;
  totalCasesNear: number;
  totalCases: number;
  casesCityLevel: ProximalCaseEntryVM[];
  casesProvinceCountryLevel: ProximalCaseEntryVM[];
};
