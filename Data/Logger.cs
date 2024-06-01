using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Data
{
    public class Logger : LoggerApi
    {
        private readonly string filename;
        private readonly ConcurrentQueue<string> logsQueue;
        private Task task;
        private readonly AsyncAutoResetEvent waitForQueue = new AsyncAutoResetEvent(false);
        private Mutex mutexLogger = new Mutex();


        public Logger(string pathWithExtention)
        {
            filename = pathWithExtention;
            logsQueue = new ConcurrentQueue<string>();
            task = RunLogger(CancellationToken.None);
        }

        public override void CreateLog(string message)
        {
            logsQueue.Enqueue(message);
            waitForQueue.Set();
        }

        private async Task RunLogger(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (!cancellationToken.IsCancellationRequested)
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
                await waitForQueue.WaitAsync(cancellationToken);
            }
        }
    }
}
