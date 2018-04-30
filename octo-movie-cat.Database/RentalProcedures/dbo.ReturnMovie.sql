USE Movies
GO

IF EXISTS (SELECT * FROM sys.procedures AS p WHERE p.name = 'ReturnMovie')
BEGIN
	DROP PROC dbo.ReturnMovie
END
GO

CREATE PROC dbo.ReturnMovie
	@RentalID INT,
	@IsPhysicalReturn BIT
AS 
BEGIN
	UPDATE dbo.Rental
	SET Returned = 1
	WHERE RentalID = @RentalID
		AND ((@IsPhysicalReturn = 1 AND InventoryID IS NOT NULL) 
			OR (@IsPhysicalReturn = 0 AND InventoryID IS NULL))

	IF @IsPhysicalReturn = 1
	BEGIN
		DECLARE @InventoryID INT = (
			SELECT r.InventoryID
			FROM dbo.Rental AS r
			WHERE r.RentalID = @RentalID )

		UPDATE dbo.Inventory
		SET IsAvailable = 1
		WHERE InventoryID = @InventoryID
	END
	RETURN @@ROWCOUNT
END
GO