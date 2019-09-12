Create TABLE [surveillance].[Xtbl_SuggestedEvent_Reason](
	SuggestedEventId varchar(30) NOT NULL,
	Reason varchar(200) NOT NULL,
	CONSTRAINT [PK_Xtbl_SuggestedEvent_Reason] PRIMARY KEY CLUSTERED (SuggestedEventId, Reason),
	CONSTRAINT FK_Xtbl_SuggestedEvent_Reason_SuggestedEventId FOREIGN KEY (SuggestedEventId) 
		REFERENCES [surveillance].[SuggestedEvent] (SuggestedEventId) ON DELETE CASCADE
);

