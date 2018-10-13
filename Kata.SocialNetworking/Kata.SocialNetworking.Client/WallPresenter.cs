using System;
using System.Collections.Generic;
using System.Linq;
using Kata.SocialNetworking.Boundaries.Clock;
using Kata.SocialNetworking.Boundaries.Messaging;
using Kata.SocialNetworking.Messages.Follow;
using Kata.SocialNetworking.Messages.Post;

namespace Kata.SocialNetworking.Client
{
    public class WallPresenter : IPresentWalls, IHandleMessagesOf<MessagePosted>, IHandleMessagesOf<UserFollowed>
    {
        private readonly IClock clock;
        private readonly Dictionary<string, List<MessagePosted>> walls = new Dictionary<string, List<MessagePosted>>();
        private readonly Dictionary<string, List<string>> followers = new Dictionary<string, List<string>>();
        public UserViewModel ViewModel { get; }

        public WallPresenter(IClock clock, UserViewModel viewModel)
        {
            this.clock = clock;
            ViewModel = viewModel;
        }

        public void PrepareWallFor(string userName)
        {
            string wall = "";

            SetUpWallFor(userName);

            bool userIsFollowingAnyone = followers.ContainsKey(userName);

            if (userIsFollowingAnyone)
            {
                var followedUsersWall = GetFollowedUsersWallFor(userName);
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

        private IEnumerable<MessagePosted> GetFollowedUsersWallFor(string userName)
        {
            var followedUsersWalls = new List<MessagePosted>();
            foreach (var followedUser in followers[userName])
            {
                followedUsersWalls.AddRange(walls[followedUser]);
            }
            return followedUsersWalls;
        }

        private void SetUpWallFor(string userName)
        {
            if (!walls.ContainsKey(userName))
            {
                walls.Add(userName, new List<MessagePosted>());
            }
        }

        public void Handle(MessagePosted messagePosted)
        {
            bool hasNeverBuiltThisUsersWallBefore = !walls.ContainsKey(messagePosted.UserName);

            if (hasNeverBuiltThisUsersWallBefore)
            {
                walls.Add(messagePosted.UserName, new List<MessagePosted> { messagePosted });
            }
            else
            {
                walls[messagePosted.UserName].Add(messagePosted);
            }
        }

        private string DefineTimeElapsed(DateTime date)
        {
            var timeElapsed = (date - clock.Now()).Duration();
            var secondsElapsed = timeElapsed.Seconds;
            var minutesElapsed = timeElapsed.Minutes;

            return minutesElapsed > 0 ? $"({minutesElapsed} minute{FormatTime(minutesElapsed)} ago)" 
                                      : $"({secondsElapsed} second{FormatTime(secondsElapsed)} ago)";
        }

        private string FormatTime(int value)
        {
            return value != 1 ? "s" : "";
        }

        private string FormatPosts(IEnumerable<MessagePosted> posts)
        {
            return String.Join(Environment.NewLine, posts.Select(post => $"{post.UserName} - {post.Message} {DefineTimeElapsed(post.Date)}"));
        }

        public void Handle(UserFollowed message)
        {
            SetUpFollowersFor(message.SourceUser);

            followers[message.SourceUser].Add(message.TargetUser);
        }

        private void SetUpFollowersFor(string userName)
        {
            if (!followers.ContainsKey(userName))
            {
                followers.Add(userName, new List<string>());
            }
        }
    }
}