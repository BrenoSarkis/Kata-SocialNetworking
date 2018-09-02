using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Clock;
using Kata.SocialNetworking.Messages.Post;
using Kata.SocialNetworking.Post;

namespace Kata.SocialNetworking.Client
{
    public class UserController
    {
        public WallPresenter Presenter { get; } = new WallPresenter(new Clock());
        public UserViewModel ViewModel { get; } = new UserViewModel();

        public void PostMessage(string message)
        {
            var bus = new Bus();
            bus.RegisterHandlers(new PostMessageHandler(bus));
            bus.RegisterHandlers(Presenter);

            var splittedMessage = message.Split(new[] { "->" }, StringSplitOptions.None);
            bus.SendCommand(new PostMessage(userName: splittedMessage[0].Trim(), message: splittedMessage[1].Trim()));
        }

        public void PrepareWallFor(string userName)
        {
            ViewModel.Output = String.Join(Environment.NewLine, Presenter.PresentWallFor(userName));
        }
    }
}
