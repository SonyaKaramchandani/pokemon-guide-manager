CREATE VIEW [zebra].[uvw_DiseaseRelevanceByUser]
AS
SELECT        zebra.Xtbl_User_Disease_Relevance.UserId, dbo.AspNetUsers.Email AS UserEmail, disease.Diseases.DiseaseId, disease.Diseases.DiseaseName, zebra.RelevanceType.RelevanceId, 
                         zebra.RelevanceType.Description AS RelevanceDescription, zebra.Xtbl_User_Disease_Relevance.StateId, zebra.RelevanceState.Description AS StateDescription
FROM            dbo.AspNetUsers INNER JOIN
                         zebra.Xtbl_User_Disease_Relevance ON dbo.AspNetUsers.Id = zebra.Xtbl_User_Disease_Relevance.UserId INNER JOIN
                         disease.Diseases ON zebra.Xtbl_User_Disease_Relevance.DiseaseId = disease.Diseases.DiseaseId INNER JOIN
                         zebra.RelevanceType ON zebra.Xtbl_User_Disease_Relevance.RelevanceId = zebra.RelevanceType.RelevanceId INNER JOIN
                         zebra.RelevanceState ON zebra.RelevanceState.StateId = zebra.Xtbl_User_Disease_Relevance.StateId
GO