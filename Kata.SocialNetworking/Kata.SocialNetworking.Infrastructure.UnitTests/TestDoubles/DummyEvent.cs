using Kata.SocialNetworking.Boundaries.Messaging;

namespace Kata.SocialNetworking.Infrastructure.UnitTests.TestDoubles
{
    public class DummyEvent : IEvent
    {
        public int Id { get; }

        public DummyEvent(int id)
        {
            Id = id;
        }
    }
}