using System.IO;

namespace GhostBackup
{
    internal class PathHelper
    {
        public static string CreateSessionDir(string localPath, string sessionId)
        {
            var dir = Directory.CreateDirectory(localPath + "\\" + sessionId);
            return dir.FullName;    
        }

        public static string GetDbPath(string sessionPath)
        {
            return sessionPath + "\\ghost.db";
        }
    }
}