using System;

namespace DataConveyor
{
    public class ConnectionMaker : IConnectionMaker
    {
        private readonly ConnectionSpec _сonnectionSpec;
        private readonly ILog _log;

        public ConnectionMaker(ILog log, ConnectionSpec defaultConnectionSpec)
        {
            _log = log;
            _сonnectionSpec = defaultConnectionSpec;
        }

        public IConnectionMaker Connect<T>(IOutputConveyorBlock<T> source, IInputConveyorBlock<T> target)
            where T : class
        {
            if (source.TryConnect(target, _сonnectionSpec))
            { 
                _log.Info($"Make connection beetwen {source} and {target} with {source.Connector}.");
                return this;
            }
            throw new Exception($"Cannot connect {source} and {source}");
        }
    }
}
