using System;
using Kata.SocialNetworking.Boundaries.Clock;

namespace Kata.SocialNetworking.Infrastructure.Clock
{
    public class Clock : IClock
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}