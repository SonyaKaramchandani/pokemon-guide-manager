-- ==========================================================================================
-- Epic:            PROXPAR
-- JIRA:            PT-1379 (https://bluedotglobal.atlassian.net/browse/PT-1379)
-- Description:     This SP is a wrapper around the function due to performance issues
--                  that occurs when calling the function from EF.
-- ==========================================================================================

create procedure zebra.usp_GetProximalEventLocations(@geonameId int,
                                                     @diseaseId int = null,
                                                     @eventId int = null)
as
begin
    set nocount on

    select * from zebra.ufn_GetProximalEventLocations(@geonameId, @diseaseId, @eventId)
end