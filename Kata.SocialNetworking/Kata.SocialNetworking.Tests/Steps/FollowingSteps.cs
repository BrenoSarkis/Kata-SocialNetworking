using NUnit.Framework;
using TechTalk.SpecFlow;

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
    }
}
