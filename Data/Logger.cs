using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using Nito.AsyncEx;
using System.Timers;

namespace Data
{
    public class Logger : LoggerApi
    {
        private readonly string filename;
        private readonly ConcurrentQueue<string> logsQueue;
        private readonly AsyncAutoResetEvent waitForQueue = new AsyncAutoResetEvent(false);
        private Mutex mutexLogger = new Mutex();
        private System.Timers.Timer timer = new System.Timers.Timer(1000);


        public Logger(string pathWithExtention)
        {
            filename = pathWithExtention;
            logsQueue = new ConcurrentQueue<string>();
            //Task task = RunLogger(CancellationToken.None);
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Elapsed += OnTimedEvent;
            timer.Start();
            
        }

        public override void CreateLog(string message)
        {
            logsQueue.Enqueue(message);
            //waitForQueue.Set();
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            while (!logsQueue.IsEmpty)
            {
                mutexLogger.WaitOne();
                if (logsQueue.TryDequeue(out var logMsg))
                {
                    File.AppendAllText(filename, logMsg + "\n");
                }
                mutexLogger.ReleaseMutex();
            }
        }

        /*private async Task RunLogger(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                timer.Elapsed
                while (!logsQueue.IsEmpty)
                {
                    mutexLogger.WaitOne();
                    if (logsQueue.TryDequeue(out var logMsg))
                    {
                        File.AppendAllText(filename, logMsg + "\n");
                    }
                    mutexLogger.ReleaseMutex();
                }
                
                //await waitForQueue.WaitAsync(cancellationToken);
                
            }
        }*/
    }
}
