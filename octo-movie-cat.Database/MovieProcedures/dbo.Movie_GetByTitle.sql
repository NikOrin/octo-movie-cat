USE Movies
GO

IF EXISTS (SELECT * FROM sys.procedures AS p WHERE p.name = 'Movie_GetByTitle')
BEGIN
	DROP PROCEDURE dbo.Movie_GetByTitle
END
GO

CREATE PROC dbo.Movie_GetByTitle
	@Title VARCHAR(100)
AS
BEGIN
	;WITH inStock AS
	(
		SELECT m.MovieID,
			COUNT(*) AS InventoryCount
		FROM dbo.Inventory AS i
			INNER JOIN dbo.Movie AS m ON i.MovieID = m.MovieID
		GROUP BY m.MovieID
	)
	SELECT m.MovieID,
		m.Title,
		m.PhysicalRentalTierID,
		m.SDRentalTierID,
		m.HDRentalTierID,
		m.ReleaseDate,
		m.Description,
		InventoryCount 
	FROM dbo.Movie AS m
		LEFT JOIN inStock AS i ON m.MovieID = i.MovieID
	WHERE m.Title LIKE @Title
END
GO