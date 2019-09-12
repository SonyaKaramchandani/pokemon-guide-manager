Create table bd.DiseaseMapUpdateDate(
	MapId int NOT NULL, 
	BlueDotAPI varchar(200) NOT NULL, 
	LastModifiedDate Date NOT NULL,
	CONSTRAINT [PK_DiseaseMapUpdateDate] PRIMARY KEY (MapId, BlueDotAPI)
);

