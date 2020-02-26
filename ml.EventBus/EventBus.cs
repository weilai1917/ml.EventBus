using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ml.EventBus.EventData;

namespace ml.EventBus
{
    public class EventBus
    {
        private static EventBus _eventBus = null;

        public static EventBus Instance
        {
            get
            {
                return _eventBus ?? new EventBus();
            }
        }

        private static IContainer IocContainer { get; set; }
        private static ConcurrentDictionary<Type, List<Type>> _eventHandlers
             = new ConcurrentDictionary<Type, List<Type>>();

        private readonly object sync = new object();

        public void RegistAllEventHandlerFromAssembly(Assembly assembly)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(assembly)
                .Named<IEventHandler>((entHandler) =>
                {
                    if (typeof(IEventHandler).IsAssignableFrom(entHandler))
                    {
                        var genricArgs = entHandler.GetInterface("IEventHandler`1").GetGenericArguments();
                        if (genricArgs.Length == 1)
                        {
                            RegisterHandler(genricArgs[0], entHandler);
                            return genricArgs[0].Name;
                        }
                    }
                    return "";
                }).SingleInstance();
            IocContainer = builder.Build();
        }

        private List<Type> GetHandlers(Type eventType)
        {
            return _eventHandlers.GetOrAdd(eventType, x => new List<Type>());
        }

        private void RegisterHandler(Type eventType, Type handlerType)
        {
            GetHandlers(eventType).Add(handlerType);
        }

        public void Publish<TEventData>(TEventData evnt)
            where TEventData : IEventData
        {
            if (evnt == null)
                throw new ArgumentNullException("evnt");

            List<Type> handlerTypes = _eventHandlers[typeof(TEventData)];

            if (handlerTypes != null && handlerTypes.Count > 0)
            {
                foreach (var handlerType in handlerTypes)
                {
                    //var handlerInterface = handlerType.GetInterface("IEventHandler`1");
                    var eventHandlers = IocContainer.ResolveKeyed<IEnumerable<IEventHandler>>(typeof(TEventData).Name);
                    foreach (var eventHandler in eventHandlers)
                    {
                        if (handlerType == eventHandler.GetType())
                        {
                            var handler = eventHandler as IEventHandler<TEventData>;
                            handler.Handle(evnt);
                        }
                    }
                }
            }


        }

        public Task PublishAsnyc<TEventData>(TEventData evnt)
            where TEventData : IEventData
        {
            return Task.Run(() => { Publish<TEventData>(evnt); });
        }

        public void PublishGeneric<TEventData>(Action<TEventData> action)
            where TEventData : IEventData
        {
            var delegateEventHandler = new DelegateEventHandler<TEventData>(action);

            var builder = new ContainerBuilder();
            builder.RegisterType<DelegateEventHandler<TEventData>>().Named<IEventHandler>(typeof(TEventData).Name);

            RegisterHandler(typeof(TEventData), delegateEventHandler.GetType());

        }

        public Task PublishGenericAsnyc<TEventData>(Action<TEventData> action)
                   where TEventData : IEventData
        {
            return Task.Run(() => { PublishGenericAsnyc<TEventData>(action); });
        }
    }
}
