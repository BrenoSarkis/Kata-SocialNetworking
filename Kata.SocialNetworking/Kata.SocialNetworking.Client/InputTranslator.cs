﻿using System;
using System.Linq;
using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Messages.Post;

namespace Kata.SocialNetworking.Client
{
    public class InputTranslator
    {
        private readonly IUserController controller;
        private readonly IPresentWalls wallPresenter;

        public InputTranslator(IUserController controller, IPresentWalls wallPresenter)
        {
            this.controller = controller;
            this.wallPresenter = wallPresenter;
        }

        public void TranslateIntoAction(string input)
        {
            var splittedInput = input.Split(new[] { "->" }, StringSplitOptions.None).Select(i => i.Trim()).ToArray();
            if (splittedInput.Length == 2)
            {
                controller.PostMessage(new PostMessage(userName: splittedInput[0], message: splittedInput[1]));
            }
            else
            {
                wallPresenter.PrepareWallFor(input);
            }
        }
    }
}