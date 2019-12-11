CREATE TABLE [bd].[ConfigurationVariables](
	[ConfigurationVariableId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Value] [nvarchar](512) NOT NULL,
	[ValueType] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](512) NOT NULL,
	[ApplicationName] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_ConfigurationVariable_ConfigurationVariableId] PRIMARY KEY CLUSTERED 
(
	[ConfigurationVariableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_ConfigurationVariable_Name_ApplicationName] UNIQUE NONCLUSTERED 
(
	[Name] ASC,
	[ApplicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



