using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Messages.Post;
using Kata.SocialNetworking.Post;
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

    public class UserFollowed : IEvent
    {
        public string SourceUser { get; }
        public string TargetUser { get; }

        public UserFollowed(string sourceUser, string targetUser)
        {
            SourceUser = sourceUser;
            TargetUser = targetUser;
        }
    }

    public class FollowUser : ICommand
    {
        public string SourceUser { get; }
        public string TargetUser { get; }

        public FollowUser(string sourceUser, string targetUser)
        {
            SourceUser = sourceUser;
            TargetUser = targetUser;
        }
    }

    public class FollowUserHandler : IHandleMessagesOf<FollowUser>
    {
        private Bus bus;

        public FollowUserHandler(Bus bus)
        {
            this.bus = bus;
        }

        public void Handle(FollowUser message)
        {
            bus.Publish(new UserFollowed(message.SourceUser, message.TargetUser));
        }
    }
}
