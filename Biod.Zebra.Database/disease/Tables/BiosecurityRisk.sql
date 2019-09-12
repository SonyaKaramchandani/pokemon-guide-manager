Create table disease.BiosecurityRisk(
	BiosecurityRiskCode varchar(100) NOT NULL,
	BiosecurityRiskDesc varchar(500) NOT NULL
CONSTRAINT PK_BiosecurityRisk PRIMARY KEY CLUSTERED (BiosecurityRiskCode)
);
