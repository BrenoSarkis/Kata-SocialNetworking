using Kata.SocialNetwork.Messages.Post;
using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Post;
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
}
