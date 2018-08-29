using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Kata.SocialNetworking.Messages.Post;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kata.SocialNetworking.Client.UnitTests
{
    [TestFixture]
    public class WhenPresentingUserWall
    {
        private string userName;
        private string message;
        private string anotherMessage;
        private WallPresenter wallPresenter;
        private FakeClock fakeClock;

        [SetUp]
        public void SetUp()
        {
            userName = "Alice";
            message = "a message";
            anotherMessage = "a different message!";
            fakeClock = new FakeClock();
            wallPresenter = new WallPresenter(fakeClock);
        }

        [Test]
        public void PresentsAUsersPost()
        {
            var userWall = GetWallBasedOnPosts(userName, new MessagePosted(userName, message, fakeClock.Now()));

            Assert.That(userWall[0], Is.EqualTo("Alice -> a message (0 seconds ago)"));
        }

        [Test]
        public void PresentesAnAggregateOfUserPosts()
        {
            wallPresenter.AppendToUsersWall(new MessagePosted(userName, message, fakeClock.Now()));

            wallPresenter.AppendToUsersWall(new MessagePosted(userName, anotherMessage, fakeClock.Now().AddSeconds(1)));

            var userWall = wallPresenter.PresentWallFor(userName);

            Assert.That(userWall[0], Is.EqualTo("Alice -> a message (0 seconds ago)"));
            Assert.That(userWall[1], Is.EqualTo("Alice -> a different message! (1 second ago)"));
        }

        private string[] GetWallBasedOnPosts(string userName, params MessagePosted[] posts)
        {
            foreach (var post in posts)
            {
                wallPresenter.AppendToUsersWall(post);
            }

            return wallPresenter.PresentWallFor(userName);
        }
    }

    public class WallPresenter
    {
        private readonly IClock clock;
        public WallPresenter(IClock clock)
        {
            this.clock = clock;
        }

        private readonly Dictionary<string, List<string>> walls = new Dictionary<string, List<string>>();

        public void AppendToUsersWall(MessagePosted messagePosted)
        {
            var messageToBeAdded = $"{messagePosted.UserName} -> {messagePosted.Message} {DefineTimeElapsed(messagePosted.Date)}";
            bool hasNeverBuiltThisUsersWallBefore = !walls.ContainsKey(messagePosted.UserName);

            if (hasNeverBuiltThisUsersWallBefore)
            {
                walls.Add(messagePosted.UserName, new List<string> { messageToBeAdded });
            }
            else
            {
                walls[messagePosted.UserName].Add(messageToBeAdded);
            }
        }

        private string DefineTimeElapsed(DateTime date)
        {
            var secondsElapsed = (date - clock.Now()).Seconds;
            var usePluralIfApplicable = secondsElapsed != 1 ? "s" : "";
            return $"({secondsElapsed} second{usePluralIfApplicable} ago)";
        }

        public string[] PresentWallFor(string userName)
        {
            return walls[userName].ToArray();
        }
    }

    public interface IClock
    {
        DateTime Now();
    }

    public class FakeClock : IClock
    {
        public DateTime HypoteticalNow { get; set; } = new DateTime(2018, 01, 01);

        public DateTime Now()
        {
            return HypoteticalNow;
        }
    }
}
