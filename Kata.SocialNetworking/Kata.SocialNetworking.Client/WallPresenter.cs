using System;
using System.Collections.Generic;
using System.Linq;
using Kata.SocialNetworking.Infrastructure.Clock;
using Kata.SocialNetworking.Infrastructure.Messaging;
using Kata.SocialNetworking.Messages.Follow;
using Kata.SocialNetworking.Messages.Post;

namespace Kata.SocialNetworking.Client
{
    public class WallPresenter : IPresentWalls, IHandleMessagesOf<MessagePosted>, IHandleMessagesOf<UserFollowed>
    {
        private readonly IClock clock;
        private readonly Dictionary<string, List<string>> walls = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, string> followers = new Dictionary<string, string>();
        public UserViewModel ViewModel { get; }

        public WallPresenter(IClock clock, UserViewModel viewModel)
        {
            this.clock = clock;
            ViewModel = viewModel;
        }

        public void PrepareWallFor(string userName)
        {
            string wall = "";

            IniatializeWall(userName);

            bool userIsFollowingAnyone = followers.ContainsKey(userName);

            if (userIsFollowingAnyone)
            {
                var followedUser = followers[userName];
                var followedUsersWall = walls[followedUser];
                var currentUsersWall = walls[userName].ToArray();

                var aggregatedMessages = currentUsersWall.Concat(followedUsersWall);

                wall = FormatPosts(aggregatedMessages);
            }
            else
            {
                wall = FormatPosts(walls[userName]);
            }

            ViewModel.Output = wall;
        }

        private void IniatializeWall(string userName)
        {
            if (!walls.ContainsKey(userName))
            {
                walls.Add(userName, new List<string>());
            }
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

        private string FormatPosts(IEnumerable<string> posts)
        {
            return String.Join(Environment.NewLine, posts);
        }

        public void Handle(UserFollowed message)
        {
            followers.Add(message.SourceUser, message.TargetUser);
        }
    }
}
