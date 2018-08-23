using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Messaging;
using NSubstitute;
using NUnit.Framework;

namespace Kata.SocialNetworking.UnitTests
{
    [TestFixture]
    public class WhenHandlingPostMessageCommand
    {
        [Test]
        public void RaisesMessagePostedEvent()
        {
            var messagePostedHandler = Substitute.For<IHandleMessagesOf<MessagePosted>>();
            var command = new PostMessage("aUserName", "aMessage");
            var bus = new Bus();

            bus.RegisterHandlers(new PostMessageHandler(bus));
            bus.RegisterHandlers(messagePostedHandler);

            bus.SendCommand(command);

            messagePostedHandler.Received().Handle(Arg.Is<MessagePosted>(mp => mp.UserName == "aUserName" && mp.Message == "aMessage"));
        }
    }

    public class PostMessage : ICommand
    {
        public string UserName { get; }
        public string Message { get; }

        public PostMessage(string userName, string message)
        {
            UserName = userName;
            Message = message;
        }
    }

    public class PostMessageHandler : IHandleMessagesOf<PostMessage>
    {
        private readonly Bus bus;

        public PostMessageHandler(Bus bus)
        {
            this.bus = bus;
        }

        public void Handle(PostMessage message)
        {
            this.bus.Publish(new MessagePosted(message.UserName, message.Message));
        }
    }

    public class MessagePosted : IEvent
    {
        public string UserName { get; }
        public string Message { get; }

        public MessagePosted(string userName, string message)
        {
            UserName = userName;
            Message = message;
        }
    }
}
