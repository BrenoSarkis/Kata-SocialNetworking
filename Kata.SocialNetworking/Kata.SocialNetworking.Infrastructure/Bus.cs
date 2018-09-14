using System;
using System.Collections.Generic;
using Kata.SocialNetworking.Boundaries.Messaging;
using Kata.SocialNetworking.Infrastructure.Exceptions;

namespace Kata.SocialNetworking.Infrastructure
{
    public class Bus : IBus
    {
        private readonly Dictionary<Type, List<Action<IMessage>>> messageHandlers = new Dictionary<Type, List<Action<IMessage>>>();

        public void RegisterHandlers<TMessage>(params IHandleMessagesOf<TMessage>[] handlers) where TMessage : IMessage
        {
            foreach (var handler in handlers)
            {
                var aHandlerForThisMessageHasBeenRegistered = messageHandlers.ContainsKey(typeof(TMessage));

                if (aHandlerForThisMessageHasBeenRegistered)
                {
                    var isACommandHandler = typeof(ICommand).IsAssignableFrom(typeof(TMessage));

                    if (isACommandHandler)
                    {
                        throw new HandlerAlreadyRegisteredException(typeof(TMessage).Name);
                    }

                    messageHandlers[typeof(TMessage)].Add(message => handler.Handle((TMessage)message));
                }
                else
                {
                    messageHandlers.Add(typeof(TMessage), new List<Action<IMessage>>{ message => handler.Handle((TMessage)message) });
                }
            }
        }

        public void SendCommand<TCommandType>(TCommandType command) where TCommandType : ICommand
        {
            List<Action<IMessage>> handlers;

            if (messageHandlers.TryGetValue(typeof(TCommandType), out handlers))
            {
                handlers[0].Invoke(command);
            }
            else
            {
                throw new HandlerNotFoundException(typeof(TCommandType).Name);
            }
        }

        public void Publish<TEventType>(TEventType @event) where TEventType : IEvent
        {

            List<Action<IMessage>> subscribers;

            if (messageHandlers.TryGetValue(typeof(TEventType), out subscribers))

            foreach (var subscriber in subscribers)
            {
                subscriber(@event);
            }
        }
    }
}