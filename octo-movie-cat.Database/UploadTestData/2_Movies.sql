USE Movies
GO

IF OBJECT_ID('tempdb..#movies') IS NOT NULL
	DROP TABLE #movies
GO

CREATE TABLE #movies
( name VARCHAR(100) NOT NULL)
GO

INSERT INTO #movies VALUES
('5 Centimeters Per Second'), ('13 Hours'), ('Magnificent 7'), ('The Shawshank Redemption'), ('The Godfather'), ('The Godfater: Part II'), ('The Dark Knight'), ('12 Angry Men'),
('Schindler''s List'), ('The Lord of the Rings: The Return of the King'), ('Pulp Fiction'), ('Avengers: Infinity War'), ('The Good, the Bad, and the Ugly'), 
('Fight Club'), ('The Lord of the Rings: The Fellowship of the Ring'), ('Forrest Gump'), ('Star Wars: Episode V - The Empire Strikes Back'), ('Inception'),
('The Lord of the Rings: The Two Towers'), ('One Flew Over the Cuckoo''s Nest'), ('Goodfellas'), ('The Matrix'), ('Seven Samurai'), ('City of God'),
('Star Wars: Episode IV - A New Hope'), ('Se7en'), ('The Silence of the Lambs'), ('It''s a Wonderful Life'), ('Life Is Beautiful'), ('The Usual Suspects'), ('Spirited Away'),
('Saving Private Ryan'), ('Interstellar'), ('The Green Mile'), ('Psycho'), ('City Lights'), ('Casablanca'), ('Modern Times'), ('The Intouchables'), ('The Pianist'), ('The Departed'),
('Terminator 2'), ('Transformers'), ('Back to the Future'), ('Rear Window'), ('Whiplash'), ('Gladiator'), ('The Lion King'), ('The Prestige'), ('Memento'), ('Apocalypse Now'), ('Alien')
GO

TRUNCATE TABLE dbo.Movie
GO

INSERT INTO dbo.Movie
(
	MovieID, 
	Title, 
	PhysicalRentalTierID, 
	SDRentalTierID, 
	HDRentalTierID, 
	ReleaseDate, 
	Description,
	RunTime,
	MpaaRatingID
)
SELECT 
	ROW_NUMBER() OVER (ORDER BY t.name) AS MovieID,
	t.name AS Title,
	5,
	2,
	4,
	DATEADD(YEAR, -2, SYSDATETIME()) AS ReleaseDate,
	'',
	109) AS RunTime,
	3 AS Rating
FROM #movies AS t
GO

DROP TABLE #movies
GO