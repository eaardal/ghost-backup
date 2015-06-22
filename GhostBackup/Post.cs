namespace GhostBackup
{
    internal class Post
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Markdown { get; set; }
        public string Html { get; set; }
        public string Slug { get; set; }
    }
}