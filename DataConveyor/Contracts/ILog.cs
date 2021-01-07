using System;

namespace DataConveyor
{
    public interface ILog
    {
        void Info(params String[] infos);
    }
}
