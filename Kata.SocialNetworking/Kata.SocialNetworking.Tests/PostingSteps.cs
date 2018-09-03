using Kata.SocialNetworking.Client;
using System;
using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Clock;
using Kata.SocialNetworking.Messages.Post;
using Kata.SocialNetworking.Post;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Kata.SocialNetworking.Tests
{
    [Binding]
    public class PostingSteps : UserFeatures
    {
        [Given(@"I post the message ""(.*)""")]
        public void GivenIPostTheMessage(string input)
        {
            InputTranslator.TranslateIntoAction(input);
        }

        [When(@"I visit ""(.*)""'s wall")]
        public void WhenIVisitSWall(string input)
        {
            InputTranslator.TranslateIntoAction(input);
        }

        [Then(@"I should see ""(.*)""")]
        public void ThenIShouldSee(string expectedMessage)
        {
            Assert.That(WallPresenter.ViewModel.Output, Is.EqualTo(expectedMessage));
        }
    }
}