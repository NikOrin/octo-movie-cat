USE Movies
GO

TRUNCATE TABLE dbo.RentalTier
GO

INSERT INTO dbo.RentalTier (RentalTierID, Price) VALUES
(1,	1.50),( 2, 2.50), (3, 3.50), (4, 4.50), (5, 5.50)
GO
