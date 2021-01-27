using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DataConveyor
{
    public class Conveyor : IDisposable
    {
        private Dictionary<Guid, IBlock> _blocks;
        private Boolean _isPaused;

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

        public void PauseResume() 
        {
            if (_isPaused)
            {
                Resume();
                _isPaused = false;
            }
            else
            {
                Pause();
                _isPaused = true;
            }
        }

        private void Pause() 
        {
            foreach (var block in _blocks.Values)
            {
                block.Pause();
            }
        }

        private  void Resume()
        {
            foreach (var block in _blocks.Values)
            {
                block.Run(block);
            }
        }

        public void Stop() 
        {
            foreach (var block in _blocks.Values)
            {
                block.Stop();
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
