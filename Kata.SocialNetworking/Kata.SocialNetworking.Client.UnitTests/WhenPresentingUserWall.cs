using System;
using Kata.SocialNetworking.Messages.Follow;
using Kata.SocialNetworking.Messages.Post;
using NUnit.Framework;

namespace Kata.SocialNetworking.Client.UnitTests
{
    [TestFixture]
    public class WhenPresentingUserWall
    {
        private string userName;
        private string message;
        private string anotherMessage;
        private string yetAnotherMessage;
        private WallPresenter wallPresenter;
        private UserViewModel viewModel;
        private FakeClock fakeClock;

        [SetUp]
        public void SetUp()
        {
            userName = "Alice";
            message = "a message";
            anotherMessage = "a different message!";
            yetAnotherMessage = "yet a different message.";
            fakeClock = new FakeClock();
            viewModel = new UserViewModel();
            wallPresenter = new WallPresenter(fakeClock, viewModel);
        }

        [Test]
        public void PresentsAUsersPost()
        {
            var userWall = GetWallBasedOnPosts(userName, new MessagePosted(userName, message, fakeClock.Now()));

            Assert.That(userWall[0], Is.EqualTo("Alice - a message (0 seconds ago)"));
        }

        [Test]
        public void PresentesAnAggregateOfUserPostsWithSecondsOfDifference()
        {
            var userWall = GetWallBasedOnPosts(userName, new MessagePosted(userName, message, fakeClock.Now()),
                                                         new MessagePosted(userName, anotherMessage, fakeClock.Now().AddSeconds(1)));

            Assert.That(userWall[0], Is.EqualTo("Alice - a message (0 seconds ago)"));
            Assert.That(userWall[1], Is.EqualTo("Alice - a different message! (1 second ago)"));
        }

        [Test]
        public void PresentesAnAggregateOfUserPostsWithMinutesOfDifference()
        {
            var userWall = GetWallBasedOnPosts(userName, new MessagePosted(userName, message, fakeClock.Now()),
                                                         new MessagePosted(userName, anotherMessage, fakeClock.Now().AddMinutes(1)),
                                                         new MessagePosted(userName, yetAnotherMessage, fakeClock.Now().AddMinutes(2)));
                

            Assert.That(userWall[0], Is.EqualTo("Alice - a message (0 seconds ago)"));
            Assert.That(userWall[1], Is.EqualTo("Alice - a different message! (1 minute ago)"));
            Assert.That(userWall[2], Is.EqualTo("Alice - yet a different message. (2 minutes ago)"));
        }

        [Test]
        public void PresentsBlankWallWhenThereIsNoPostsForUser()
        {
            wallPresenter.PrepareWallFor("ANewUserName");

            Assert.That(wallPresenter.ViewModel.Output, Is.EqualTo(String.Empty));
        }

        [Test]
        public void AggregatesTheirMessagesToTheSourceUsersWall()
        {
            var alicesPost = new MessagePosted("Alice", "A message from Alice", fakeClock.HypoteticalNow);
            var aliceFollowedBob = new UserFollowed("Alice", "Bob");
            var bobsPost = new MessagePosted("Bob", "A message from Bob", fakeClock.HypoteticalNow.AddSeconds(1));

            wallPresenter.Handle(alicesPost);
            wallPresenter.Handle(aliceFollowedBob);
            wallPresenter.Handle(bobsPost);

            var alicesWall = GetWallFor(userName);

            Assert.That(alicesWall[0], Is.EqualTo("Alice - A message from Alice (0 seconds ago)"));
            Assert.That(alicesWall[1], Is.EqualTo("Bob - A message from Bob (1 second ago)"));
        }

        [Test]
        public void ShowsOnlyMessagesFromTheFollowedUser()
        {
            var aliceFollowedBob = new UserFollowed("Alice", "Bob");
            var bobsPost = new MessagePosted("Bob", "A message from Bob", fakeClock.HypoteticalNow);

            wallPresenter.Handle(aliceFollowedBob);
            wallPresenter.Handle(bobsPost);

            var alicesWall = GetWallFor(userName);

            Assert.That(alicesWall[0], Is.EqualTo("Bob - A message from Bob (0 seconds ago)"));
        }


        [Test]
        public void ShowsMessagesFromMultipleFollowedUsers()
        {
            var aliceFollowedBob = new UserFollowed("Alice", "Bob");
            var aliceFollowedCharlie = new UserFollowed("Alice", "Charlie");

            var bobsPost = new MessagePosted("Bob", "A message from Bob", fakeClock.HypoteticalNow);
            var charliePost = new MessagePosted("Charlie", "A message from Charlie", fakeClock.HypoteticalNow);

            wallPresenter.Handle(aliceFollowedBob);
            wallPresenter.Handle(aliceFollowedCharlie);
            wallPresenter.Handle(bobsPost);
            wallPresenter.Handle(charliePost);

            var alicesWall = GetWallFor(userName);

            Assert.That(alicesWall[0], Is.EqualTo("Bob - A message from Bob (0 seconds ago)"));
            Assert.That(alicesWall[1], Is.EqualTo("Charlie - A message from Charlie (0 seconds ago)"));
        }

        private string[] GetWallBasedOnPosts(string userName, params MessagePosted[] posts)
        {
            foreach (var post in posts)
            {
                wallPresenter.Handle(post);
            }

            return GetWallFor(userName);
        }

        private string[] GetWallFor(string userName)
        {
            wallPresenter.PrepareWallFor(userName);
            return wallPresenter.ViewModel.Output.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
        }
    }
}