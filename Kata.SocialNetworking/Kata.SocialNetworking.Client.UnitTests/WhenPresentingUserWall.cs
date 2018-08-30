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
        private FakeClock fakeClock;

        [SetUp]
        public void SetUp()
        {
            userName = "Alice";
            message = "a message";
            anotherMessage = "a different message!";
            yetAnotherMessage = "yet a different message.";
            fakeClock = new FakeClock();
            wallPresenter = new WallPresenter(fakeClock);
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

        private string[] GetWallBasedOnPosts(string userName, params MessagePosted[] posts)
        {
            foreach (var post in posts)
            {
                wallPresenter.Handle(post);
            }

            return wallPresenter.PresentWallFor(userName);
        }
    }
}
