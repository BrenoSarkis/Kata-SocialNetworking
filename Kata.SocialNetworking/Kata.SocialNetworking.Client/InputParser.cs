using System;
using System.Linq;
using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Messages.Post;

namespace Kata.SocialNetworking.Client
{
    public class InputParser
    {
        public ICommand Parse(string input)
        {
            var splittedInput = input.Split(new[] { "->" }, StringSplitOptions.None).Select(i => i.Trim()).ToArray();
            return new PostMessage(userName: splittedInput[0], message: splittedInput[1]);
        }
    }
}
