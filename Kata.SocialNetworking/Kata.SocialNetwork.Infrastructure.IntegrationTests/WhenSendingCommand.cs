using System;
using System.Collections.Generic;
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

    public class HandlerNotFoundException : Exception
    {
        public HandlerNotFoundException(string commandName) : base($"No handler found for command {commandName}")
        {
        }
    }

    public interface IHandleCommandsOf<TCommandType> where TCommandType : Command
    {
        void Handle(TCommandType command);
    }

    public class DummyCommand : Command
    {
    }

    public class Command
    {
    }

    public class Bus
    {
        private readonly Dictionary<Type, Action<Command>> commandHandlers = new Dictionary<Type, Action<Command>>();

        public void SendCommand<TCommandType>(TCommandType command) where TCommandType : Command
        {
            Action<Command> handler;

            if (commandHandlers.TryGetValue(typeof(TCommandType), out handler))
            {
                handler.Invoke(command);
            }
            else
            {
                throw new HandlerNotFoundException(typeof(TCommandType).Name);
            }

        }

        public void RegisterCommandHandler<TCommandType>(IHandleCommandsOf<TCommandType> commandHandler) where TCommandType : Command
        {
            commandHandlers.Add(typeof(TCommandType), command => commandHandler.Handle((TCommandType)command));
        }
    }
}
