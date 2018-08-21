using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata.SocialNetworking.Infrastructure.Exceptions
{
    public class HandlerAlreadyRegisteredException : Exception
    {
        public HandlerAlreadyRegisteredException(string commandName) : base($"There is handler already registered for {commandName}")
        {
        }
    }
}
