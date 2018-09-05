using TechTalk.SpecFlow;

namespace Kata.SocialNetworking.Tests
{
    [Binding]
    public class FollowSteps 
    {
        [Given(@"User posts message: ""(.*)""")]
        public void GivenUserPostsMessage(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"""(.*)"" follows ""(.*)""")]
        public void WhenFollows(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"""(.*)""'s wall should contain: ""(.*)""")]
        public void ThenSWallShouldContain(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
