import * as dto from 'client/dto';
import { NumericDictionary } from 'lodash';

export type ProximalCaseEntryVM = dto.ProximalCaseCountModel & {
  locationTypeSubtitle?: string;
};

export type ProximalCaseVM = {
  dictByLocationId: NumericDictionary<dto.ProximalCaseCountModel>;
  geonameIds: number[];
  totalCasesIn: number;
  totalCasesNear: number;
  totalCases: number;
  casesCityLevel: ProximalCaseEntryVM[];
  casesProvinceCountryLevel: ProximalCaseEntryVM[];
};
