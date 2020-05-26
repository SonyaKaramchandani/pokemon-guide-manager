import * as dto from 'client/dto';
import * as _ from 'lodash';

import { ProximalCaseVM } from 'models/EventModels';
import { MapShapeVM } from 'models/GeonameModels';
import { RiskLikelihood, RiskMagnitude } from 'models/RiskCategories';

import { mapToNumericDictionary } from './arrayHelpers';
import { parseAndAugmentMapShapes } from './geonameHelper';

export const map2RiskLikelihood = (
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
    ? 'Very high'
    : 'Not calculated';
};

export const getRiskLikelihood = (risk: dto.RiskModel): RiskLikelihood =>
  risk
    ? map2RiskLikelihood(risk.minProbability, risk.maxProbability, risk.isModelNotRun)
    : 'Not calculated';

export const map2RiskMagnitude = (
  minVal: number,
  maxVal: number,
  isModelNotRun = false
): RiskMagnitude => {
  const avg = (minVal + maxVal) / 2;
  return isModelNotRun
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
    ? map2RiskMagnitude(risk.minMagnitude, risk.maxMagnitude, risk.isModelNotRun)
    : 'Not calculated';

export const MapProximalLocations2VM = (
  proximalLocations: dto.ProximalCaseCountModel[]
): ProximalCaseVM => {
  if (!proximalLocations) return null;
  return {
    dictByLocationId: mapToNumericDictionary(
      proximalLocations,
      x => x.locationId,
      x => x
    ),
    geonameIds: proximalLocations.map(e => e.locationId),
    totalCasesIn: proximalLocations.reduce(
      (acc, x) => acc + (x.isWithinLocation ? x.proximalCases : 0),
      0
    ),
    totalCasesNear: proximalLocations.reduce(
      (acc, x) => acc + (!x.isWithinLocation ? x.proximalCases : 0),
      0
    ),
    totalCases: proximalLocations.reduce((acc, x) => acc + x.proximalCases, 0),
    casesCityLevel: _.chain(proximalLocations)
      .filter(x => x.locationType === dto.LocationType.City)
      .orderBy([x => (x.isWithinLocation ? 0 : 1), x => x.locationName])
      .value(),
    casesProvinceCountryLevel: _.chain(proximalLocations)
      .filter(
        x =>
          x.locationType === dto.LocationType.Province ||
          x.locationType === dto.LocationType.Country
      )
      .map(x => ({
        ...x,
        locationTypeSubtitle:
          x.locationType === dto.LocationType.Province ? 'Province/State' : 'Country'
      }))
      .orderBy([x => (x.isWithinLocation ? 0 : 1), x => x.locationType, x => x.locationName])
      .value()
  };
};

export function MapShapesToProximalMapShapes(
  shapes: dto.GetGeonameModel[],
  proximalVM: ProximalCaseVM
): MapShapeVM<dto.ProximalCaseCountModel>[] {
  return parseAndAugmentMapShapes<dto.ProximalCaseCountModel>(
    shapes,
    geonameId => proximalVM.dictByLocationId[geonameId]
  );
}
