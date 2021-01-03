using System;

namespace DataConveyor
{
    public interface IConveyorBuilder
    {
        IConveyorBuilder Start<TOutput>(IOutputConveyorBlock<TOutput> block);
        Boolean TryAdd<TInput, TOutput>(IConveyorBlock<TInput, TOutput> block);
        Conveyor End<TInput>(IInputConveyorBlock<TInput> block);
    }
}
