use NovelReader

IF EXISTS(SELECT 1 FROM sys.columns 
          WHERE Object_ID = Object_ID(N'dbo.Novels'))
	DROP TABLE Novels
CREATE TABLE Novels(
	Name VARCHAR(255) PRIMARY KEY not null,
	Chapter int not null,
	Link VARCHAR(255) not null,
	Source VARCHAR(255),
	FOREIGN KEY (Source) REFERENCES Sources(Name)
)