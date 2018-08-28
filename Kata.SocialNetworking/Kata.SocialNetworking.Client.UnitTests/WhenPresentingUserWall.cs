using System.Collections.Generic;
using System.Linq;
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

        [SetUp]
        public void SetUp()
        {
            userName = "Alice";
            message = "a message";
            anotherMessage = "a different message!";
            wallPresenter = new WallPresenter();
        }

        [Test]
        public void PresentsAUsersPost()
        {
            var userWall = GetWallBasedOnPosts(userName, new MessagePosted(userName, message));

            Assert.That(userWall[0], Is.EqualTo("Alice -> a message"));
        }

        [Test]
        public void PresentesAnAggregateOfUserPosts()
        {
            var userWall = GetWallBasedOnPosts(userName, new MessagePosted(userName, message),
                                                         new MessagePosted(userName, anotherMessage));

            Assert.That(userWall[0], Is.EqualTo("Alice -> a message"));
            Assert.That(userWall[1], Is.EqualTo("Alice -> a different message!"));
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
        private readonly Dictionary<string, List<string>> walls = new Dictionary<string, List<string>>();

        public void AppendToUsersWall(MessagePosted messagePosted)
        {
            var messageToBeAdded = $"{messagePosted.UserName} -> {messagePosted.Message}";
            bool hasNeverBuiltThisUsersWallBefore = !walls.ContainsKey(messagePosted.UserName);

            if (hasNeverBuiltThisUsersWallBefore)
            {
                walls.Add(messagePosted.UserName, new List<string> {messageToBeAdded});
            }
            else
            {
                walls[messagePosted.UserName].Add(messageToBeAdded);
            }
        }

        public string[] PresentWallFor(string userName)
        {
            return walls[userName].ToArray();
        }
    }
}
