use NovelReader

--IF (SELECT COUNT(*) FROM Sources) <> 0
--	truncate table Sources
INSERT INTO Sources(Name) 
VALUES ('WuxiaWorld'),
	   ('NovelWorld'),
	   ('FreeWebNovel')