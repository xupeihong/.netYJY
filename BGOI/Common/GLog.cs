using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace TECOCITY_BGOI
{
    public class GLog
    {
        public GLog()
        {
            SetConfig();
        }
        //private static readonly ILog log = LogManager.GetLogger(typeof(GLog));
        private static readonly ILog log = LogManager.GetLogger("loginfo");
        private static bool IsLoadConfig = false;
        private static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        private static void IsLoadCon()
        {
            if (!IsLoadConfig)
            {
                SetConfig();
                IsLoadConfig = true;
            }
        }
        public static void LogError(Object message, Exception exception)
        {
            IsLoadCon();
            if (log.IsInfoEnabled)
            {
                log.Error(message, exception);
            }
        }

        public static void LogError(Object message)
        {
            IsLoadCon();
            if (log.IsInfoEnabled)
            {
                log.Error(message);
            }
        }

        public static void LogFatal(Object message, Exception exception)
        {
            IsLoadCon();
            if (log.IsInfoEnabled)
            {
                log.Fatal(message, exception);
            }
        }

        public static void LogFatal(Object message)
        {
            IsLoadCon();
            if (log.IsInfoEnabled)
            {
                log.Fatal(message);
            }
        }

        public static void LogInfo(Object message, Exception exception)
        {
            IsLoadCon();
            if (log.IsInfoEnabled)
            {
                log.Info(message, exception);
            }
        }

        public static void LogInfo(Object message)
        {
            IsLoadCon();
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
        }

        public static void LogDebug(Object message, Exception exception)
        {
            IsLoadCon();
            if (log.IsInfoEnabled)
            {
                log.Debug(message, exception);
            }
        }

        public static void LogDebug(Object message)
        {
            IsLoadCon();
            if (log.IsInfoEnabled)
            {
                log.Debug(message);
            }
        }

        public static void LogWarn(Object message, Exception exception)
        {
            IsLoadCon();
            if (log.IsInfoEnabled)
            {
                log.Warn(message, exception);
            }
        }

        public static void LogWarn(Object message)
        {
            IsLoadCon();
            if (log.IsInfoEnabled)
            {
                log.Warn(message);
            }

        }
    }
}
