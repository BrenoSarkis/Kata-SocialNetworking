namespace Kata.SocialNetworking.Infrastructure.Storage
{
    public interface IEventStore
    {
        void Save<IEvent>(IEvent @event);
    }
}
