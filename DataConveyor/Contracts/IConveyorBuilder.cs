using System;

namespace DataConveyor
{
    public interface IConveyorBuilder
    {
        IConveyorBuilder Start<TOutput>(IOutputConveyorBlock<TOutput> block) where TOutput : class;
        Boolean TryAdd<TInput, TOutput>(IConveyorBlock<TInput, TOutput> block) where TInput : class where TOutput : class;
        Conveyor End<TInput>(IInputConveyorBlock<TInput> block) where TInput : class;
    }
}
