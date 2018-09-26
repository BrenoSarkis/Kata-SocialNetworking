using Kata.SocialNetworking.Boundaries.Messaging;

namespace Kata.SocialNetworking.Messages.Post
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