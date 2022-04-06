use NovelReader

IF EXISTS(SELECT 1 FROM sys.columns 
          WHERE Object_ID = Object_ID(N'dbo.Novels'))
	DROP TABLE Novels