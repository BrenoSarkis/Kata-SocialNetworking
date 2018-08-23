using Kata.SocialNetworking.Infrastructure.Messaging;

namespace Kata.SocialNetwork.Messages.Post
{
    public class PostMessage : ICommand
    {
        public string UserName { get; }
        public string Message { get; }

        public PostMessage(string userName, string message)
        {
            UserName = userName;
            Message = message;
        }
    }
}
