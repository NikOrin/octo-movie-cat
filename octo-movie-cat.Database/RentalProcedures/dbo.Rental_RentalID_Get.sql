USE Movies
GO

IF EXISTS (SELECT * FROM sys.procedures AS p WHERE p.name = 'Rental_RentalID_Get')
BEGIN
	DROP PROC dbo.Rental_RentalID_Get
END
GO

CREATE PROC dbo.Rental_RentalID_Get
	@InventoryID INT,
	@RentalID INT = NULL OUTPUT
AS
BEGIN
	SET @RentalID = (
		SELECT r.RentalID
		FROM dbo.Rental AS r
		WHERE r.InventoryID = @InventoryID
			AND Returned = 0 )
END
GO