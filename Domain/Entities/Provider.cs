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
        public string? PasswordHash {  get; private set; }
        public string? PasswordSalt { get; private set; }
        public User User { get; private set; }

        private Provider() { }

        public void SetPassword(string passwordHash, string passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public Provider(Guid userId, ProviderType type, string? providerKey)
        {
            UserId = userId;
            Type = type;
            ProviderKey = providerKey;
        }
    }
}
