namespace Kata.SocialNetworking.Boundaries.Messaging
{
    public interface IHandleMessagesOf<TMessage> where TMessage : IMessage
    {
        void Handle(TMessage message);
    }
}
