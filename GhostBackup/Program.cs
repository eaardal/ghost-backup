using System;
using System.Collections.Generic;
using System.IO;

namespace GhostBackup
{
    class Program
    {
        private static readonly string _sessionId = Guid.NewGuid().ToString().Substring(0, 4);

        static void Main(string[] args)
        {
            try
            {
                Config.Init();

                var localPath = args[0];
                var sessionPath = PathHelper.CreateSessionDir(localPath, _sessionId);

                Console.WriteLine("Starting backup to " + sessionPath);

                var success = FtpHelper.DownloadFile(sessionPath);

                if (success)
                {
                    Console.WriteLine("Downloaded " + Config.DbPath);

                    var dbPath = PathHelper.GetDbPath(sessionPath);
                    var posts = DbHelper.GetPosts(dbPath);

                    WritePostsToDisk(posts, sessionPath);
                }
                else
                {
                    Console.WriteLine("A problem occurred when attempting to download the database");
                }

                Console.WriteLine("All done");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }

        private static void WritePostsToDisk(IEnumerable<Post> posts, string sessionPath)
        {
            foreach (var post in posts)
            {
                WriteMarkdownToDisk(sessionPath, post);
                WriteHtmlToDisk(sessionPath, post);
            }
        }

        private static void WriteHtmlToDisk(string sessionPath, Post post)
        {
            var htmlFile = sessionPath + "\\" + post.Slug + ".html";
            File.WriteAllText(htmlFile, post.Html);

            Console.WriteLine("Created " + htmlFile);
        }

        private static void WriteMarkdownToDisk(string sessionPath, Post post)
        {
            var mdFile = sessionPath + "\\" + post.Slug + ".md";
            File.WriteAllText(mdFile, post.Markdown);

            Console.WriteLine("Created " + mdFile);
        }
    }
}
