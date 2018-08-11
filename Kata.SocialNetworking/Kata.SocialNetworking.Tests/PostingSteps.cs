using System;
using TechTalk.SpecFlow;

namespace Kata.SocialNetworking.Tests
{
    [Binding]
    public class PostingSteps
    {
        [Given(@"I'm using the system as ""(.*)""")]
        public void GivenIMUsingTheSystemAs(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I post the message ""(.*)""")]
        public void WhenIPostTheMessage(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I should see ""(.*)""")]
        public void ThenIShouldSee(string p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
