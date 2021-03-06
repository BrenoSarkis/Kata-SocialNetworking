﻿using Kata.SocialNetworking.Boundaries.Clock;
using Kata.SocialNetworking.Boundaries.Messaging;
using Kata.SocialNetworking.Client;
using Kata.SocialNetworking.Follow;
using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Clock;
using Kata.SocialNetworking.MessageBus;
using Kata.SocialNetworking.Messages.Follow;
using Kata.SocialNetworking.Messages.Post;
using Kata.SocialNetworking.Post;

namespace Kata.SocialNetworking.Tests
{
    public class UserFeatures
    {
        public UserFeatures()
        {
            WallPresenter = new WallPresenter(Clock, new UserViewModel());
            UserController = new UserController(Bus);

            Bus.RegisterHandlers(new PostMessageHandler(Bus));
            Bus.RegisterHandlers(new FollowUserHandler(Bus));
            Bus.RegisterHandlers((IHandleMessagesOf<MessagePosted>)WallPresenter);
            Bus.RegisterHandlers((IHandleMessagesOf<UserFollowed>)WallPresenter);

            InputTranslator = new InputTranslator(UserController, WallPresenter);
        }

        public IBus Bus { get; } = new Bus();
        public IClock Clock { get; } = new Clock();
        public UserController UserController { get; }
        public WallPresenter WallPresenter { get; }
        public InputTranslator InputTranslator { get; }
    }
}