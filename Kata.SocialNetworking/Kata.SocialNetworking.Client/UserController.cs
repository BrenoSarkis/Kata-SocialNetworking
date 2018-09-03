using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Clock;
using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Messages.Post;
using Kata.SocialNetworking.Post;

namespace Kata.SocialNetworking.Client
{
    public class UserController
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
    }
}
