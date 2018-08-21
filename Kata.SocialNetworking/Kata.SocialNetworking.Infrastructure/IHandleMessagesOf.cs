namespace Kata.SocialNetworking.Infrastructure
{
    public interface IHandleMessagesOf<TMessage> where TMessage : IMessage
    {
        void Handle(TMessage message);
    }
}
