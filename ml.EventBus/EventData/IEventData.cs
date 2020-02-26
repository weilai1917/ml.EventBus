using System;
using System.Collections.Generic;
using System.Text;

namespace ml.EventBus.EventData
{
    public interface IEventData
    {
        DateTime EventTime { get; set; }
    }
}
