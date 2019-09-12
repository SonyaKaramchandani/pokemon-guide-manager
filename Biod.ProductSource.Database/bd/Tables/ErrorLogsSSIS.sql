Create table bd.ErrorLogsSSIS(
	Id int identity PRIMARY KEY, 
	ServerName varchar(200), 
	PackageName varchar(200), 
	TaskName varchar(200), 
	ErrorCode int,
	ErrorDescription varchar(max),
	ReportDate Datetime default GETDATE()
);

