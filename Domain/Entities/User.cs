using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string? Avatar { get; private set; }
        public string? Phone { get; private set; }
        public string? Location { get; private set; }
        public string? Bio { get; private set; }
        public bool Notifications { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public ApprovalStatus ApprovalStatus { get; private set; }

        public ICollection<Provider> Providers { get; private set; }

        private User() { }

        // Factory/constructor (customize as needed)
        public User(string email, string name, bool notifications)
        {
            Id = Guid.NewGuid();
            Email = email;
            Name = name;
            Notifications = notifications;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            ApprovalStatus = ApprovalStatus.Pending;
            Providers = new List<Provider>();
        }

        public void SetAdditionalInfo(string? avatar, string? phone, string? location, string? bio)
        {
            Avatar = avatar;
            Phone = phone;
            Location = location;
            Bio = bio;
        }
    }
}
