using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using log4net;

namespace CommonUtls
{
    public class LogHelper
    {
        private LogHelper() { }
        static LogHelper()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo("Log4Net.config"));

            ILog logs = LogManager.GetLogger(typeof(LogHelper));

            logs.Fatal("Excption:这里就是要提示的LOG信息");
        }


        public static ILog GetLog(Type type)
        {
            var log = LogManager.GetLogger(type);
            return log;
        }

        public static void WriteLog(string logMessage)
        {
            var log = LogManager.GetLogger("sdf");
            log.Info(logMessage);
        }
    }
}
