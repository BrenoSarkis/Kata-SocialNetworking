using Kata.SocialNetworking.Boundaries.Messaging;
using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Messages.Follow;

namespace Kata.SocialNetworking.Follow
{
    public class FollowUserHandler : IHandleMessagesOf<FollowUser>
    {
        private IBus bus;

        public FollowUserHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(FollowUser message)
        {
            bus.Publish(new UserFollowed(message.SourceUser, message.TargetUser));
        }
    }
}