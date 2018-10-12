using System;
using Kata.SocialNetworking.Boundaries.Messaging;

namespace Kata.SocialNetworking.Client
{
    public class UserView
    {
        private readonly WallPresenter wallPresenter;
        private readonly IBus bus;

        public UserView(WallPresenter wallPresenter, IBus bus)
        {
            this.wallPresenter = wallPresenter;
            this.bus = bus;
        }

        public void Show()
        {
            while (true)
            {
                var input = Console.ReadLine();
                var inputTranslator = new InputTranslator(new UserController(bus), wallPresenter);
                inputTranslator.TranslateIntoAction(input);
                Console.Write(wallPresenter.ViewModel.Output);
            }
        }
    }
}
