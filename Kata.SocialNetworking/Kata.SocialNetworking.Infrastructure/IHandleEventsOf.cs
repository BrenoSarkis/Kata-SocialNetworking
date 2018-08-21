namespace Kata.SocialNetworking.Infrastructure
{
    public interface IHandleEventsOf<TEventType> where TEventType : IEvent
    {
        void Handle(TEventType eventType);
    }
}