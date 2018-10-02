using System;

namespace Kata.SocialNetworking.Infrastructure.Exceptions
{
    public class HandlerNotFoundException : Exception
    {
        public HandlerNotFoundException(string commandName) : base($"No handler found for command {commandName}")
        {
        }
    }
}