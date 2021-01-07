using System;

namespace DataConveyor
{
    public interface ILog
    {
        void Info(params String[] infos);
    }
    public class DefaultLog : ILog
    {
        public void Info(params string[] infos)
        {
            
        }
    }
}
