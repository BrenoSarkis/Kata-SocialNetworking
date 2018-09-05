using Kata.SocialNetworking.Infrastructure.Messaging;

namespace Kata.SocialNetworking.Messages.Follow
{
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
}