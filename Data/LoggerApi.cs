using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class LoggerApi
    {
        public static LoggerApi CreateApi()
        {
            string dateDay = DateTime.Now.ToString("ddMMyyyy");
            string dateTime = DateTime.Now.ToString("HHmmss");
            return new Logger($"BallsInfo_{dateDay}_{dateTime}.log");
        }

        public abstract void CreateLog(string message);
    }
}