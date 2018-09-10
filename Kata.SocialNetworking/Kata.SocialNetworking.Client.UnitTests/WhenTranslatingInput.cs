using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Messages.Follow;
using Kata.SocialNetworking.Messages.Post;
using NSubstitute;
using NUnit.Framework;

namespace Kata.SocialNetworking.Client.UnitTests
{
    [TestFixture]
    public class WhenTranslatingInput
    {
        private readonly UserController controller = Substitute.For<UserController>(Substitute.For<IBus>());
        private readonly IPresentWalls presenter = Substitute.For<IPresentWalls>();

        private const string UserName = "Alice";
        private const string AnotherUserName = "Bob";
        private const string Message = "hello!";

        [Test]
        public void TranslatesIntoPostMessage()
        {
            var inputTranslator = new InputTranslator(controller, presenter);

            inputTranslator.TranslateIntoAction($"{UserName} -> {Message}");

            controller.Received().PostMessage(Arg.Is<PostMessage>(postMessage => postMessage.UserName == UserName 
                                                                              && postMessage.Message == Message));
        }

        [Test]
        public void TranslatesIntoGetWall()
        {
            var inputTranslator = new InputTranslator(controller, presenter);

            inputTranslator.TranslateIntoAction(UserName);

            presenter.Received().PrepareWallFor(Arg.Is<string>(userName => userName == UserName));
        }

        [Test]
        public void TranslatesIntoFollowing()
        {
            var inputTranslator = new InputTranslator(controller, presenter);

            inputTranslator.TranslateIntoAction($"{UserName} follows {AnotherUserName}");

            controller.Received().FollowUser(Arg.Is<FollowUser>(follow => follow.SourceUser == UserName
                                                                       && follow.TargetUser == AnotherUserName));
        }
    }
}