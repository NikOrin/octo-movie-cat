USE Movies
GO

IF NOT EXISTS (SELECT * FROM sys.schemas AS s WHERE s.name = 'Auth')
BEGIN
	EXEC('CREATE SCHEMA Auth AUTHORIZATION dbo')
END
GO

IF EXISTS (
	SELECT * FROM sys.procedures AS p 
		INNER JOIN sys.schemas AS s ON p.schema_id = s.schema_id
	WHERE p.name = 'User_Authentication_Get'
		AND s.name = 'Auth')
BEGIN
	DROP PROC Auth.User_Authentication_Get
END
GO

CREATE PROC Auth.User_Authentication_Get
	@UserID INT
AS
BEGIN
	SELECT u.UserID 
		, u.Password_e
		, u.Salt
		, u.Username
	FROM dbo.[User] AS u
	WHERE u.UserID = @UserID
END
GO