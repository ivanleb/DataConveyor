using System;

namespace DataConveyor
{
    public class ConnectionSpec
    {
        public ConnectionSpec(Int32 maxBufferSize, ILog log)
        {
            MaxBufferSize = maxBufferSize;
            Log = log;
        }

        private ConnectionSpec() { }

        public Int32 MaxBufferSize { get; private set; }
        public ILog Log { get; private set; }
        public static ConnectionSpec Default => new ConnectionSpec { MaxBufferSize = 1 , Log = new DefaultLog() };
    }
}