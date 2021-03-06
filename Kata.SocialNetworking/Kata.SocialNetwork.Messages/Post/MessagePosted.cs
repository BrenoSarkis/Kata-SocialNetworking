﻿using Kata.SocialNetworking.Infrastructure.Messaging;

namespace Kata.SocialNetwork.Messages.Post
{
    public class MessagePosted : IEvent
    {
        public string UserName { get; }
        public string Message { get; }

        public MessagePosted(string userName, string message)
        {
            UserName = userName;
            Message = message;
        }
    }
}
