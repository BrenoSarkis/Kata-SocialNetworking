using Kata.SocialNetworking.Infrastructure;
using Kata.SocialNetworking.Infrastructure.Exceptions;
using NSubstitute;
using NUnit.Framework;

namespace Kata.SocialNetwork.Infrastructure.UnitTests
{
    [TestFixture]
    public class WhenSendingCommand
    {
        private Bus bus;
        private DummyCommand dummyCommand;
        private int id = 1;

        [SetUp]
        public void SetUp()
        {
            bus = new Bus();
            dummyCommand = new DummyCommand(id);
        }

        [Test]
        public void ItIsDeliveredToItsHandler()
        {
            var commandHandlerMock = Substitute.For<IHandleMessagesOf<DummyCommand>>();

            bus.RegisterHandlers(commandHandlerMock);

            bus.SendCommand(dummyCommand);

            commandHandlerMock.Received().Handle(Arg.Is<DummyCommand>(command => command.Id == id));
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
        public int Id { get; }

        public DummyCommand(int id)
        {
            Id = id;
        }
    }
}
