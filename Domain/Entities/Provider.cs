using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Provider
    {
        public int Id { get; private set; }
        public Guid UserId { get; private set; }
        public ProviderType Type { get; private set; }
        public string? ProviderKey { get; private set; }  // e.g., Google sub, Facebook id
        public User User { get; private set; }

        private Provider() { }

        public Provider(Guid userId, ProviderType type, string? providerKey)
        {
            UserId = userId;
            Type = type;
            ProviderKey = providerKey;
        }
    }
}
