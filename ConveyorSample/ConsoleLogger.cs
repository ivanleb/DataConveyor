using DataConveyor;
using System;

namespace ConveyorSample
{
    public class ConsoleLogger : ILog
    {
        private readonly Boolean _onlyDebugLogging;

        public ConsoleLogger(bool onlyDebugLogging)
        {
            _onlyDebugLogging = onlyDebugLogging;
        }

        public void Info(params String[] info)
        {
            if (!_onlyDebugLogging)
            {
                LogInternal(info);
                return;
            }
#if DEBUG
            LogInternal(info);
#endif
        }

        private static void LogInternal(string[] info)
        {
            for (int i = 0; i < info.Length; i++)
            {
                Console.WriteLine($"[{DateTime.Now}] {info[i]}");
            }
        }
    }
}
