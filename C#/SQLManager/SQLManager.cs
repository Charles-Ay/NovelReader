using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using TextLogger;

namespace SQLManager
{
    public class SQLManager
    {
        //current connection
        private SqlConnection cnn;
        //string to connect to db
        private const string connetionString = @"Data Source=localhost\CHARLES_SERVER;Initial Catalog=NovelReader;Integrated Security=True;";
        //all the novels being grabed
        //private static List<Novel.Novel> novels;

        /// <summary>
        /// initlize to sql and get the know novles
        /// </summary>
        public SQLManager()
        {
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            //initNovles();
        }

        ~SQLManager()
        {
            cnn.Close();
        }

        //void initNovles()
        //{
        //    if (novels == null)
        //    {
        //        novels = new List<Novel.Novel>();

        //        Novel.Novel novel = new Novel.Novel("Overgeared", 1601, "https://www.wuxiaworld.com/novel/overgeared/og-chapter-1", "WuxiaWorld");
        //    }
        //}


        public void InsertChaptersWithLinks(Novel.Novel novel)
        {
            SqlCommand command;
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            string sql;

            for (int i = 0; i < novel.totalChapters; ++i)
            {
                string curchapter = novel.initalLink.Remove(novel.initalLink.Length - 1, 1) + (i + 1);
                sql = $"EXEC insertNovel @name = \'{novel.name}\', @chapter = {i + 1}, @link = \'{curchapter}\', @source = \'{novel.source}\'";
                command = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                try
                {
                    dataAdapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Logger.writeToLog($"SQL ERROR - Line:{Logger.GetLineNumber(e)} -- {e.Message} for query: {sql}");
                }
                command.Dispose();
            }
        }

        private void checkIfUpdated()
        {

        }


        public List<Novel.Novel> GetNovelChapters(string name, string source)
        {
            List<Novel.Novel> novels = new List<Novel.Novel>();
            SqlCommand command;
            SqlDataReader reader;
            string sql;

            sql = $"SELECT * FROM Novels WHERE Name = \'{name}\' and Source = \'{source}\' ORDER BY CHAPTER";
            command = new SqlCommand(sql, cnn);

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                int chapter = (int)reader.GetValue(1);
                string link = (string)reader.GetValue(2);
                Novel.Novel novel = new Novel.Novel(name, chapter, link, source);
                novels.Add(novel);
            }
            return novels;
        }
    }
}