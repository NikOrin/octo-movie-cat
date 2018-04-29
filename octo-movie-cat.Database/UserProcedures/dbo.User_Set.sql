USE Movies
GO

IF EXISTS (SELECT * FROM sys.procedures AS p WHERE p.name = 'User_Set')
BEGIN
	DROP PROC dbo.User_Set
END
GO

CREATE PROC dbo.User_Set
	@UserID INT = NULL OUTPUT,
	@Username VARCHAR(50),
	@FirstName VARCHAR(50),
	@LastName VARCHAR(50),
	@Password_e VARCHAR(64),
	@Salt VARCHAR(24),
	@DateOfBirth DATE
AS
BEGIN
	IF @UserID IS NULL
	BEGIN
		INSERT INTO dbo.[User]
		(
			Username,
			FirstName,
			LastName,
			Password_e,
			Salt,
			DateOfBirth,
			CreatedOn,
			UpdatedOn
		)
		VALUES
		(
			@Username,
			@FirstName,
			@LastName,
			@Password_e,
			@Salt,
			@DateOfBirth,
			SYSDATETIME(),
			SYSDATETIME()
		)

		SET @UserID = SCOPE_IDENTITY()
	END
	ELSE 
	BEGIN
		UPDATE dbo.[User]
		SET FirstName = @FirstName,
			LastName = @LastName,
			UpdatedOn = SYSDATETIME()
		WHERE UserID = @UserID
	END
END
GO