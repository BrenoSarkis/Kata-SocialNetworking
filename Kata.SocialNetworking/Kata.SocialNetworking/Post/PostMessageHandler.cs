using System;
using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Messages.Post;

namespace Kata.SocialNetworking.Post
{
    public class PostMessageHandler : IHandleMessagesOf<PostMessage>
    {
        private readonly Bus bus;

        public PostMessageHandler(Bus bus)
        {
            this.bus = bus;
        }

        public void Handle(PostMessage message)
        {
            var messagePosted = new MessagePosted(message.UserName, message.Message, DateTime.Now);
            bus.Publish(messagePosted);
        }
    }
}
