CREATE VIEW [zebra].[uvw_DiseaseRelevanceByRole]
AS
SELECT        zebra.Xtbl_Role_Disease_Relevance.RoleId, dbo.AspNetRoles.Name AS RoleName, disease.Diseases.DiseaseId, disease.Diseases.DiseaseName, zebra.RelevanceType.RelevanceId, 
                         zebra.RelevanceType.Description AS RelevanceDescription, zebra.Xtbl_Role_Disease_Relevance.StateId, zebra.RelevanceState.Description AS StateDescription
FROM            disease.Diseases INNER JOIN
                         zebra.Xtbl_Role_Disease_Relevance ON disease.Diseases.DiseaseId = zebra.Xtbl_Role_Disease_Relevance.DiseaseId INNER JOIN
                         zebra.RelevanceType ON zebra.Xtbl_Role_Disease_Relevance.RelevanceId = zebra.RelevanceType.RelevanceId INNER JOIN
                         dbo.AspNetRoles ON dbo.AspNetRoles.Id = zebra.Xtbl_Role_Disease_Relevance.RoleId INNER JOIN
                         zebra.RelevanceState ON zebra.RelevanceState.StateId = zebra.Xtbl_Role_Disease_Relevance.StateId
GO