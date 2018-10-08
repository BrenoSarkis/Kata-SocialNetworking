using Kata.SocialNetworking.Boundaries.Messaging;
using Kata.SocialNetworking.Follow;
using Kata.SocialNetworking.Infrastructure;
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
            this.Bind<IBus>().ToConstant(bus);
        }
    }
}
