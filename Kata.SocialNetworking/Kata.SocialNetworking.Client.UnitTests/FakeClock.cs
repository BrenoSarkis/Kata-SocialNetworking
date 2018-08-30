using System;
using Kata.SocialNetworking.Infrastructure.Clock;

namespace Kata.SocialNetworking.Client.UnitTests
{
    public class FakeClock : IClock
    {
        public DateTime HypoteticalNow { get; set; } = new DateTime(2018, 01, 01);

        public DateTime Now()
        {
            return HypoteticalNow;
        }
    }
}
