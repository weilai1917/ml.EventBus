using System;
using System.Collections.Generic;
using System.Text;

namespace ml.EventBus.EventData
{
    public class EventData : IEventData
    {
        public DateTime EventTime
        {
            get { return DateTime.Now; }
            set { }
        }
    }
}
