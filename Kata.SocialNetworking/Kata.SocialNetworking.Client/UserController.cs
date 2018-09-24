using Kata.SocialNetworking.Boundaries.Messaging;
using Kata.SocialNetworking.Messages.Follow;
using Kata.SocialNetworking.Messages.Post;

namespace Kata.SocialNetworking.Client
{
    public class UserController : IUserController
    {
        private readonly IBus bus;

        public UserController(IBus bus)
        {
            this.bus = bus;
        }

        public void PostMessage(PostMessage postMessage)
        {
            bus.SendCommand(postMessage);
        }

        public void FollowUser(FollowUser followUser)
        {
            bus.SendCommand(followUser);
        }
    }

    public interface IUserController
    {
        void PostMessage(PostMessage postMessage);
        void FollowUser(FollowUser followUser);
    }
}
