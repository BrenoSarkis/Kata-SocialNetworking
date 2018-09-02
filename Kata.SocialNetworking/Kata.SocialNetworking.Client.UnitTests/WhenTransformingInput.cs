using System;
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
}