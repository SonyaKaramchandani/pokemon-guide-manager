CREATE view place.uvw_CountrySubset 
with SCHEMABINDING
AS
Select GeonameId, [Name] as AsciiName, [Population], DisplayName, CountryName,
	Case featureCode when'PCLI'then 1
		when'PCLS' then 2 
		when'PCLD' then 3
		when'PCL' then 4
		when'TERR' then 5
		when'PCLIX' then 6
		else 7 end as SearchSeq2
From place.Geonames
Where LocationType=6;

GO
CREATE UNIQUE CLUSTERED INDEX [idx_uvw_CountrySubset_GeonameId]
    ON [place].uvw_CountrySubset([GeonameId] ASC);

GO
CREATE NONCLUSTERED INDEX [idx_uvw_CountrySubset_AsciiName]
    ON [place].uvw_CountrySubset(AsciiName ASC);
GO
CREATE NONCLUSTERED INDEX [idx_uvw_CountrySubset_DisplayName]
    ON [place].uvw_CountrySubset(DisplayName ASC);

GO
CREATE NONCLUSTERED INDEX [idx_uvw_CountrySubset_Population]
    ON [place].uvw_CountrySubset([Population] ASC);
GO
CREATE NONCLUSTERED INDEX [idx_uvw_CountrySubset_SearchSeq2]
    ON [place].uvw_CountrySubset(SearchSeq2 ASC);
