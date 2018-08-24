using Kata.SocialNetwork.Messages.Post;
using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Infrastructure.Storage;

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
            this.eventStore.Save(new MessagePosted(message.UserName, message.Message));
            this.bus.Publish(new MessagePosted(message.UserName, message.Message));
        }
    }
}
