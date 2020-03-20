//============================ enums ============================

/** SOURCE: `Biod.Insights.Api.Constants.LocationType` */
export enum LocationType {
  Unknown = 0,
  City = 2,
  Province = 4,
  Country = 6
}

/** SOURCE: `Biod.Insights.Api.Constants.OutbreakPotentialCategory` */
export enum OutbreakPotentialCategory {
  Sustained = 1,
  Sporadic = 2,
  NeedsMapSustained = 3,
  NeedsMapUnlikely = 4,
  Unlikely = 5,
  Unknown = 6
}

//============================ classes ============================

/** SOURCE: `Biod.Insights.Api.Models.Disease.AcquisitionModeGroupModel` */
export interface AcquisitionModeGroupModel {
  acquisitionModes?: AcquisitionModeModel[];
  rankId?: number;
  rankName?: string;
}

/** SOURCE: `Biod.Insights.Api.Models.Disease.AcquisitionModeModel` */
export interface AcquisitionModeModel {
  description?: string;
  id?: number;
  label?: string;
  rankId?: number;
}

/** SOURCE: `Biod.Insights.Api.Models.Article.ArticleModel` */
export interface ArticleModel {
  originalLanguage?: string;
  publishedDate?: Date;
  sourceName?: string;
  title?: string;
  url?: string;
}

/** SOURCE: `Biod.Insights.Api.Models.CaseCountModel` */
export interface CaseCountModel {
  confirmedCases?: number;
  deaths?: number;
  hasConfirmedCasesNesting?: boolean;
  hasDeathsNesting?: boolean;
  hasReportedCasesNesting?: boolean;
  hasSuspectedCasesNesting?: boolean;
  reportedCases?: number;
  suspectedCases?: number;
}

/** SOURCE: `Biod.Insights.Api.Models.Account.ChangePasswordModel` */
export interface ChangePasswordModel {
  confirmNewPassword?: string;
  currentPassword?: string;
  newPassword?: string;
}

/** SOURCE: `Biod.Insights.Api.Models.Disease.DiseaseInformationModel` */
export interface DiseaseInformationModel {
  acquisitionModeGroups?: AcquisitionModeGroupModel[];
  agents?: string;
  agentTypes?: string;
  biosecurityRisk?: string;
  id?: number;
  incubationPeriod?: string;
  name?: string;
  preventionMeasure?: string;
  transmissionModes?: string;
}

/** SOURCE: `Biod.Insights.Api.Models.DiseaseRelevanceSettingsModel` */
export interface DiseaseRelevanceSettingsModel {
  alwaysNotifyDiseaseIds?: number[];
  neverNotifyDiseaseIds?: number[];
  riskOnlyDiseaseIds?: number[];
}

/** SOURCE: `Biod.Insights.Api.Models.Disease.DiseaseRiskModel` */
export interface DiseaseRiskModel {
  caseCounts?: CaseCountModel;
  diseaseInformation?: DiseaseInformationModel;
  exportationRisk?: RiskModel;
  hasLocalEvents?: boolean;
  importationRisk?: RiskModel;
  lastUpdatedEventDate?: Date;
  outbreakPotentialCategory?: OutbreakPotentialCategoryModel;
}

/** SOURCE: `Biod.Insights.Api.Models.Event.EventAirportModel` */
export interface EventAirportModel {
  destinationAirports?: GetAirportModel[];
  sourceAirports?: GetAirportModel[];
}

/** SOURCE: `Biod.Insights.Api.Models.Event.EventInformationModel` */
export interface EventInformationModel {
  diseaseId?: number;
  endDate?: Date;
  id?: number;
  lastUpdatedDate?: Date;
  startDate?: Date;
  summary?: string;
  title?: string;
}

/** SOURCE: `Biod.Insights.Api.Models.Event.EventLocationModel` */
export interface EventLocationModel {
  caseCounts?: CaseCountModel;
  countryName?: string;
  geonameId?: number;
  locationName?: string;
  locationType?: number;
  provinceName?: string;
}

/** SOURCE: `Biod.Insights.Api.Models.Map.EventsPinModel` */
export interface EventsPinModel {
  events?: EventInformationModel[];
  geonameId?: number;
  locationName?: string;
  point?: string;
}

/** SOURCE: `Biod.Insights.Api.Models.Airport.GetAirportModel` */
export interface GetAirportModel {
  city?: string;
  code?: string;
  id?: number;
  importationRisk?: RiskModel;
  latitude?: number;
  longitude?: number;
  name?: string;
  volume?: number;
}

/** SOURCE: `Biod.Insights.Api.Models.Event.GetEventListModel` */
export interface GetEventListModel {
  countryPins?: EventsPinModel[];
  diseaseInformation?: DiseaseInformationModel;
  eventsList?: GetEventModel[];
  exportationRisk?: RiskModel;
  importationRisk?: RiskModel;
  localCaseCounts?: CaseCountModel;
  outbreakPotentialCategory?: OutbreakPotentialCategoryModel;
}

/** SOURCE: `Biod.Insights.Api.Models.Event.GetEventModel` */
export interface GetEventModel {
  articles?: ArticleModel[];
  caseCounts?: CaseCountModel;
  destinationAirports?: GetAirportModel[];
  diseaseInformation?: DiseaseInformationModel;
  eventInformation?: EventInformationModel;
  eventLocations?: EventLocationModel[];
  exportationRisk?: RiskModel;
  importationRisk?: RiskModel;
  isLocal?: boolean;
  localCaseCounts?: CaseCountModel;
  outbreakPotentialCategory?: OutbreakPotentialCategoryModel;
  sourceAirports?: GetAirportModel[];
}

/** SOURCE: `Biod.Insights.Api.Models.Geoname.GetGeonameModel` */
export interface GetGeonameModel {
  country?: string;
  fullDisplayName?: string;
  geonameId?: number;
  latitude?: number;
  locationType?: number;
  longitude?: number;
  name?: string;
  province?: string;
  shape?: string;
}

/** SOURCE: `Biod.Insights.Api.Models.Geoname.GetGeonameShapesModel` */
export interface GetGeonameShapesModel {
  geonameIds?: number[];
}

/** SOURCE: `Biod.Insights.Api.Models.User.GetUserLocationModel` */
export interface GetUserLocationModel {
  geonames?: GetGeonameModel[];
}

/** SOURCE: `Biod.Insights.Api.Models.Disease.OutbreakPotentialCategoryModel` */
export interface OutbreakPotentialCategoryModel {
  attributeId?: number;
  diseaseId?: number;
  id?: number;
  name?: string;
}

/** SOURCE: `Biod.Insights.Api.Models.User.PostUserLocationModel` */
export interface PostUserLocationModel {
  geonameId?: number;
}

/** SOURCE: `Biod.Insights.Api.Models.Disease.RiskAggregationModel` */
export interface RiskAggregationModel {
  countryPins?: EventsPinModel[];
  diseaseRisks?: DiseaseRiskModel[];
}

/** SOURCE: `Biod.Insights.Api.Models.RiskModel` */
export interface RiskModel {
  isModelNotRun?: boolean;
  maxMagnitude?: number;
  maxProbability?: number;
  minMagnitude?: number;
  minProbability?: number;
}

/** SOURCE: `Biod.Insights.Api.Models.Geoname.SearchGeonameModel` */
export interface SearchGeonameModel {
  geonameId?: number;
  latitude?: number;
  locationType?: LocationType;
  longitude?: number;
  name?: string;
}

/** SOURCE: `Biod.Insights.Api.Models.User.UserCustomSettingsModel` */
export interface UserCustomSettingsModel {
  diseaseRelevanceSettings?: DiseaseRelevanceSettingsModel;
  geonameIds?: number[];
  roleId?: string;
}

/** SOURCE: `Biod.Insights.Api.Models.User.UserModel` */
export interface UserModel {
  diseaseRelevanceSetting?: DiseaseRelevanceSettingsModel;
  groupId?: number;
  id?: string;
  isDoNotTrack?: boolean;
  location?: GetGeonameModel;
  notificationsSetting?: UserNotificationsModel;
  personalDetails?: UserPersonalDetailsModel;
  roles?: UserRoleModel[];
}

/** SOURCE: `Biod.Insights.Api.Models.User.UserNotificationsModel` */
export interface UserNotificationsModel {
  isEventEmailEnabled?: boolean;
  isProximalEmailEnabled?: boolean;
  isWeeklyEmailEnabled?: boolean;
}

/** SOURCE: `Biod.Insights.Api.Models.User.UserPersonalDetailsModel` */
export interface UserPersonalDetailsModel {
  email?: string;
  firstName?: string;
  lastName?: string;
  locationGeonameId?: number;
  organization?: string;
  phoneNumber?: string;
  roleId?: string;
}

/** SOURCE: `Biod.Insights.Api.Models.User.UserRoleModel` */
export interface UserRoleModel {
  id?: string;
  isPublic?: boolean;
  name?: string;
}
