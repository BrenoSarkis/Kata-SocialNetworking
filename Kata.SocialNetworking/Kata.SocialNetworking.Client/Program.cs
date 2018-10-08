using System;
using Kata.SocialNetworking.Boundaries.Messaging;
using Kata.SocialNetworking.DependencyInversion;
using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Clock;
using Kata.SocialNetworking.Messages.Follow;
using Kata.SocialNetworking.Messages.Post;
using Ninject;

namespace Kata.SocialNetworking.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = new StandardKernel(new DependencyBootstrapper());

            var wallPresenter = new WallPresenter(new Clock(), new UserViewModel());

            var bus = container.Get<IBus>();

            bus.RegisterHandlers((IHandleMessagesOf<MessagePosted>)wallPresenter);
            bus.RegisterHandlers((IHandleMessagesOf<UserFollowed>)wallPresenter);

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