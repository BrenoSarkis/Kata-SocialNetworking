using Kata.SocialNetworking.Infrastructure;
using NSubstitute;
using NUnit.Framework;

namespace Kata.SocialNetwork.Infrastructure.UnitTests
{
    [TestFixture]
    public class WhenPublishingEvent
    {
        [Test]
        public void ItIsDeliveredToAHandler()
        {
            var id = 1;
            var dummyEvent = new DummyEvent(id: id);
            var bus = new Bus();
            var eventHandlerMock = Substitute.For<IHandleEventsOf<DummyEvent>>();

            bus.RegisterEventHandlers(eventHandlerMock);

            bus.Publish(dummyEvent);

            eventHandlerMock.Received().Handle(Arg.Is<DummyEvent>(@event => @event.Id == id));
        }
    }

    public class DummyEvent : IEvent
    {
        public int Id { get; }

        public DummyEvent(int id)
        {
            Id = id;
        }
    }
}
