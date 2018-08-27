using System;
using System.Collections.Generic;
using Kata.SocialNetworking.Infrastructure.Messaging;

namespace Kata.SocialNetworking.Infrastructure
{
    public class EventStore
    {
        private readonly Dictionary<Guid, List<IEvent>> events = new Dictionary<Guid, List<IEvent>>();

        public void Save(Guid aggregateId, IEvent @event)
        {
            if (events.ContainsKey(aggregateId)) events[aggregateId].Add(@event);
            else events[aggregateId] = new List<IEvent> { @event };
        }

        public IEvent[] GetEventsForAggregate(Guid aggregateId)
        {
            return events[aggregateId].ToArray();
        }
    }
}
