﻿using System;
using System.Linq;
using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Messages.Post;
using NUnit.Framework;

namespace Kata.SocialNetworking.Client.UnitTests
{
    [TestFixture]
    public class WhenTransformingInput
    {
        [Test]
        public void SuccessfullyParsesASendCommand()
        {
            var inputParser = new InputParser();

            var command = inputParser.Parse("Alice -> hello!");

            Assert.That(command, Is.TypeOf<PostMessage>());
            Assert.That(((PostMessage)command).UserName, Is.EqualTo("Alice"));
            Assert.That(((PostMessage)command).Message, Is.EqualTo("hello!"));
        }
    }

    public class InputParser
    {
        public ICommand Parse(string input)
        {
            var splittedInput = input.Split(new[] {"->"}, StringSplitOptions.None).Select(i => i.Trim()).ToArray();
            return new PostMessage(userName: splittedInput[0], message: splittedInput[1]);
        }
    }
}