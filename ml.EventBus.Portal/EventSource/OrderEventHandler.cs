using ml.EventBus.EventData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ml.EventBus.Portal.EventSource
{
    public class OrderEventData : IEventData
    {
        public DateTime EventTime
        {
            get { return DateTime.Now; }
            set { }
        }
    }

    public class OrderEventHandler : IEventHandler<OrderEventData>
    {
        public void Handle(OrderEventData ent)
        {
            Console.WriteLine($"已完成订单，时间：{ent.EventTime}");
        }
    }

    public class ABCEventHandler : IEventHandler<OrderEventData>
    {
        public void Handle(OrderEventData ent)
        {
            Console.WriteLine($"Test，时间：{ent.EventTime}");
        }
    }
}
