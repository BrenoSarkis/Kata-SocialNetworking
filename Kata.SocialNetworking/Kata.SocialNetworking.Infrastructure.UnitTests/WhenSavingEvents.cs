using System;
using System.Collections.Generic;
using System.Linq;
using Kata.SocialNetworking.Infrastructure.Messaging;
using NUnit.Framework;

namespace Kata.SocialNetworking.Infrastructure.UnitTests
{
    [TestFixture]
    public class WhenSavingEvents
    {
        [Test]
        public void EventsAreStored()
        {
            var aggregateId = Guid.Parse("CE1B3BFC-D7BA-4734-826A-7D5C59DB43DE");
            var eventStore = new EventStore();

            eventStore.Save(aggregateId, new DummyEvent(1));
            var newlyStoredEvent = eventStore.GetEventsForAggregate(aggregateId).Cast<DummyEvent>().Single();

            Assert.That(newlyStoredEvent.Id, Is.EqualTo(1));
        }
    }

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
