using Kata.SocialNetworking.Infrastructure.Clock;
using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Messages.Post;
using NSubstitute;
using NUnit.Framework;

namespace Kata.SocialNetworking.Client.UnitTests
{
    [TestFixture]
    public class WhenTranslatingInput
    {
        private const string UserName = "Alice";

        [Test]
        public void TranslatesIntoPostMessage()
        {
            var presenter = Substitute.For<IPresentWalls>();
            var controller = Substitute.For<UserController>(Substitute.For<IBus>());

            var inputTranslator = new InputTranslator(controller, presenter);

            inputTranslator.TranslateIntoAction("Alice -> hello!");

            controller.Received().PostMessage(Arg.Is<PostMessage>(postMessage => postMessage.UserName == UserName 
                                                                              && postMessage.Message == "hello!"));
        }

        [Test]
        public void TranslatesIntoGetWall()
        {
            var controller = Substitute.For<UserController>(Substitute.For<IBus>());
            var presenter = Substitute.For<IPresentWalls>();

            var inputTranslator = new InputTranslator(controller, presenter);

            inputTranslator.TranslateIntoAction(UserName);

            presenter.Received().PrepareWallFor(Arg.Is<string>(userName => userName == UserName));
        }
    }
}