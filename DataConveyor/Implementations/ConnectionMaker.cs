using System;

namespace DataConveyor
{
    public class ConnectionMaker : IConnectionMaker
    {
        private readonly Int32 _defaultCacheLimit = 1;
        private readonly ILog _log;

        public ConnectionMaker(ILog log, int defaultCacheLimit)
        {
            _log = log;
            _defaultCacheLimit = defaultCacheLimit;
        }

        public IConnectionMaker Connect<T>(IOutputConveyorBlock<T> source, IInputConveyorBlock<T> target)
            where T : class
        {
            var connector = new Connector<T>(_defaultCacheLimit, _log).Connect(source, target);

            _log.Info($"Make connection beetwen {source} and {target}. {connector}");
            return this;
        }
    }

    public static class ConnectorEx 
    {
        public static IConnector<T> Connect<T>(this IConnector<T> connector, IOutputConveyorBlock<T> source, IInputConveyorBlock<T> target)
            where T : class
        {
            var sourceConnector = source.Connect(connector);
            var targetConnector = target.Connect(connector);

            IConnector<T> commonConnector;
            if (sourceConnector != connector)
                commonConnector = sourceConnector;
            else if (targetConnector != connector)
                commonConnector = targetConnector;
            else return connector;

            source.Connect(commonConnector);
            target.Connect(commonConnector);
            return commonConnector;

        }
    }
}
