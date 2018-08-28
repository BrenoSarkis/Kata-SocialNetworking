using Kata.SocialNetworking.Messages.Post;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kata.SocialNetworking.Client.UnitTests
{
    [TestFixture]
    public class WhenPresentingUserWall
    {
        [Test]
        public void PresentsAUsersPost()
        {
            var UserName = "Alice";
            var Message = "a message";
            var presenter = new WallPresenter();

            var userWall = presenter.PreparePresentationFor(new MessagePosted(UserName, Message));

            Assert.That(userWall, Is.EqualTo("Alice -> a message"));
        }
    }

    public class WallPresenter
    {
        public string PreparePresentationFor(MessagePosted messagePosted)
        {
            return $"{messagePosted.UserName} -> {messagePosted.Message}";
        }
    }
}
