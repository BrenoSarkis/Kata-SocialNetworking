using System;
using System.Collections.Generic;
using Kata.SocialNetworking.Infrastructure.Clock;
using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Messages.Post;

namespace Kata.SocialNetworking.Client
{
    public class WallPresenter : IHandleMessagesOf<MessagePosted>
    {
        private readonly IClock clock;
        private readonly Dictionary<string, List<string>> walls = new Dictionary<string, List<string>>();

        public WallPresenter(IClock clock)
        {
            this.clock = clock;
        }

        public void Handle(MessagePosted messagePosted)
        {
            var messageToBeAdded = $"{messagePosted.UserName} - {messagePosted.Message} {DefineTimeElapsed(messagePosted.Date)}";
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
            var timeElapsed = (date - clock.Now());
            var secondsElapsed = timeElapsed.Seconds;
            var minutesElapsed = timeElapsed.Minutes;

            if (minutesElapsed > 0)
            {
                return $"({minutesElapsed} minute{FormatTime(minutesElapsed)} ago)";
            }

            return $"({secondsElapsed} second{FormatTime(secondsElapsed)} ago)";
        }

        private string FormatTime(int value)
        {
            return value != 1 ? "s" : "";
        }

        public string[] PresentWallFor(string userName)
        {
            return walls[userName].ToArray();
        }
    }
}
