USE Movies
GO

IF EXISTS (SELECT * FROM sys.procedures AS p WHERE p.name = 'User_Get')
BEGIN
	DROP PROC dbo.User_Get
END
GO

CREATE PROC dbo.User_Get
	@UserID INT
AS
BEGIN
	SELECT u.UserID
		, u.Email
		, u.Username
		, u.FirstName
		, u.LastName
		, u.DateOfBirth
		, u.CreatedOn
	FROM dbo.[User] AS u
	WHERE u.UserID = @UserID
END
GO
