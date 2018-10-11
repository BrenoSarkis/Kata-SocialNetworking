using System;
using System.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Kata.SocialNetworking.Tests.Steps
{
    [Binding]
    public class FollowingSteps : UserFeatures
    {
        [Given(@"User posts message: ""(.*)""")]
        public void GivenUserPostsMessage(string postInput)
        {
            InputTranslator.TranslateIntoAction(postInput);
        }

        [When(@"""(.*)"" follows ""(.*)""")]
        public void WhenFollows(string user, string anotherUser)
        {
            InputTranslator.TranslateIntoAction($"{user} follows {anotherUser}");
        }

        [Then(@"""(.*)""'s wall should contain: ""(.*)""")]
        public void ThenSWallShouldContain(string userWallInput, string wallContent)
        {
            InputTranslator.TranslateIntoAction(userWallInput);
            Assert.That(WallPresenter.ViewModel.Output, Is.EqualTo(wallContent));
        }

        [Then(@"""(.*)""'s wall should display:")]
        public void ThenSWallShouldDisplay(string userWallInput, Table wallContentTable)
        {
            var wallContent = String.Join(Environment.NewLine, wallContentTable.Rows.Select(r => r["Message"]));
            InputTranslator.TranslateIntoAction(userWallInput);
            Assert.That(WallPresenter.ViewModel.Output, Is.EqualTo(wallContent));
        }
    }
}
