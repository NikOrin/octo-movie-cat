USE Movies
GO

TRUNCATE TABLE dbo.Inventory
GO

DECLARE movie_cursor 
CURSOR FAST_FORWARD READ_ONLY FOR
	SELECT MovieID FROM dbo.Movie

OPEN movie_cursor

DECLARE @MovieID INT

FETCH NEXT FROM movie_cursor
INTO @MovieID

DECLARE @InventoryID INT = 1

WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO dbo.Inventory
	( InventoryID, MovieID, IsAvailable )
	VALUES
	(
		@InventoryID,
		@MovieID,
		1
	)

	SET @InventoryID = @InventoryID + 1;

	IF @InventoryID % 4 = 0
	BEGIN
		FETCH NEXT FROM movie_cursor
		INTO @MovieID
	END
END

CLOSE movie_cursor
DEALLOCATE movie_cursor
GO