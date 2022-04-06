use NovelReader

IF EXISTS(SELECT 1 FROM sys.columns 
          WHERE Object_ID = Object_ID(N'dbo.Sources'))
	DROP TABLE Sources
CREATE TABLE Sources(
	Name VARCHAR(255) PRIMARY KEY not null
)
