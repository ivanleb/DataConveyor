using System;

namespace DataConveyor
{
    public interface IBlock : IDisposable
    {
        void Run(Object state);
    }
}
