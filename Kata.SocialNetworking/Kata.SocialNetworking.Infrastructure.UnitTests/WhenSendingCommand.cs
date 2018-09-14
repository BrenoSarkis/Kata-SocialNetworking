using Kata.SocialNetworking.Boundaries.Messaging;
using Kata.SocialNetworking.Infrastructure.Exceptions;
using NSubstitute;
using NUnit.Framework;

namespace Kata.SocialNetworking.Infrastructure.UnitTests
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

        [Test]
        public void ThrowsWhenThereAreMoreThanOneHandler()
        {
            var aHandler = Substitute.For<IHandleMessagesOf<DummyCommand>>();
            var anotherHandler = Substitute.For<IHandleMessagesOf<DummyCommand>>();

            bus.RegisterHandlers(aHandler);

            Assert.That(() => bus.RegisterHandlers(anotherHandler),
                Throws.TypeOf<HandlerAlreadyRegisteredException>().With.Message.EqualTo("There is handler already registered for DummyCommand"));
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
