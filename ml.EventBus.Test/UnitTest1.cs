using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ml.EventBus.EventData;

namespace ml.EventBus.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //EventBus.Instance.RegistAllEventHandlerFromAssembly(Assembly.GetExecutingAssembly());
            //Console.WriteLine("Hello World!");
            //EventBus.Instance.Publish<TestEventData>(new TestEventData());



        }
    }

    public class TestEventData : IEventData
    {
        public DateTime EventTime
        {
            get { return DateTime.Now; }
            set { }
        }
    }

    public class TestEventHandler : IEventHandler<TestEventData>
    {
        public void Handle(TestEventData ent)
        {
            Console.WriteLine($"已完成订单，时间：{ent.EventTime}");
        }
    }
}
