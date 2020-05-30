import { NumericDictionary } from 'lodash';
import * as dto from 'client/dto';
import { ProximalCaseVM } from './EventModels';

// NOTE: maps diseaseId to ProximalCaseVM
export type ProximalCaseModelMap = NumericDictionary<ProximalCaseVM>;

export type DiseaseAndProximalRiskVM = {
  disease: dto.DiseaseRiskModel;
  proximalVM: ProximalCaseVM;
  caseCounts: dto.CaseCountModel
};
