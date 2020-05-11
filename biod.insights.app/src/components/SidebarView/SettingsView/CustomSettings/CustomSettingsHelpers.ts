import * as dto from 'client/dto';

import { mapToNumericDictionary } from 'utils/arrayHelpers';
import { containsNoCaseNoLocale } from 'utils/stringHelpers';

import {
  DiseaseGroupingVM,
  DiseaseNotificationLevel,
  DiseaseNotificationLevelDict,
  DiseaserRowVM,
  CustomSettingsFM,
  CustomSettingsSubmitData
} from './CustomSettingsModels';

export function Map2DiseaseGroupingVM(
  rawGroups: dto.DiseaseGroupModel[],
  diseases: { [key: number]: dto.DiseaseInformationModel },
  filterText: string
): DiseaseGroupingVM {
  function Map2DiseaseGroupingVM_inner(rawGroups: dto.DiseaseGroupModel[]): DiseaseGroupingVM[] {
    function mapDiseases(ids: number[]) {
      if (!ids) return [];
      const allMapped = ids.map(
        diseaseId =>
          ({
            id: diseaseId,
            name: (diseases[diseaseId] && diseases[diseaseId].name) || null
          } as DiseaserRowVM)
      );
      return filterText && filterText.length > 0
        ? allMapped.filter(vm => containsNoCaseNoLocale(vm.name, filterText))
        : allMapped;
    }

    function* generatorFlattenDiseaseIds(diseaseGroup: dto.DiseaseGroupModel): Iterable<number> {
      if (diseaseGroup.subGroups)
        for (const subgroup of diseaseGroup.subGroups) {
          for (const flattenDisease of generatorFlattenDiseaseIds(subgroup)) yield flattenDisease;
        }
      if (diseaseGroup.diseaseIds)
        for (const id of diseaseGroup.diseaseIds) {
          yield id;
        }
    }

    return rawGroups
      .map(
        group =>
          ({
            name: group.groupName,
            subgroups: group.subGroups && Map2DiseaseGroupingVM_inner(group.subGroups),
            diseases: group.diseaseIds && mapDiseases(group.diseaseIds),
            diseaseIdsFlattened: Array.from(generatorFlattenDiseaseIds(group))
          } as DiseaseGroupingVM)
      )
      .filter(groupVM => !!groupVM.subgroups || (groupVM.diseases && groupVM.diseases.length > 0));
  }

  return {
    name: 'root',
    subgroups: Map2DiseaseGroupingVM_inner(rawGroups),
    diseaseIdsFlattened: Object.keys(diseases).map(x => parseInt(x))
  };
}

export function GetUnanimousNotificationLevelSelectedInGroupOrNull(
  diseaseGroup: DiseaseGroupingVM,
  diseaseFormData: DiseaseNotificationLevelDict
): DiseaseNotificationLevel {
  if (!diseaseGroup || !diseaseFormData) return null;
  const diseasIds = diseaseGroup.diseaseIdsFlattened;
  const diseasValues = diseasIds.map(id => diseaseFormData[id] || null);
  diseasValues.sort();
  if (diseasValues.length > 1)
    return diseasValues[0] === diseasValues[diseasValues.length - 1] ? diseasValues[0] : null;
  if (diseasValues.length === 1) return diseasValues[0];
  return null;
}

export function MapDiseaseRelevance2DiseaseNotificationLevelDict(
  raw: dto.DiseaseRelevanceSettingsModel
): DiseaseNotificationLevelDict {
  return {
    ...mapToNumericDictionary(
      raw.alwaysNotifyDiseaseIds,
      x => x,
      x => 'always' as DiseaseNotificationLevel
    ),
    ...mapToNumericDictionary(
      raw.neverNotifyDiseaseIds,
      x => x,
      x => 'never' as DiseaseNotificationLevel
    ),
    ...mapToNumericDictionary(
      raw.riskOnlyDiseaseIds,
      x => x,
      x => 'risky' as DiseaseNotificationLevel
    )
  } as DiseaseNotificationLevelDict;
}

export function MapCustomSettingsSubmitData2DtoPayload(
  data: CustomSettingsSubmitData
): dto.UserCustomSettingsModel {
  function getDiseaseIds(level: DiseaseNotificationLevel): number[] {
    return Object.keys(data.diseaseMatrix)
      .map(k => parseInt(k))
      .filter(k => data.diseaseMatrix[k] === level);
  }
  return {
    userTypeId: data.roleId,
    geonameIds: data.geonames.map(aoi => aoi.geonameId),
    diseaseRelevanceSettings: {
      alwaysNotifyDiseaseIds: getDiseaseIds('always'),
      neverNotifyDiseaseIds: getDiseaseIds('never'),
      riskOnlyDiseaseIds: getDiseaseIds('risky')
    },
    isPresetSelected: !data.isPresetAltered
  };
}
