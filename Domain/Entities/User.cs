using Domain.Enums;
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
        public string ClerkId { get; private set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        // Pseudocode plan:
        // 1. Ensure all DateTime properties are always set with DateTimeKind.Utc.
        // 2. In all constructors and setters, explicitly set DateTime to UTC.
        // 3. Add a private setter for BirthDate that enforces UTC.
        // 4. Update all usages to ensure UTC is enforced.

        public DateTime BirthDate
        {
            get => _birthDate;
            private set => _birthDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
        private DateTime _birthDate;

        // Update SetPersonalInfo and UpdateProfile to use the property setter:
        public void SetPersonalInfo(DateTime birthDate, string cpf)
        {
            BirthDate = birthDate;
            CPF = cpf;
        }

        public void UpdateProfile(string? avatar, string phone, string bio, DateTime birthDate, string cpf, string address, string neighborhood, string cep, string state, string city, string complement, string number, bool notifications)
        {
            Avatar = avatar;
            Phone = phone;
            Bio = bio;
            BirthDate = birthDate;
            CPF = cpf;
            Address = address;
            Neighborhood = neighborhood;
            CEP = cep;
            State = state;
            City = city;
            Complement = complement;
            Number = number;
            Notifications = notifications;
            UpdatedAt = DateTime.UtcNow;
        }
        public string CPF { get; private set; }
        public string? Avatar { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
        public string Neighborhood { get; private set; }
        public string CEP { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }
        public string Bio { get; private set; }
        public bool Notifications { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; } 
        public ApprovalStatus ApprovalStatus { get; private set; }
        public UserRole Role { get; private set; } = UserRole.User;
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public ICollection<Pet> Pets { get; private set; }
        public ICollection<Provider> Providers { get; private set; }

        private User() { }

        // Factory/constructor (customize as needed)
        public User(string email, string name, string phone, bool notifications)
        {
            Id = Guid.NewGuid();
            Email = email;
            Name = name;
            Phone = phone;
            Notifications = notifications;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            ApprovalStatus = ApprovalStatus.Pending;
            Providers = new List<Provider>();
        }

        public void SetAdditionalInfo(string? avatar, string phone, string bio, string clerkId)
        {
            Avatar = avatar;
            Phone = phone;
            Bio = bio;
            ClerkId = clerkId;
        }

        public void SetAddressInformation(string address, string neighborhood, string cep, string state, string city, string complement,string number)
        {
            Address = address;
            Neighborhood = neighborhood;
            CEP = cep;
            State = state;
            City = city;
            Complement = complement;
            Number = number;
        }

        public void SetRole(UserRole role)
        {
            Role = role;
        }
    }
}
