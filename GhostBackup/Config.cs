using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostBackup
{
    class Config
    {
        public static string Host { get; set; }
        public static string User { get; set; }
        public static string Password { get; set; }
        public static string DbPath { get; set; }

        public static void Init()
        {
            Host = ConfigurationManager.AppSettings["ftp_host"];
            User = ConfigurationManager.AppSettings["ftp_user"];
            Password = ConfigurationManager.AppSettings["ftp_password"];
            DbPath = ConfigurationManager.AppSettings["ftp_dbpath"];
        }
    }
}
