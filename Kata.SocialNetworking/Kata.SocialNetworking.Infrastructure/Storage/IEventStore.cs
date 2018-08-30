using System;
using Kata.SocialNetworking.Infrastructure.Messaging;

namespace Kata.SocialNetworking.Infrastructure.Storage
{
    public interface IEventStore
    {
        void Save(Guid aggregateId, IEvent @event);
    }
}
