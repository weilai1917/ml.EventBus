using ml.EventBus.Portal.EventSource;
using System;
using System.Reflection;

namespace ml.EventBus.Portal
{
    class Program
    {
        static void Main(string[] args)
        {
            //EventBus.Instance.RegistAllEventHandlerFromAssembly(Assembly.GetExecutingAssembly());
            //Console.WriteLine("Hello World!");
            //EventBus.Instance.Publish<OrderEventData>(new OrderEventData());

            EventBus.Instance.PublishGeneric<OrderEventData>(x =>
            {
                //...
            });
        }
    }
}
