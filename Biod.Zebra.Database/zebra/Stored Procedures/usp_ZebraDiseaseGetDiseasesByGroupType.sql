
-- =============================================
-- Author:		Anson
-- Create date: 2019-07 
-- Description:	Returns a list of groups and associated diseases
-- 2019-09: disease schema change
-- =============================================
CREATE PROCEDURE [zebra].[usp_ZebraDiseaseGetDiseasesByGroupType]
	@GroupType AS INT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @tbl_diseasegroups table (GroupId int, GroupName varchar(100), DiseaseIds varchar(max));

	--GroupType = 1 - Transmission Mode
	IF @GroupType = 1
		With ST2 as (
			Select f1.DiseaseId, f3.DiseaseName, f2.TransmissionModeId, f2.DisplayName
			From [disease].[Xtbl_Disease_TransmissionMode] as f1, 
				[disease].[TransmissionModes] as f2, [disease].Diseases as f3
			Where f1.SpeciesId=1 and (f1.DiseaseId=f3.DiseaseId)
				AND f1.TransmissionModeId=f2.TransmissionModeId
			)
		Insert into @tbl_diseasegroups(GroupId, GroupName, DiseaseIds)
			Select Distinct TransmissionModeId, DisplayName,
					stuff(
					(	Select ', '+ CAST(ST1.DiseaseId as varchar(10))
						From ST2 as ST1
						Where ST1.TransmissionModeId=ST2.TransmissionModeId
						ORDER BY ST1.DiseaseName
						For XML PATH ('')
					), 1,2,'') as DiseaseIds
					From ST2;
	--GroupType = 0 or others - return row of null
	ELSE

	--solve &amp; issue
	Update @tbl_diseasegroups set DiseaseIds=REPLACE(DiseaseIds, '&amp;', '&');

	SELECT * FROM @tbl_diseasegroups ORDER BY GroupName

END
GO
