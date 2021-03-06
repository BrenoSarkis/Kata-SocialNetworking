﻿using System;
using System.Threading;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Kata.SocialNetworking.Tests.Steps
{
    [Binding]
    public class PostingSteps : UserFeatures
    {
        [Given(@"I post the message ""(.*)""")]
        public void GivenIPostTheMessage(string input)
        {
            InputTranslator.TranslateIntoAction(input);
        }

        [Given(@"I wait for (.*) seconds")]
        public void GivenIWaitForSeconds(int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
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