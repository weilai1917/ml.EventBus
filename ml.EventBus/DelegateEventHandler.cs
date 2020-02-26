using ml.EventBus.EventData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ml.EventBus
{
    internal class DelegateEventHandler<TEventData> : IEventHandler<TEventData>
         where TEventData : IEventData
    {
        public Action<TEventData> Action { get; private set; }

        public DelegateEventHandler(Action<TEventData> action)
        {
            Action = action;
        }

        public void Handle(TEventData ent)
        {
            Action(ent);
        }
    }
}
