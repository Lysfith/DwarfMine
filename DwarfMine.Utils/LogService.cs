using DwarfMine.Interfaces.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Utils
{
    public class LogService : ILogService
    {
        public void Debug(string message)
        {
            Write("DEBUG", message);
        }

        public void Info(string message)
        {
            Write("INFO", message);
        }

        public void Warn(string message)
        {
            Write("WARN", message);
        }

        public void Error(string message, Exception ex = null)
        {
            Write("ERROR", $"{message} - {ex.StackTrace}");
        }

        private void Write(string type, string message)
        {
#if DEBUG
            Console.WriteLine(string.Format("[{0}][{1}] {2}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), type, message));
#endif
        }
    }
}
