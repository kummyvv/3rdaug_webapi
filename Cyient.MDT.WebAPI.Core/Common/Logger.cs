using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Cyient.MDT.WebAPI.Core.Common
{
    public class Logger
    {
        public bool WriteErrorLog(string LogMessage)
        {
            bool Status = false;
            string LogSize = ConfigurationManager.AppSettings["LogSize"].ToString();
            long logsz = Convert.ToInt32(LogSize);
            logsz = logsz * 1028;

            DateTime CurrentDateTime = DateTime.Now;
            string CurrentDateTimeString = CurrentDateTime.ToString();
            string LogDirectory = Path.GetTempPath();
            string logLine = BuildLogLine(CurrentDateTime, LogMessage);
            LogDirectory = (LogDirectory + "Log_" + LogFileName(DateTime.Now) + ".txt");
            if (!File.Exists(LogDirectory))
                using (StreamWriter w = File.AppendText(LogDirectory)) ;

            long length = new System.IO.FileInfo(LogDirectory).Length;

            if (length > logsz)
            {
                LogDirectory = "";
                LogDirectory = Path.GetTempPath();
                LogDirectory = (LogDirectory + "Log_" + LogFileName(DateTime.Now, "second") + ".txt");
            }
            lock (typeof(Logger))
            {
                StreamWriter oStreamWriter = null;
                try
                {
                    oStreamWriter = new StreamWriter(LogDirectory, true);
                    oStreamWriter.WriteLine(logLine);
                    Status = true;
                }
                catch
                {

                }
                finally
                {
                    if (oStreamWriter != null)
                    {
                        oStreamWriter.Close();
                    }
                }
            }
            return Status;
        }


        private string BuildLogLine(DateTime CurrentDateTime, string LogMessage)
        {
            StringBuilder loglineStringBuilder = new StringBuilder();
            loglineStringBuilder.Append(LogFileEntryDateTime(CurrentDateTime));
            loglineStringBuilder.Append(" \t");
            loglineStringBuilder.Append(LogMessage);
            return loglineStringBuilder.ToString();
        }


        public string LogFileEntryDateTime(DateTime CurrentDateTime)
        {
            return CurrentDateTime.ToString("dd-MM-yyyy HH:mm:ss");
        }


        private string LogFileName(DateTime CurrentDateTime, string str="first")
        {
            if (str.Equals("first"))
                return CurrentDateTime.ToString("dd_MM_yyyy");

            return CurrentDateTime.ToString("dd_MM_yyyy_HH_mm_ss");
        }
    }
}
