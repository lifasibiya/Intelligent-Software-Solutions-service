USE MASTER
GO


IF (OBJECT_ID('iss') IS NOT NULL)
	DROP DATABASE iss
GO

CREATE DATABASE iss 
ON PRIMARY
(
	NAME = 'iss_data',
	FILENAME = 'C:\SQL\iss.mdf',
	SIZE = 5MB
)
LOG ON
(
	NAME = 'iss_log',
	FILENAME = 'C:\SQL\iss.ldf',
	SIZE = 5MB
)



USE iss
GO


IF (OBJECT_ID('[dbo].[document]') IS NOT NULL)
	DROP TABLE [dbo].[document]
GO

IF (OBJECT_ID('[dbo].[user]') IS NOT NULL)
	DROP TABLE [dbo].[user]
GO



CREATE TABLE [dbo].[user]
(
	[id] int not null identity(1,1) primary key,
	[name] nvarchar(100) not null,
	[surname] nvarchar(100) not null,
	[age] int not null,	
	[dateOfBirth] date not null,
	[idnumber] varchar(13) not null
)
GO


CREATE TABLE [dbo].[document]
(
	[id] int not null identity(1,1) primary key,
	[filename] nvarchar(max) not null,
	[content] varbinary(max) not null,
	[date] date not null,	
	[user] int not null references [dbo].[user]([id])
)




USE iss
GO


IF (OBJECT_ID('[dbo].[sp_GetAllFiles]') IS NOT NULL)
	DROP PROCEDURE [dbo].[sp_GetAllFiles]
GO

CREATE PROCEDURE [dbo].[sp_GetAllFiles]
AS
	SELECT [id],[filename],[content],[date],[user]
	FROM [dbo].[document]
GO



IF (OBJECT_ID('[dbo].[sp_UploadFile]') IS NOT NULL)
	DROP PROCEDURE [dbo].[sp_UploadFile]
GO

CREATE PROCEDURE [dbo].[sp_UploadFile]
	@filename nvarchar(max),
	@content varbinary(max),
	@user int
AS
	INSERT INTO [dbo].[document]([filename],[content],[date],[user])
	VALUES(@filename,@content,GETDATE(),@user)

	SELECT 1;
GO



IF (OBJECT_ID('[dbo].[sp_DeleteFile]') IS NOT NULL)
	DROP PROCEDURE [dbo].[sp_DeleteFile]
GO

CREATE PROCEDURE [dbo].[sp_DeleteFile]
	@id int
AS
	IF EXISTS (SELECT [id] FROM [dbo].[document] WHERE [id] = @id)
	BEGIN
		DELETE FROM [dbo].[document]
		WHERE [id] = @id

		SELECT 1;
	END
	ELSE
		SELECT 0;
GO



IF (OBJECT_ID('[dbo].[sp_CreateUser]') IS NOT NULL)
	DROP PROCEDURE [dbo].[sp_CreateUser]
GO

CREATE PROCEDURE [dbo].[sp_CreateUser]
	@name nvarchar(100),
	@surname nvarchar(100),
	@dateOfBirth date,
	@idnumber varchar(13)
AS
	INSERT INTO [dbo].[user]([name],[surname],[age],[dateOfBirth],[idnumber])
	VALUES(@name,@surname,DATEDIFF(year, @dateOfBirth, GETDATE()) ,@dateOfBirth,@idnumber)

	SELECT 1;
GO



IF (OBJECT_ID('[dbo].[sp_AuthUser]') IS NOT NULL)
	DROP PROCEDURE [dbo].[sp_AuthUser]
GO

CREATE PROCEDURE [dbo].[sp_AuthUser]
	@idnumber varchar(13)
AS
	IF EXISTS 
	(SELECT [id],[name],[surname],[age],[dateOfBirth],[idnumber]
	FROM [dbo].[user]
	WHERE SUBSTRING([idnumber],LEN([idnumber]) - 3, LEN([idnumber])) = @idnumber)
		SELECT [id],[name],[surname],[age],[dateOfBirth],[idnumber]
		FROM [dbo].[user]
		WHERE SUBSTRING([idnumber],LEN([idnumber]) - 3, LEN([idnumber])) = @idnumber
	ELSE
		SELECT 0
GO



IF (OBJECT_ID('[dbo].[fn_GetIdSubstring]') IS NOT NULL)
	DROP PROCEDURE [dbo].[fn_GetIdSubstring]
GO

CREATE FUNCTION [dbo].[fn_GetIdSubstring](@idnumber varchar(13))
	RETURNS varchar(4)
AS
BEGIN
DECLARE @matchingId varchar(4)
RETURN (SELECT [id],[name],[surname],[age],[dateOfBirth],[idnumber]
	FROM [dbo].[user]
	WHERE SUBSTRING([idnumber],LEN([idnumber]) - 3, LEN([idnumber])) = @idnumber)
END
GO



begin
DECLARE @idnum varchar(13)
set @idnum = '1710095687436'
select SUBSTRING(@idnum,LEN(@idnum) - 3, LEN(@idnum))
end









----------------------TESTNG------------------------


exec [dbo].[sp_GetAllFiles]




select * from [dbo].[user]




exec [dbo].[sp_AuthUser] '9899'




