﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SQLManager
{
    public class SQLManager
    {
        private SqlConnection cnn;
        private string connetionString = @"Data Source=localhost\CHARLES_SERVER;Initial Catalog=NovelReader;Integrated Security=True;";
        private static List<Novel.Novel> novels;


        public SQLManager()
        {
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            initNovles();
        }

        ~SQLManager()
        {
            cnn.Close();
        }

        void initNovles()
        {
            if (novels == null)
            {
                novels = new List<Novel.Novel>();

                Novel.Novel novel = new Novel.Novel("Overgeared", 1601, "https://www.wuxiaworld.com/novel/overgeared/og-chapter-1", "WuxiaWorld");
            }
        }

        public void InsertChaptersWithLinks()
        {
            SqlCommand command;
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            String sql;

            foreach (Novel.Novel novel in novels)
            {
                for (int i = 0; i < novel.totalChapters; ++i)
                {
                    string curchapter = novel.initalLink.Remove(novel.initalLink.Length - 1, 1) + (i + 1);
                    sql = $"INSERT into Novels ({novel.name}, {i + 1}, {curchapter}, {novel.source})";
                    command = new SqlCommand(sql, cnn);
                    dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                    try
                    {
                        dataAdapter.InsertCommand.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                    }

                    command.Dispose();
                }
            }
            Console.WriteLine("Inserted");
        }
    }
}