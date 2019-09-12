CREATE FULLTEXT INDEX ON [place].[GeonameAlternatenameEng]
    ([AlternatenameEng] LANGUAGE 1033)
    KEY INDEX [idx_GeonameAlternatenameEng_AlternateNameId]
    ON [ZebraGeonamesCatalog];