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
}
