CREATE view place.uvw_CitySubset 
with SCHEMABINDING
AS
Select GeonameId, [Name] as AsciiName, DisplayName, [Population], 
	Case featureCode when'PPLC'then 1
		when'PPLA' then 2 
		when'PPLA2' then 3
		when'ADM2' then 4
		when'PPLA3' then 5
		when'ADM3' then 6
		when'PPLA4' then 7
		when'ADM4' then 8
		when'ADM5' then 9
		when'PPL' then 10
		else 11 end as SearchSeq2
From place.Geonames
Where LocationType=2;

GO
CREATE UNIQUE CLUSTERED INDEX [idx_uvw_CitySubset_GeonameId]
    ON [place].uvw_CitySubset([GeonameId] ASC);

GO
CREATE NONCLUSTERED INDEX [idx_uvw_CitySubset_AsciiName]
    ON [place].uvw_CitySubset(AsciiName ASC);

GO

CREATE NONCLUSTERED INDEX [idx_uvw_CitySubset_Population]
    ON [place].uvw_CitySubset([Population] ASC);
GO
CREATE NONCLUSTERED INDEX [idx_uvw_CitySubset_SearchSeq2]
    ON [place].uvw_CitySubset(SearchSeq2 ASC);
