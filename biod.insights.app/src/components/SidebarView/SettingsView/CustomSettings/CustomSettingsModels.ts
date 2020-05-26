import { NumericDictionary } from 'lodash';

export type DiseaseNotificationLevel = 'always' | 'risky' | 'never';
export type DiseaseNotificationLevelDict = NumericDictionary<DiseaseNotificationLevel>;

export type CustomSettingsFM = {
  roleId: string;
  geonames: CustomSettingsGeoname[];
  diseasesPerRole?: { [id: string]: DiseaseNotificationLevelDict };
};

export type CustomSettingsSubmitData = {
  roleId: string;
  geonames: CustomSettingsGeoname[];
  diseaseMatrix: DiseaseNotificationLevelDict;
  isPresetAltered: boolean;
};

export type CustomSettingsGeoname = {
  geonameId: number;
  name: string;
};

export type DiseaseGroupingVM = {
  name: string;
  subgroups?: DiseaseGroupingVM[];
  diseases?: DiseaserRowVM[];
  diseaseIdsFlattened: number[];
};
export type DiseaserRowVM = {
  id: number;
  name: string;
};

export type RoleAndItsPresets = {
  id: string;
  name: string;
  notificationDescription: string;
  preset: DiseaseNotificationLevelDict;
};
