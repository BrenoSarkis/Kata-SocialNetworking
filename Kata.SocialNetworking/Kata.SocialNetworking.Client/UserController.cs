using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kata.SocialNetworking.Boundaries.Messaging;
using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Clock;
using Kata.SocialNetworking.Messages.Follow;
using Kata.SocialNetworking.Messages.Post;
using Kata.SocialNetworking.Post;

namespace Kata.SocialNetworking.Client
{
    public class UserController : IUserController
    {
        private readonly IBus bus;

        public UserController(IBus bus)
        {
            this.bus = bus;
        }

        public WallPresenter Presenter { get; set; }
        public UserViewModel ViewModel { get; } = new UserViewModel();

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
