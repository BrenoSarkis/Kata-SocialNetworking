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
            Assert.That(UserController.ViewModel.Output, Is.EqualTo(expectedMessage));
        }
    }

    public class UserController
    {
        public WallPresenter Presenter { get; } = new WallPresenter(new Clock());
        public ViewModel ViewModel { get; } = new ViewModel();

        public void PostMessage(string message)
        {
            var bus = new Bus();
            bus.RegisterHandlers(new PostMessageHandler(bus));
            bus.RegisterHandlers(Presenter);

            var splittedMessage = message.Split(new [] {"->"}, StringSplitOptions.None);
            bus.SendCommand(new PostMessage(userName: splittedMessage[0].Trim(), message: splittedMessage[1].Trim()));
        }

        public void PrepareWallFor(string userName)
        {
            ViewModel.Output = String.Join(Environment.NewLine, Presenter.PresentWallFor(userName));
        }
    }

    public class ViewModel
    {
        public string Output { get; set; }
    }
}