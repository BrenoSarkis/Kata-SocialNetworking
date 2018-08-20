using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Exceptions;
using NSubstitute;
using NUnit.Framework;

namespace Kata.SocialNetwork.Infrastructure.IntegrationTests
{
    [TestFixture]
    public class WhenSendingCommand
    {
        [Test]
        public void ItIsDeliveredToItsHandler()
        {
            var dummyCommand = new DummyCommand();
            var bus = new Bus();
            var commandHandlerMock = Substitute.For<IHandleCommandsOf<DummyCommand>>();

            bus.RegisterCommandHandler(commandHandlerMock);

            bus.SendCommand(dummyCommand);

            commandHandlerMock.Received().Handle(Arg.Any<DummyCommand>());
        }

        [Test]
        public void ThrowsWhenHandlerIsNotRegistered()
        {
            var commandWithoutHandler = new DummyCommand();
            var bus = new Bus();
            
            Assert.That(() => bus.SendCommand(commandWithoutHandler), 
                Throws.TypeOf<HandlerNotFoundException>().With.Message.EqualTo($"No handler found for command DummyCommand"));
        }
    }

    public class DummyCommand : ICommand
    {
    }
}
