using Kata.SocialNetworking.Client;
using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Messages.Post;
using NSubstitute;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Kata.SocialNetworking.Tests
{
    [Binding]
    public class PostingSteps
    {
        public UserController UserController { get; } = new UserController();

        [Given(@"I post the message ""(.*)""")]
        public void GivenIPostTheMessage(string command)
        {
            UserController.PostMessage(command);
        }

        [When(@"I visit ""(.*)""'s wall")]
        public void WhenIVisitSWall(string userName)
        {
            UserController.PrepareWallFor(userName);
        }

        [Then(@"I should see ""(.*)""")]
        public void ThenIShouldSee(string expectedMessage)
        {
            Assert.That(UserController.ViewModel.Output.ToString(), Is.EqualTo(expectedMessage));
        }
    }

    public class UserController
    {
        public Presenter Presenter { get; } = new Presenter();
        public ViewModel ViewModel { get; } = new ViewModel();

        public void PostMessage(string message)
        {
        }

        public void PrepareWallFor(string userName)
        {
            ViewModel.Output.Append(Presenter.GetWallFor(userName));
        }
    }

    public class ViewModel
    {
        public StringBuilder Output { get; set; }
    }

    public class Presenter
    {
        public string[] GetWallFor(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
