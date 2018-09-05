using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Messages.Follow;

namespace Kata.SocialNetworking.Follow
{
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