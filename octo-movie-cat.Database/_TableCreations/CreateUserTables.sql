﻿USE Movies
GO

IF NOT EXISTS (SELECT * FROM sys.tables AS t WHERE t.name = 'User')
BEGIN
	CREATE TABLE dbo.[User]
	(
		UserID INT NOT NULL IDENTITY(1,1),
		Username VARCHAR(50) NOT NULL,
		Email VARCHAR(250) NOT NULL,
		FirstName VARCHAR(50) NOT NULL,
		LastName VARCHAR(50) NOT NULL,
		Password_e VARCHAR(64) NOT NULL,
		Salt VARCHAR(24) NOT NULL,
		DateOfBirth DATE NOT NULL,
		CreatedOn DATETIME2(2) NOT NULL,
		UpdatedOn DATETIME2(2) NOT NULL,
		CONSTRAINT PK_User_UserID PRIMARY KEY CLUSTERED (UserID),
		CONSTRAINT UC_User_Username UNIQUE NONCLUSTERED (Username)
	)
END
GO