using System;

namespace DataConveyor
{
    public class ConnectionSpec
    {
        public ConnectionSpec(Int32 maxBufferSize)
        {
            MaxBufferSize = maxBufferSize;
        }

        private ConnectionSpec() { }

        public Int32 MaxBufferSize { get; private set; }
        public static ConnectionSpec Default => new ConnectionSpec { MaxBufferSize = 1 };
    }
}