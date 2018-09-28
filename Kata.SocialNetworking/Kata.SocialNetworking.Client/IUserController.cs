using Kata.SocialNetworking.Messages.Follow;
using Kata.SocialNetworking.Messages.Post;

namespace Kata.SocialNetworking.Client
{
    public interface IUserController
    {
        void PostMessage(PostMessage postMessage);
        void FollowUser(FollowUser followUser);
    }
}