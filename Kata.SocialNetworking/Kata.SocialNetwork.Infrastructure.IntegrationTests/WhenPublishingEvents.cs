using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Messaging;
using NSubstitute;
using NUnit.Framework;

namespace Kata.SocialNetwork.Infrastructure.UnitTests
{
    [TestFixture]
    public class WhenPublishingEvents
    {
        [Test]
        public void DeliverThemToSubscribers()
        {
            var id = 1;
            var dummyEvent = new DummyEvent(id: id);
            var bus = new Bus();
            var aHandler = Substitute.For<IHandleMessagesOf<DummyEvent>>();
            var anotherHandler = Substitute.For<IHandleMessagesOf<DummyEvent>>();

            bus.RegisterHandlers(aHandler, anotherHandler);

            bus.Publish(dummyEvent);

            aHandler.Received().Handle(Arg.Is<DummyEvent>(@event => @event.Id == id));
            anotherHandler.Received().Handle(Arg.Is<DummyEvent>(@event => @event.Id == id));
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
