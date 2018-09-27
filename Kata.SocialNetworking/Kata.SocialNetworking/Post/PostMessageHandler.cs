using System;
using Kata.SocialNetworking.Boundaries.Messaging;
using Kata.SocialNetworking.Messages.Post;

namespace Kata.SocialNetworking.Post
{
    public class PostMessageHandler : IHandleMessagesOf<PostMessage>
    {
        private readonly IBus bus;

        public PostMessageHandler(IBus bus)
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