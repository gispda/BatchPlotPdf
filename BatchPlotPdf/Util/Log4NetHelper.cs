using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace BatchPlotPdf.Util
{
    public class Log4NetHelper
    {
        private static string m_logFile;
        private static Dictionary<string, log4net.ILog> m_lstLog = new Dictionary<string, log4net.ILog>();
        public static void InitLog4Net(string strLog4NetConfigFile)
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(strLog4NetConfigFile));
            m_logFile = strLog4NetConfigFile;
            m_lstLog["info_logo"] = log4net.LogManager.GetLogger("info_logo");
            m_lstLog["error_logo"] = log4net.LogManager.GetLogger("error_logo");
        }

        /// <summary>
        /// 功能描述:写入常规日志
        /// </summary>
        /// <param name="strInfoLog">strInfoLog</param>
        public static void WriteInfoLog(string strInfoLog)
        {
            if (m_lstLog["info_logo"].IsInfoEnabled)
            {
                m_lstLog["info_logo"].Info(strInfoLog);
            }
        }

        /// <summary>
        /// 功能描述:写入错误日志
        /// </summary>
        /// <param name="strErrLog">strErrLog</param>
        /// <param name="ex">ex</param>
        public static void WriteErrorLog(string strErrLog, Exception ex = null)
        {
            if (m_lstLog["error_logo"].IsErrorEnabled)
            {
                m_lstLog["error_logo"].Error(strErrLog, ex);
            }
        }

        /// <summary>
        /// 功能描述:写入日志
        /// </summary>
        /// <param name="strType">日志类型（对应log4net配置文件中logger.nama）</param>
        /// <param name="strLog">strLog</param>
        public static void WriteByLogType(string strType, string strLog)
        {
            if (!m_lstLog.ContainsKey(strType))
            {
                //判断是否存在节点
                if (!HasLogNode(strType))
                {
                    WriteErrorLog("log4net配置文件不存在【" + strType + "】配置");
                    return;
                }
                m_lstLog[strType] = log4net.LogManager.GetLogger(strType);
            }
            m_lstLog[strType].Error(strLog);
        }

        /// <summary>
        /// 功能描述:是否存在指定的配置
        /// </summary>
        /// <param name="strNodeName">strNodeName</param>
        /// <returns>返回值</returns>
        private static bool HasLogNode(string strNodeName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(m_logFile);
            var lstNodes = doc.SelectNodes("//configuration/log4net/logger");
            foreach (XmlNode item in lstNodes)
            {
                if (item.Attributes["name"].Value.ToLower() == strNodeName)
                    return true;
            }
            return false;
        }
    }
}
