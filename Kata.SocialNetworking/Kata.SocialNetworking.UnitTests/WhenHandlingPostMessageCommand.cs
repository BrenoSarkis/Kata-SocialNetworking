using Kata.SocialNetwork.Messages.Post;
using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Infrastructure.Storage;
using Kata.SocialNetworking.Post;
using NSubstitute;
using NUnit.Framework;

namespace Kata.SocialNetworking.UnitTests
{
    [TestFixture]
    public class WhenHandlingPostMessageCommand
    {
        private const string UserName = "aUserName";
        private const string Message = "aMessage";
        private IEventStore eventStore;
        private Bus bus;
        private PostMessageHandler postMessageHandler;
        private PostMessage postMessageCommand;

        [SetUp]
        public void SetUp()
        {
            eventStore = Substitute.For<IEventStore>();
            bus = new Bus();
            postMessageHandler = new PostMessageHandler(bus, eventStore);
            postMessageCommand = new PostMessage(UserName, Message);
        }

        [Test]
        public void RaisesMessagePostedEvent()
        {
            var messagePostedHandler = Substitute.For<IHandleMessagesOf<MessagePosted>>();

            bus.RegisterHandlers(postMessageHandler);
            bus.RegisterHandlers(messagePostedHandler);

            bus.SendCommand(postMessageCommand);

            messagePostedHandler.Received().Handle(Arg.Is<MessagePosted>(mp => mp.UserName == UserName && mp.Message == Message));
        }

        [Test]
        public void MessagePostedIsStored()
        {
            postMessageHandler.Handle(postMessageCommand);

            eventStore.Received().Save(Arg.Is<MessagePosted>(mp => mp.UserName == UserName && mp.Message == Message));
        }
    }
}
