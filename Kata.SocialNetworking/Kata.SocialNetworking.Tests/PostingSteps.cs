using Kata.SocialNetwork.Messages;
using Kata.SocialNetworking.Client;
using System;
using System.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Kata.SocialNetworking.Tests
{
    [Binding]
    public class PostingSteps : ClientTopToBottomTest
    {
        [Given(@"I'm using the system as ""(.*)""")]
        public void GivenIMUsingTheSystemAs(string userName)
        {
            ScenarioContext.Current.Add("userName", userName);
        }
        
        [When(@"I post the message ""(.*)""")]
        public void WhenIPostTheMessage(string message)
        {
            var userName = ScenarioContext.Current["userName"].ToString();
            WriteToClient($"{userName} -> {message}");
        }

        [Then(@"I should see ""(.*)""")]
        public void ThenIShouldSee(string expectedMessage)
        {
            var clientText = ReadClientText();
            Assert.That(clientText.Contains(expectedMessage), Is.EqualTo(true));
        }
    }
}
