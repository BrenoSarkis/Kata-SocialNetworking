using Kata.SocialNetworking.Client;
using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using NSubstitute;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Kata.SocialNetworking.Tests
{
    [Binding]
    public class PostingSteps
    {

        [Given(@"I post the message ""(.*)""")]
        public void GivenIPostTheMessage(string message)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I visit ""(.*)""'s wall")]
        public void WhenIVisitSWall(string userName)
        {
            ScenarioContext.Current.Pending();
        }


        [Then(@"I should see ""(.*)""")]
        public void ThenIShouldSee(string expectedMessage)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
