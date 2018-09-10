using System;
using System.Linq;
using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Messages.Follow;
using Kata.SocialNetworking.Messages.Post;

namespace Kata.SocialNetworking.Client
{
    public class InputTranslator
    {
        private const string PostMessageIdentifier = "->";
        private const string FollowUserIdentifier = "follows";
        private readonly IUserController controller;
        private readonly IPresentWalls wallPresenter;

        public InputTranslator(IUserController controller, IPresentWalls wallPresenter)
        {
            this.controller = controller;
            this.wallPresenter = wallPresenter;
        }

        public void TranslateIntoAction(string input)
        {
            if (input.Contains(PostMessageIdentifier))
            {
                var splittedInput = SplitInputOn(input, PostMessageIdentifier); 
                controller.PostMessage(new PostMessage(userName: splittedInput[0], message: splittedInput[1]));
            }
            else if (input.Contains(FollowUserIdentifier))
            {
               var splittedInput = SplitInputOn(input, FollowUserIdentifier);
                controller.FollowUser(new FollowUser(sourceUser: splittedInput[0], targetUser: splittedInput[1]));
            }
            else
            {
                wallPresenter.PrepareWallFor(input);
            }
        }

        private string[] SplitInputOn(string input, string toSplit)
        {
            return input.Split(new[] { toSplit }, StringSplitOptions.None).Select(i => i.Trim()).ToArray();
        }
    }
}
