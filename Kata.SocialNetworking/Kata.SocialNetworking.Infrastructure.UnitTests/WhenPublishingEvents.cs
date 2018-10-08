using Kata.SocialNetworking.Boundaries.Messaging;
using Kata.SocialNetworking.Infrastructure.UnitTests.TestDoubles;
using Kata.SocialNetworking.MessageBus;
using NSubstitute;
using NUnit.Framework;

namespace Kata.SocialNetworking.Infrastructure.UnitTests
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
}