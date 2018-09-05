using Kata.SocialNetworking.Infrastructure.Messaging;

namespace Kata.SocialNetworking.Messages.Follow
{
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
}