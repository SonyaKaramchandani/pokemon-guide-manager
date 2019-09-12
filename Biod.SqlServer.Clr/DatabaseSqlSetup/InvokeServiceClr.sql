--Important --- Change the DB name and DLL path before execute------
--enable clr
sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO
sp_configure 'clr enabled', 1;
GO
RECONFIGURE;
GO
PRINT ('------End enable clr------')
PRINT ('------start deleting old Service clr if any------')
USE DiseasesAPI
GO
IF EXISTS (SELECT * FROM sys.database_principals WHERE name = N'InvokeServiceClrLogin')
DROP USER [InvokeServiceClrLogin]
GO
GO
IF object_id('InvokeService') IS NOT NULL
DROP PROCEDURE [InvokeService]
GO
GO
IF EXISTS (SELECT * FROM sys.assemblies WHERE [name] = 'InvokeServiceClrAssembly')
DROP ASSEMBLY InvokeServiceClrAssembly;
GO
GO
IF object_id('InvokeService') IS NOT NULL
DROP PROCEDURE [InvokeService]
GO
--------------------------------
USE [master]
GO
If Exists (SELECT loginname FROM master.dbo.syslogins WHERE name = N'InvokeServiceClrLogin' AND dbname = 'master')
DROP LOGIN [InvokeServiceClrLogin]
GO
GO
IF EXISTS (SELECT * FROM sys.asymmetric_keys WHERE [name] = 'ServiceDllKey')
DROP ASYMMETRIC KEY ServiceDllKey
GO
PRINT ('------end deleting old Service clr if any------')
--------------------------------
USE [master]
GO
CREATE ASYMMETRIC KEY ServiceDllKey
FROM EXECUTABLE FILE = 'C:\temp\InvokeServiceClr.dll'
GO
PRINT ('-----ServiceDllKey done------')
GO
CREATE LOGIN InvokeServiceClrLogin FROM ASYMMETRIC KEY ServiceDllKey
GO
PRINT ('-----InvokeServiceClrLogin done------')
GO
GRANT EXTERNAL ACCESS ASSEMBLY TO InvokeServiceClrLogin
--GRANT UNSAFE ASSEMBLY TO InvokeServiceClrLogin
GO
PRINT ('-----GRANT EXTERNAL ACCESS done------')

USE DiseasesAPI
GO
CREATE USER InvokeServiceClrLogin FOR LOGIN InvokeServiceClrLogin
GO
PRINT ('-----CREATE USER InvokeServiceClrLogin done------')
GO
CREATE ASSEMBLY InvokeServiceClrAssembly
FROM 'C:\temp\InvokeServiceClr.dll'
--WITH PERMISSION_SET = UNSAFE
WITH PERMISSION_SET = EXTERNAL_ACCESS
GO
PRINT ('-----InvokeServiceClrAssembly done------')
GO
CREATE PROCEDURE [bd].[InvokeService]
@apiUrl [nvarchar](4000),
@returnval [nvarchar](max) OUTPUT
WITH EXECUTE AS CALLER
AS
EXTERNAL NAME [InvokeServiceClrAssembly].[InvokeServiceClr].[InvokeService]
GO
PRINT ('-----CREATE PROCEDURE InvokeService done------')
GO
CREATE PROCEDURE [bd].[InvokeSpatialService]
@apiUrl [nvarchar](4000),
@returnval [nvarchar](max) OUTPUT
WITH EXECUTE AS CALLER
AS
EXTERNAL NAME [InvokeServiceClrAssembly].[InvokeServiceClr].[InvokeSpatialService]
GO
PRINT ('-----CREATE PROCEDURE InvokeSpatialService done------')
PRINT ('------End of all commands-----')
--Call WS
GO
USE [DiseasesAPI]
Declare @Response NVARCHAR(max)
EXECUTE bd.InvokeService 'http://api-prod1.ad.bluedot.global/api/v1/Diseases/Diseases?pagesize=1&pagenum=10', @Response OUT
SELECT @Response ApiJsonStringResult
GO
