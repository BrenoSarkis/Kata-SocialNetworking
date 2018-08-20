using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Exceptions;
using NSubstitute;
using NUnit.Framework;

namespace Kata.SocialNetwork.Infrastructure.IntegrationTests
{
    [TestFixture]
    public class WhenSendingCommand
    {
        private Bus bus;
        private DummyCommand dummyCommand;

        [SetUp]
        public void SetUp()
        {
            bus = new Bus();
            dummyCommand = new DummyCommand();
        }

        [Test]
        public void ItIsDeliveredToItsHandler()
        {
            var commandHandlerMock = Substitute.For<IHandleCommandsOf<DummyCommand>>();

            bus.RegisterCommandHandler(commandHandlerMock);

            bus.SendCommand(dummyCommand);

            commandHandlerMock.Received().Handle(Arg.Any<DummyCommand>());
        }

        [Test]
        public void ThrowsWhenHandlerIsNotRegistered()
        {
            Assert.That(() => bus.SendCommand(dummyCommand), 
                Throws.TypeOf<HandlerNotFoundException>().With.Message.EqualTo("No handler found for command DummyCommand"));
        }
    }

    public class DummyCommand : ICommand
    {
    }
}
