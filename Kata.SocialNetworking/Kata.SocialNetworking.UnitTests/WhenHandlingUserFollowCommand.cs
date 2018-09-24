using Kata.SocialNetworking.Boundaries.Messaging;
using Kata.SocialNetworking.Follow;
using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Messages.Follow;
using NSubstitute;
using NUnit.Framework;

namespace Kata.SocialNetworking.UnitTests
{
    [TestFixture]
    public class WhenHandlingUserFollowCommand
    {
        private const string SourceUser = "aUserName";
        private const string TargetUser = "anotherUser";
        private Bus bus;
        private FollowUserHandler followUserHandler;
        private FollowUser followUserCommand;

        [SetUp]
        public void SetUp()
        {
            bus = new Bus();
            followUserHandler = new FollowUserHandler(bus);
            followUserCommand = new FollowUser(SourceUser, TargetUser);
        }

        [Test]
        public void RaisesUserFollowedEvent()
        {
            var userFollowedHandler = Substitute.For<IHandleMessagesOf<UserFollowed>>();

            bus.RegisterHandlers(followUserHandler);
            bus.RegisterHandlers(userFollowedHandler);

            bus.SendCommand(followUserCommand);

            userFollowedHandler.Received().Handle(Arg.Is<UserFollowed>(mp => mp.SourceUser == SourceUser && mp.TargetUser == TargetUser));
        }
    }
}