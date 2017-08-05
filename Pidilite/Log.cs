using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nxton
{
    public static class Log
    {
        #region Declaration

        public enum Status { Fatal = 1, Error, Debug, Info }
        private static string MailerName;
        public static string JobName
        {
            get { return MailerName; }
            set { MailerName = value; }
        }

        #endregion

        public static void LogData(string logDataString, Status st)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                log4net.ILog log = log4net.LogManager.GetLogger("HireCraft Notification Error Log");
                switch (st)
                {
                    case Status.Fatal:
                        log.Fatal(System.DateTime.Now + ": " + JobName + ':' + logDataString);
                        break;
                    case Status.Error:
                        log.Error(System.DateTime.Now + ": " + JobName + ':' + logDataString);
                        break;
                    case Status.Debug:
                        log.Debug(System.DateTime.Now + ": " + JobName + ':' + logDataString);
                        break;
                    case Status.Info:
                        log.Info(System.DateTime.Now + ": " + JobName + ':' + logDataString);
                        break;
                    default:
                        log.Info(System.DateTime.Now + ": " + JobName + ":" + logDataString);
                        break;
                }
            }
            catch
            {
                //Debug.Assert(false, ex.Message, ex.StackTrace);
            }
        }

    }
}
