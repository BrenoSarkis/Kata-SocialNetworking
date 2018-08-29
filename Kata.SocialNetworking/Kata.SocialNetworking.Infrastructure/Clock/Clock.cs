using System;

namespace Kata.SocialNetworking.Infrastructure.Clock
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
