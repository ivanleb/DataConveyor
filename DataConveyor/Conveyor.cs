using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DataConveyor
{
    public class Conveyor : IDisposable
    {
        private Dictionary<Guid, IBlock> _blocks;

        public Conveyor(params IBlock[] blocks)
        {
            _blocks = blocks.ToDictionary(item => item.Id);
        }

        public Conveyor Add(IBlock block) 
        {
            _blocks.TryAdd(block.Id, block);
            return this;
        }

        public void Run()
        {
            foreach (var block in _blocks.Values)
            {
                ThreadPool.QueueUserWorkItem(block.Run);
            }
        }

        public void Dispose()
        {
            foreach (var block in _blocks.Values)
            {
                block.Dispose();
            }
        }
    }
}
