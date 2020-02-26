using ml.EventBus.EventData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ml.EventBus
{
    public interface IEventHandler
    { 
    }

    public interface IEventHandler<TEvent> : IEventHandler
        where TEvent : IEventData
    {
        void Handle(TEvent ent);
    }
}
