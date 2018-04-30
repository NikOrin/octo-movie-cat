USE Movies
GO

IF EXISTS (SELECT * FROM sys.procedures AS p WHERE p.name = 'Inventory_Checkout')
BEGIN 
	DROP PROC dbo.Inventory_Checkout
END
GO

CREATE PROC dbo.Inventory_Checkout
	@MovieID INT,
	@InventoryID INT = NULL OUTPUT
AS
BEGIN
	SET @InventoryID = ( SELECT TOP 1 
			i.InventoryID
		FROM dbo.Inventory AS i
		WHERE i.MovieID = @MovieID
			AND IsAvailable = 1)

	UPDATE dbo.Inventory
	SET IsAvailable = 0
	WHERE InventoryID = @InventoryID
END
GO