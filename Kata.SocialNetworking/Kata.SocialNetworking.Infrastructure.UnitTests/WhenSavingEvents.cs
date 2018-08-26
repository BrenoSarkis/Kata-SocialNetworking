using NUnit.Framework;

namespace Kata.SocialNetworking.Infrastructure.UnitTests
{
    [TestFixture]
    public class WhenSavingEvents
    {
        [Test]
        public void EventsAreStored()
        {
            var eventStore = new EventStore();

            eventStore.Save(new DummyEvent(1));
        }
    }

    public class EventStore
    {
        public void Save(DummyEvent dummyEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}
