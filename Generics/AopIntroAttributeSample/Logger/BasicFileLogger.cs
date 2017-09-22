using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroAttributeSample.Logger
{
    public class BasicFileLogger : ILogger
    {
        public string LogPath { get; set; }
        public string DateTimePrefix { get; set; }

        public void Log(string message)
        {
            string logName = GetLogFileName();

            string path = System.IO.Path.Combine(LogPath, logName);

            if (!Directory.Exists((LogPath)))
                Directory.CreateDirectory(LogPath);

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(message);
                sw.Close();
            }
        }

        public string GetLogFileName()
        {
            return string.Format("{0}.txt", DateTime.Now.ToString(DateTimePrefix));
        }
    }
}

