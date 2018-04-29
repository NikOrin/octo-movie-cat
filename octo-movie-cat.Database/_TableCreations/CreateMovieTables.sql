USE Movies
GO

IF NOT EXISTS (SELECT * FROM sys.tables AS t WHERE t.name = 'RentalTier')
BEGIN
	CREATE TABLE dbo.RentalTier
	(
		RentalTierID TINYINT NOT NULL,
		Price DECIMAL(4,2) NOT NULL,
		CONSTRAINT PK_RentalTier_RentalTierID PRIMARY KEY CLUSTERED (RentalTierID)
	)
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables AS t WHERE t.name = 'Movie')
BEGIN
	CREATE TABLE dbo.Movie
	(
		MovieID INT NOT NULL,
		Title VARCHAR(100) NOT NULL,
		PhysicalRentalTierID TINYINT NULL,
		SDRentalTierID TINYINT NOT NULL,
		HDRentalTierID TINYINT NOT NULL,
		ReleaseDate DATE NULL,
		Description VARCHAR(1000) NULL,
		CONSTRAINT PK_Movie_MovieID PRIMARY KEY CLUSTERED (MovieID)
	)
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables AS t WHERE t.name = 'Inventory')
BEGIN
	CREATE TABLE dbo.Inventory
	(
		InventoryID INT NOT NULL,
		MovieID INT NOT NULL,
		IsAvailable BIT NOT NULL,
		CONSTRAINT PK_Inventory_InventoryID PRIMARY KEY CLUSTERED (InventoryID),
		CONSTRAINT FK_Inventory_Movie FOREIGN KEY (MovieID) REFERENCES dbo.Movie (MovieID)
	)
END
GO

IF NOT EXISTS (
	SELECT * FROM sys.indexes AS i 
	WHERE i.name = 'NC_Inventory_MovieID')
BEGIN
	CREATE NONCLUSTERED INDEX NC_Inventory_MovieID
	ON dbo.Inventory (MovieID)
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables AS t WHERE t.name = 'Genre')
BEGIN
	CREATE TABLE dbo.Genre
	(
		GenreID TINYINT NOT NULL,
		GenreName VARCHAR(50) NOT NULL
		CONSTRAINT PK_Genre_GenreID PRIMARY KEY CLUSTERED (GenreID)
	)
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables AS t WHERE t.name = 'MovieGenre')
BEGIN
	CREATE TABLE dbo.MovieGenre
	(
		MovieID INT NOT NULL,
		GenreID TINYINT NOT NULL,
		CONSTRAINT PK_MovieGenre_MovieID_GenreID PRIMARY KEY CLUSTERED (MovieID, GenreID),
		CONSTRAINT FK_MovieGenre_Movie FOREIGN KEY (MovieID) REFERENCES dbo.Movie (MovieID),
		CONSTRAINT FK_MovieGenre_Genre FOREIGN KEY (GenreID) REFERENCES dbo.Genre (GenreID)
	)
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables AS t WHERE t.name = 'Rental')
BEGIN
	CREATE TABLE dbo.Rental
	(
		RentalID BIGINT NOT NULL IDENTITY(1,1),
		UserID INT NOT NULL,
		MovieID INT NOT NULL,
		InventoryID INT NULL,
		RentalDate DATETIME2(2) NOT NULL,
		RentalDurationHours TINYINT NOT NULL,
		Returned BIT NOT NULL,
		CONSTRAINT PK_Rental_RentalID PRIMARY KEY CLUSTERED (RentalID),
		CONSTRAINT FK_Rental_User FOREIGN KEY (UserID) REFERENCES dbo.[User] (UserID),
		CONSTRAINT FK_Rental_Movie FOREIGN KEY (MovieID) REFERENCES dbo.Movie (MovieID),
		CONSTRAINT FK_Rental_Inventory FOREIGN KEY (InventoryID) REFERENCES dbo.Inventory (InventoryID)
	)
END
GO