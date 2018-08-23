using Kata.SocialNetwork.Messages.Post;
using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Messaging;

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
            this.bus.Publish(new MessagePosted(message.UserName, message.Message));
        }
    }
}
