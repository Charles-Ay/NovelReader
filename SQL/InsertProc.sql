CREATE OR ALTER PROCEDURE dbo.insertNovel @name varchar(255), @chapter int, @link varchar(255), @source varchar(255)
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM Novels
				  WHERE chapter = @chapter
				  AND link = @link)
	
	BEGIN
		INSERT INTO Novels(Name, Chapter, Link, Source) 
		VALUES (@name, @chapter, @link, @source)
	END
END