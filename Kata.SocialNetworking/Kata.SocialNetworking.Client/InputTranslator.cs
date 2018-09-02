using System;
using System.Linq;
using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Messages.Post;

namespace Kata.SocialNetworking.Client
{
    public class InputTranslator
    {
        private readonly UserController controller;

        public InputTranslator(UserController controller)
        {
            this.controller = controller;
        }

        public void TranslateIntoCommand(string input)
        {
            var splittedInput = input.Split(new[] { "->" }, StringSplitOptions.None).Select(i => i.Trim()).ToArray();
            this.controller.PostMessage(new PostMessage(userName: splittedInput[0], message: splittedInput[1]));
        }
    }
}
