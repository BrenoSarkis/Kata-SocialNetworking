using System;

namespace Kata.SocialNetworking.Infrastructure.Exceptions
{
    public class HandlerAlreadyRegisteredException : Exception
    {
        public HandlerAlreadyRegisteredException(string commandName) : base($"There is handler already registered for {commandName}")
        {
        }
    }
}
