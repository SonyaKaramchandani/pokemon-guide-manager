//============================ enums ============================

/** SOURCE: `Biod.Insights.Common.Constants.LocationType` */
export enum LocationType {
  Unknown = 0,
  City = 2,
  Province = 4,
  Country = 6
}

/** SOURCE: `Biod.Insights.Common.Constants.OutbreakPotentialCategory` */
export enum OutbreakPotentialCategory {
  Sustained = 1,
  Sporadic = 2,
  NeedsMapSustained = 3,
  NeedsMapUnlikely = 4,
  Unlikely = 5,
  Unknown = 6
}

//============================ classes ============================

/** SOURCE: `Biod.Insights.Service.Models.Disease.AcquisitionModeGroupModel` */
export interface AcquisitionModeGroupModel {
  acquisitionModes?: AcquisitionModeModel[];
  rankId?: number;
  rankName?: string;
}

/** SOURCE: `Biod.Insights.Service.Models.Disease.AcquisitionModeModel` */
export interface AcquisitionModeModel {
  description?: string;
  id?: number;
  label?: string;
  rankId?: number;
}

/** SOURCE: `Biod.Insights.Service.Models.ApplicationMetadataModel` */
export interface ApplicationMetadataModel {
  iataDatasetYear?: number;
  landscanDatasetYear?: number;
}

/** SOURCE: `Biod.Insights.Service.Models.Article.ArticleModel` */
export interface ArticleModel {
  originalLanguage?: string;
  publishedDate?: string;
  sourceName?: string;
  title?: string;
  url?: string;
}

/** SOURCE: `Biod.Insights.Service.Models.Risk.CalculationBreakdownModel` */
export interface CalculationBreakdownModel {
  airports?: EventAirportModel;
  calculationCases?: EventCalculationCasesModel;
  diseaseInformation?: DiseaseInformationModel;
}

/** SOURCE: `Biod.Insights.Service.Models.CaseCountModel` */
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

/** SOURCE: `Biod.Insights.Service.Models.Account.ChangePasswordModel` */
export interface ChangePasswordModel {
  confirmNewPassword?: string;
  currentPassword?: string;
  newPassword?: string;
}

/** SOURCE: `Biod.Insights.Service.Models.Disease.DiseaseInformationModel` */
export interface DiseaseInformationModel {
  acquisitionModeGroups?: AcquisitionModeGroupModel[];
  agents?: string;
  agentTypes?: string;
  biosecurityRisk?: string;
  id?: number;
  incubationPeriod?: string;
  name?: string;
  preventionMeasure?: string;
  symptomaticPeriod?: string;
  transmissionModes?: string;
}

/** SOURCE: `Biod.Insights.Service.Models.DiseaseRelevanceSettingsModel` */
export interface DiseaseRelevanceSettingsModel {
  alwaysNotifyDiseaseIds?: number[];
  neverNotifyDiseaseIds?: number[];
  riskOnlyDiseaseIds?: number[];
}

/** SOURCE: `Biod.Insights.Service.Models.Disease.DiseaseRiskModel` */
export interface DiseaseRiskModel {
  caseCounts?: CaseCountModel;
  diseaseInformation?: DiseaseInformationModel;
  exportationRisk?: RiskModel;
  hasLocalEvents?: boolean;
  importationRisk?: RiskModel;
  lastUpdatedEventDate?: string;
  outbreakPotentialCategory?: OutbreakPotentialCategoryModel;
}

/** SOURCE: `Biod.Insights.Service.Models.Event.EventAirportModel` */
export interface EventAirportModel {
  destinationAirports?: GetAirportModel[];
  sourceAirports?: GetAirportModel[];
  totalDestinationAirports?: number;
  totalDestinationVolume?: number;
  totalSourceAirports?: number;
  totalSourceVolume?: number;
}

/** SOURCE: `Biod.Insights.Service.Models.Event.EventCalculationCasesModel` */
export interface EventCalculationCasesModel {
  casesIncluded?: number;
  maxCasesIncluded?: number;
  minCasesIncluded?: number;
}

/** SOURCE: `Biod.Insights.Service.Models.Event.EventInformationModel` */
export interface EventInformationModel {
  diseaseId?: number;
  endDate?: string;
  id?: number;
  lastUpdatedDate?: string;
  startDate?: string;
  summary?: string;
  title?: string;
}

/** SOURCE: `Biod.Insights.Service.Models.Event.EventLocationModel` */
export interface EventLocationModel {
  caseCounts?: CaseCountModel;
  countryName?: string;
  eventDate?: string;
  geonameId?: number;
  locationName?: string;
  locationType?: number;
  previousEventDate?: string;
  provinceName?: string;
}

/** SOURCE: `Biod.Insights.Service.Models.Map.EventsPinModel` */
export interface EventsPinModel {
  events?: EventInformationModel[];
  geonameId?: number;
  locationName?: string;
  point?: string;
}

/** SOURCE: `Biod.Insights.Service.Models.Airport.GetAirportModel` */
export interface GetAirportModel {
  cases?: EventCalculationCasesModel;
  city?: string;
  code?: string;
  exportationRisk?: RiskModel;
  id?: number;
  importationRisk?: RiskModel;
  latitude?: number;
  longitude?: number;
  maxPrevalence?: number;
  minPrevalence?: number;
  name?: string;
  population?: number;
  volume?: number;
}

/** SOURCE: `Biod.Insights.Service.Models.Event.GetEventListModel` */
export interface GetEventListModel {
  countryPins?: EventsPinModel[];
  diseaseInformation?: DiseaseInformationModel;
  eventsList?: GetEventModel[];
  exportationRisk?: RiskModel;
  importationRisk?: RiskModel;
  localCaseCounts?: CaseCountModel;
  outbreakPotentialCategory?: OutbreakPotentialCategoryModel;
}

/** SOURCE: `Biod.Insights.Service.Models.Event.GetEventModel` */
export interface GetEventModel {
  airports?: EventAirportModel;
  articles?: ArticleModel[];
  calculationMetadata?: EventCalculationCasesModel;
  caseCounts?: CaseCountModel;
  diseaseInformation?: DiseaseInformationModel;
  eventInformation?: EventInformationModel;
  eventLocations?: EventLocationModel[];
  exportationRisk?: RiskModel;
  importationRisk?: RiskModel;
  isLocal?: boolean;
  localCaseCounts?: CaseCountModel;
  outbreakPotentialCategory?: OutbreakPotentialCategoryModel;
  previousActivityDate?: string;
  updatedLocations?: EventLocationModel[];
}

/** SOURCE: `Biod.Insights.Service.Models.Geoname.GetGeonameModel` */
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

/** SOURCE: `Biod.Insights.Service.Models.Geoname.GetGeonameShapesModel` */
export interface GetGeonameShapesModel {
  geonameIds?: number[];
}

/** SOURCE: `Biod.Insights.Service.Models.User.GetUserLocationModel` */
export interface GetUserLocationModel {
  geonames?: GetGeonameModel[];
}

/** SOURCE: `Biod.Insights.Service.Models.Disease.OutbreakPotentialCategoryModel` */
export interface OutbreakPotentialCategoryModel {
  attributeId?: number;
  diseaseId?: number;
  id?: number;
  name?: string;
}

/** SOURCE: `Biod.Insights.Service.Models.User.PostUserLocationModel` */
export interface PostUserLocationModel {
  geonameId?: number;
}

/** SOURCE: `Biod.Insights.Service.Models.Disease.RiskAggregationModel` */
export interface RiskAggregationModel {
  countryPins?: EventsPinModel[];
  diseaseRisks?: DiseaseRiskModel[];
}

/** SOURCE: `Biod.Insights.Service.Models.RiskModel` */
export interface RiskModel {
  isModelNotRun?: boolean;
  maxMagnitude?: number;
  maxProbability?: number;
  minMagnitude?: number;
  minProbability?: number;
}

/** SOURCE: `Biod.Insights.Service.Models.Geoname.SearchGeonameModel` */
export interface SearchGeonameModel {
  geonameId?: number;
  latitude?: number;
  locationType?: LocationType;
  longitude?: number;
  name?: string;
}

/** SOURCE: `Biod.Insights.Service.Models.User.UserCustomSettingsModel` */
export interface UserCustomSettingsModel {
  diseaseRelevanceSettings?: DiseaseRelevanceSettingsModel;
  geonameIds?: number[];
  roleId?: string;
}

/** SOURCE: `Biod.Insights.Service.Models.User.UserModel` */
export interface UserModel {
  aoiGeonameIds?: string;
  diseaseRelevanceSetting?: DiseaseRelevanceSettingsModel;
  groupId?: number;
  id?: string;
  isDoNotTrack?: boolean;
  isEmailConfirmed?: boolean;
  location?: GetGeonameModel;
  notificationsSetting?: UserNotificationsModel;
  personalDetails?: UserPersonalDetailsModel;
  roles?: UserRoleModel[];
}

/** SOURCE: `Biod.Insights.Service.Models.User.UserNotificationsModel` */
export interface UserNotificationsModel {
  isEventEmailEnabled?: boolean;
  isProximalEmailEnabled?: boolean;
  isWeeklyEmailEnabled?: boolean;
}

/** SOURCE: `Biod.Insights.Service.Models.User.UserPersonalDetailsModel` */
export interface UserPersonalDetailsModel {
  email?: string;
  firstName?: string;
  lastName?: string;
  locationGeonameId?: number;
  organization?: string;
  phoneNumber?: string;
  roleId?: string;
}

/** SOURCE: `Biod.Insights.Service.Models.User.UserRoleModel` */
export interface UserRoleModel {
  id?: string;
  isPublic?: boolean;
  name?: string;
}
