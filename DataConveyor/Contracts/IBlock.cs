using System;

namespace DataConveyor
{
    public interface IBlock : IDisposable
    {
        Guid Id { get; }
        void Run(Object state);
        void Stop();
        void Pause();
    }
}
