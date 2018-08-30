using System;
using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Infrastructure.Storage;
using Kata.SocialNetworking.Messages.Post;

namespace Kata.SocialNetworking.Post
{
    public class PostMessageHandler : IHandleMessagesOf<PostMessage>
    {
        private readonly Bus bus;
        private readonly IEventStore eventStore;

        public PostMessageHandler(Bus bus, IEventStore eventStore)
        {
            this.bus = bus;
            this.eventStore = eventStore;
        }

        public void Handle(PostMessage message)
        {
            var messagePosted = new MessagePosted(message.UserName, message.Message, DateTime.Now);
            //eventStore.Save(messagePosted);
            bus.Publish(messagePosted);
        }
    }
}
