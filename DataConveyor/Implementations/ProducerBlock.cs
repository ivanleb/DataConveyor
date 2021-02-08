﻿using System;

namespace DataConveyor
{
    public class ProducerBlock<TOutput> : ProducerConveyorBlock<TOutput>
    {
        public String Name { get; }
        public ProducerBlock(String name, Func<(TOutput Data, Boolean IsStopped)> dataGenerator) 
            : base(dataGenerator)
        {
            Name = name;
        }
    }
}
