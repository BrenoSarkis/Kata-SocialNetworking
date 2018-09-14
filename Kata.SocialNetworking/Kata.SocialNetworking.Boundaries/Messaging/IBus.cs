
namespace Kata.SocialNetworking.Boundaries.Messaging
{
    public interface IBus
    {
        void RegisterHandlers<TMessage>(params IHandleMessagesOf<TMessage>[] handlers) where TMessage : IMessage;
        void SendCommand<TCommandType>(TCommandType command) where TCommandType : ICommand;
        void Publish<TEventType>(TEventType @event) where TEventType : IEvent;
    }
}
