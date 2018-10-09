using Kata.SocialNetworking.Boundaries.Messaging;
using Kata.SocialNetworking.Follow;
using Kata.SocialNetworking.MessageBus;
using Kata.SocialNetworking.Post;
using Ninject.Modules;

namespace Kata.SocialNetworking.DependencyInversion
{
    public class DependencyBootstrapper : NinjectModule
    {
        public override void Load()
        {
            var bus = new Bus();
            bus.RegisterHandlers(new PostMessageHandler(bus));
            bus.RegisterHandlers(new FollowUserHandler(bus));
            Bind<IBus>().ToConstant(bus);
        }
    }
}