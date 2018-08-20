namespace Kata.SocialNetworking.Infrastructure
{
    public interface IHandleCommandsOf<TCommandType> where TCommandType : ICommand
    {
        void Handle(TCommandType command);
    }
}