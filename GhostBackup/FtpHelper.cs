using System;
using System.IO;
using System.Net;
using GhostBackup;

static internal class FtpHelper
{
    private static FtpWebRequest CreateFtpWebRequest()
    {
        var request = (FtpWebRequest) WebRequest.Create(Config.Host + Config.DbPath);
        request.Method = WebRequestMethods.Ftp.DownloadFile;
        request.Credentials = new NetworkCredential(Config.User, Config.Password);
        return request;
    }

    public static bool DownloadFile(string localPath)
    {
        var request = CreateFtpWebRequest();
            
        try
        {
            using (var reader = request.GetResponse().GetResponseStream())
            {
                var dbPath = PathHelper.GetDbPath(localPath);

                using (var fileStream = new FileStream(dbPath, FileMode.Create))
                {
                    var bytesRead = 0;
                    var buffer = new byte[2048];

                    while (true)
                    {
                        bytesRead = reader.Read(buffer, 0, buffer.Length);

                        if (bytesRead == 0)
                            break;

                        fileStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}