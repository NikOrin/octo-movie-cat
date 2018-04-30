USE Movies
GO

IF EXISTS (SELECT  * FROM sys.procedures AS p WHERE p.name = 'RentMovie')
BEGIN
	DROP PROC dbo.RentMovie
END
GO

CREATE PROC dbo.RentMovie
	@RentalID INT = NULL OUTPUT,
	@UserID INT,
	@MovieID INT,
	@InventoryID INT = NULL,
	@RentalDurationHours TINYINT
AS
BEGIN
	INSERT INTO dbo.Rental
	(
		UserID,
		MovieID,
		InventoryID,
		RentalDate,
		RentalDurationHours,
		Returned
	)
	VALUES
	(
		@UserID,
		@MovieID,
		@InventoryID,
		SYSDATETIME(),
		@RentalDurationHours,
		0
	)

	SET @RentalID = SCOPE_IDENTITY()
END
GO