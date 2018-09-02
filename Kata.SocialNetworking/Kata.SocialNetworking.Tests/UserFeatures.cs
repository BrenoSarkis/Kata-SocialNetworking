using Kata.SocialNetworking.Client;
using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Clock;
using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Post;

namespace Kata.SocialNetworking.Tests
{
    public class UserFeatures
    {
        public UserFeatures()
        {
            WallPresenter = new WallPresenter(Clock);
            UserController = new UserController(Bus);
            UserController.Presenter = WallPresenter;

            Bus.RegisterHandlers(new PostMessageHandler(Bus));
            Bus.RegisterHandlers(WallPresenter);

            InputTranslator = new InputTranslator(UserController);
        }

        public IBus Bus { get; } = new Bus();
        public IClock Clock { get; } = new Clock();
        public UserController UserController { get; }
        public WallPresenter WallPresenter { get; }
        public InputTranslator InputTranslator { get; }
    }
}
