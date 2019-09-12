Create TABLE [surveillance].ArticleGDELTLanguageLookup
([LangName] varchar(200) NOT NULL,
[Iso2] varchar(20) NOT NULL,
[Iso3] varchar(20) NOT NULL
 CONSTRAINT [PK_ArticleGDELTLanguageLookup] PRIMARY KEY CLUSTERED (Iso2)
)

