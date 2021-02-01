using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DataConveyor
{
    public enum ConveyorState 
    {
        NotStarted = 0,
        Running = 10,
        Paused = 20,
        Resumed = 30,
        Stopped = 40
    }

    public class Conveyor : IDisposable
    {
        private Dictionary<Guid, IBlock> _blocks;
        public ConveyorState ConveyorState { get; private set; } = 0;

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
            ConveyorState = ConveyorState.Running;
        }

        public ConveyorState PauseResume()
        {
            switch (ConveyorState)
            {
                case ConveyorState.Running:
                case ConveyorState.Resumed:
                    {
                        Pause();
                        ConveyorState = ConveyorState.Paused;
                        return ConveyorState;
                    }

                case ConveyorState.Paused:
                    {
                        Resume();
                        ConveyorState = ConveyorState.Resumed;
                        return ConveyorState;
                    }

                case ConveyorState.NotStarted:
                case ConveyorState.Stopped:                    
                default:
                    return ConveyorState;
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
            ConveyorState = ConveyorState.Stopped;
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
