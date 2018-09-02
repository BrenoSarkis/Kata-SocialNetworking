using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Messages.Post;
using NSubstitute;
using NUnit.Framework;

namespace Kata.SocialNetworking.Client.UnitTests
{
    [TestFixture]
    public class WhenTranslatingInput
    {
        [Test]
        public void TranslatesIntoPostMessage()
        {
            var controller = Substitute.For<UserController>(Substitute.For<IBus>());

            var inputTranslator = new InputTranslator(controller);

            inputTranslator.TranslateIntoCommand("Alice -> hello!");

            controller.Received().PostMessage(Arg.Is<PostMessage>(postMessage => postMessage.UserName == "Alice" 
                                                                              && postMessage.Message == "hello!"));
        }
    }
}