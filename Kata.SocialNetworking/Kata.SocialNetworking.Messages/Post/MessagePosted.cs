using System;
using Kata.SocialNetworking.Boundaries.Messaging;

namespace Kata.SocialNetworking.Messages.Post
{
    public class MessagePosted : IEvent
    {
        public string UserName { get; }
        public string Message { get; }
        public DateTime Date { get; set; }

        public MessagePosted(string userName, string message, DateTime date)
        {
            UserName = userName;
            Message = message;
            Date = date;
        }
    }
}