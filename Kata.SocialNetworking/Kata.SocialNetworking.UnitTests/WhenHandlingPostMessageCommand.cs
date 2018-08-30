using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Messages.Post;
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
        private Bus bus;
        private PostMessageHandler postMessageHandler;
        private PostMessage postMessageCommand;

        [SetUp]
        public void SetUp()
        {
            bus = new Bus();
            postMessageHandler = new PostMessageHandler(bus);
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
    }
}
