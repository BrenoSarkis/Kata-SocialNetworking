using System;
using System.Collections.Generic;
using System.Linq;
using Kata.SocialNetworking.Infrastructure.Exceptions;

namespace Kata.SocialNetworking.Infrastructure
{
    public class Bus
    {
        private readonly Dictionary<Type, Action<ICommand>> commandHandlers = new Dictionary<Type, Action<ICommand>>();
        private readonly List<Tuple<Type, Action<IEvent>>> eventHandlers = new List<Tuple<Type, Action<IEvent>>>();

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

        public void RegisterEventHandlers<TEventType>(params IHandleEventsOf<TEventType>[] handlersToBeRegistered) where TEventType : IEvent
        {
            foreach (var eventHandler in handlersToBeRegistered)
            {
                eventHandlers.Add(new Tuple<Type, Action<IEvent>>(typeof(TEventType), @event => eventHandler.Handle((TEventType)@event)));
            }
        }

        public void Publish<TEventType>(TEventType @event) where TEventType : IEvent
        {
            var everySubscriberOfTheEvent = eventHandlers.Where(handler => handler.Item1 == typeof(TEventType));

            foreach (var subscriber in everySubscriberOfTheEvent)
            {
                subscriber.Item2.Invoke(@event);
            }
        }
    }
}