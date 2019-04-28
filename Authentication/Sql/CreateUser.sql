CREATE LOGIN mainuser WITH PASSWORD = 'user@@123';
GO

exec sp_addsrvrolemember @loginame='mainuser', @rolename='sysadmin'
go