using System;
using System.Collections.Generic;
using Kata.SocialNetworking.Infrastructure.Clock;
using Kata.SocialNetworking.Messages.Post;

namespace Kata.SocialNetworking.Client
{
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
