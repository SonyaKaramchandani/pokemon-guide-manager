dotnet user-secrets init
dotnet user-secrets set ConnectionStrings.BiodZebraContext ""

dotnet tool restore
dotnet-ef dbcontext scaffold name=BiodZebraContext Microsoft.EntityFrameworkCore.SqlServer -o ./Data/EntityModels -c BiodZebraContext  -t ActiveGeonames -t Agents -t AgentTypes -t ArticleFeed -t AspNetRoles -t AspNetUserRoles -t AspNetUsers -t BiosecurityRisk -t CountryProvinceShapes -t Diseases -t DiseaseSpeciesIncubation -t Event -t EventDestinationAirport -t EventExtension -t EventImportationRisksByGeoname -t EventPriorities -t EventSourceAirport -t GeonameOutbreakPotential -t Geonames -t HamType -t Interventions -t OutbreakPotentialCategory -t ProcessedArticle -t RelevanceState -t RelevanceType -t Species -t Stations -t TransmissionModes -t UserGroup -t Xtbl_Article_Event -t Xtbl_Disease_Agents -t Xtbl_Disease_Interventions -t Xtbl_Disease_TransmissionMode -t Xtbl_Event_Location -t Xtbl_Role_Disease_Relevance -t Xtbl_User_Disease_Relevance -f