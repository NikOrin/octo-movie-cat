USE Movies
GO

IF EXISTS (SELECT * FROM sys.procedures AS p WHERE p.name = 'ReturnMovie')
BEGIN
	DROP PROC dbo.ReturnMovie
END
GO

CREATE PROC dbo.ReturnMovie
	@RentalID INT
AS 
BEGIN
	UPDATE dbo.Rental
	SET Returned = 1
	WHERE RentalID = @RentalID
END
GO