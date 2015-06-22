using System;
using System.Collections.Generic;
using System.Data.SQLite;
using GhostBackup;

static internal class DbHelper
{
    public static IEnumerable<Post> GetPosts(string dbPath)
    {
        var connectionString = string.Format("Data Source={0}", dbPath);

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            const string query = "SELECT id, title, markdown, html, slug FROM posts";

            using (var command = new SQLiteCommand(query, connection))
            {
                var posts = new List<Post>();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var id = (long) reader["id"];
                    var title = (string) reader["title"];
                    var markdown = (string) reader["markdown"];
                    var html = (string) reader["html"];
                    var slug = (string)reader["slug"];

                    posts.Add(new Post
                    {
                        Id = id,
                        Title = title,
                        Html = html,
                        Markdown = markdown,
                        Slug = slug
                    });
                }

                connection.Close();

                return posts;
            }
        }
    }
}