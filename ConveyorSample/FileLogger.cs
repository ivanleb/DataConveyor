using DataConveyor;
using System;
using System.IO;

namespace ConveyorSample
{
    public class FileLogger : ILog
    {
        private readonly Boolean _onlyDebugLogging;
        private readonly StreamWriter _writer;

        public FileLogger(String filePath, bool onlyDebugLogging)
        {
            _writer = new StreamWriter(filePath, append: false);
            _onlyDebugLogging = onlyDebugLogging;
        }

        public void Info(params String[] infos)
        {
            if (!_onlyDebugLogging)
            {
                LogInternal(infos);
                return;
            }
#if DEBUG
            LogInternal(infos);
#endif
        }

        private void LogInternal(string[] infos)
        {
            for (int i = 0; i < infos.Length; i++)
            {
                _writer.WriteLine(infos[i]);
            }
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}
