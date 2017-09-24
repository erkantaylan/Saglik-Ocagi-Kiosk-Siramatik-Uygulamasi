using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HealtCare.Common.Aggregator {

    public class EventAggregator : IEventAggregator {
        private readonly Dictionary<Type, List<WeakReference>> eventSubsribers = new Dictionary<Type, List<WeakReference>>();
        private readonly object lockSubscriberDictionary = new object();

        private void InvokeSubscriberEvent<TEventType>(TEventType eventToPublish, ISubscriber<TEventType> subscriber) {
            //Synchronize the invocation of method 
            SynchronizationContext syncContext = SynchronizationContext.Current ?? new SynchronizationContext();
            syncContext.Post(s => subscriber.OnEventHandler(eventToPublish), null);
        }

        private List<WeakReference> GetSubscriberList(Type subsriberType) {
            List<WeakReference> subsribersList;
            lock (lockSubscriberDictionary) {
                bool found = eventSubsribers.TryGetValue(subsriberType, out subsribersList);
                if (!found) {
                    //First time create the list.
                    subsribersList = new List<WeakReference>();
                    eventSubsribers.Add(subsriberType, subsribersList);
                }
            }
            return subsribersList;
        }

        #region IEventAggregator Members              

        /// <summary>
        ///     Publish an event
        /// </summary>
        /// <typeparam name="TEventType"></typeparam>
        /// <param name="eventToPublish"></param>
        public void PublishEvent<TEventType>(TEventType eventToPublish) {
            Type subsriberType = typeof(ISubscriber<>).MakeGenericType(typeof(TEventType));
            List<WeakReference> subscribers = GetSubscriberList(subsriberType);
            List<WeakReference> subsribersToBeRemoved = new List<WeakReference>();
            foreach (WeakReference weakSubsriber in subscribers) {
                if (weakSubsriber.IsAlive) {
                    ISubscriber<TEventType> subscriber = (ISubscriber<TEventType>) weakSubsriber.Target;
                    InvokeSubscriberEvent(eventToPublish, subscriber);
                } else {
                    subsribersToBeRemoved.Add(weakSubsriber);
                }
            }

            if (subsribersToBeRemoved.Any()) {
                lock (lockSubscriberDictionary) {
                    foreach (WeakReference remove in subsribersToBeRemoved) {
                        subscribers.Remove(remove);
                    }
                }
            }
        }

        /// <summary>
        ///     Subribe to an event.
        /// </summary>
        /// <param name="subscriber"></param>
        public void SubsribeEvent(object subscriber) {
            lock (lockSubscriberDictionary) {
                IEnumerable<Type> subsriberTypes = subscriber.GetType().GetInterfaces()
                                                             .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISubscriber<>));
                WeakReference weakReference = new WeakReference(subscriber);
                foreach (Type subsriberType in subsriberTypes) {
                    List<WeakReference> subscribers = GetSubscriberList(subsriberType);

                    subscribers.Add(weakReference);
                }
            }
        }

        #endregion
    }

}