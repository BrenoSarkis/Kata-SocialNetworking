using System;
using System.Collections.Generic;
using Kata.SocialNetworking.Infrastructure.Exceptions;

namespace Kata.SocialNetworking.Infrastructure
{
    public class Bus
    {
        private readonly Dictionary<Type, Action<ICommand>> commandHandlers = new Dictionary<Type, Action<ICommand>>();
        private readonly Dictionary<Type, Action<IEvent>> eventHandlers = new Dictionary<Type, Action<IEvent>>();

        public void SendCommand<TCommandType>(TCommandType command) where TCommandType : ICommand
        {
            Action<ICommand> handler;

            if (commandHandlers.TryGetValue(typeof(TCommandType), out handler))
            {
                handler.Invoke(command);
            }
            else
            {
                throw new HandlerNotFoundException(typeof(TCommandType).Name);
            }

        }

        public void RegisterCommandHandler<TCommandType>(IHandleCommandsOf<TCommandType> commandHandler) where TCommandType : ICommand
        {
            commandHandlers.Add(typeof(TCommandType), command => commandHandler.Handle((TCommandType)command));
        }

        public void RegisterEventHandlers<TEventType>(IHandleEventsOf<TEventType> eventHandler) where TEventType : IEvent
        {
            eventHandlers.Add(typeof(TEventType), @event => eventHandler.Handle((TEventType)@event));

        }

        public void Publish<TEventType>(TEventType @event) where TEventType : IEvent
        {
            eventHandlers[typeof(TEventType)].Invoke(@event);
        }
    }
}