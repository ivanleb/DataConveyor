using System;

namespace DataConveyor
{
    internal static class ConveyorBlockEx
    {
        public static Boolean Connect<TInput>(this IInputConveyorBlock<TInput> inputBlock, IOutputConveyorBlock<TInput> outputBlock, ConnectionSpec spec)
            where TInput : class
        {
            if (inputBlock.Connector == null && outputBlock.Connector == null)
            {
                inputBlock.Connector = new Connector<TInput>(spec.MaxBufferSize, spec.Log);

                return outputBlock.TryConnect(inputBlock, spec);
            }

            if (inputBlock.Connector == null && outputBlock.Connector != null)
            {
                inputBlock.Connector = outputBlock.Connector;
                return true;
            }

            if (inputBlock.Connector != null && outputBlock.Connector == null)
            {
                outputBlock.Connector = inputBlock.Connector;
                return true; ;
            }
            return (inputBlock.Connector == outputBlock.Connector);
        }

        public static Boolean Connect<TOutput>(this IOutputConveyorBlock<TOutput> outputBlock, IInputConveyorBlock<TOutput> inputBlock, ConnectionSpec spec)
            where TOutput : class
        {
            if (outputBlock.Connector == null && inputBlock.Connector == null)
            {
                outputBlock.Connector = new Connector<TOutput>(spec.MaxBufferSize, spec.Log);

                return inputBlock.TryConnect(outputBlock, spec);
            }

            if (outputBlock.Connector == null && inputBlock.Connector != null)
            {
                outputBlock.Connector = inputBlock.Connector;
                return true;
            }

            if (outputBlock.Connector != null && inputBlock.Connector == null)
            {
                inputBlock.Connector = outputBlock.Connector;
                return true;
            }
            return (outputBlock.Connector == inputBlock.Connector);
        }
    }
}
