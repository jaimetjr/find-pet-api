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

        public void UpdateProfile(string? avatar, string phone, string bio, DateTime birthDate, string cpf, string address, string neighborhood, string cep, string state, string city, string? complement, string number, bool notifications, ContactType contactType)
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
            ContactType = contactType;
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
        public string? Complement { get; private set; }
        public string? ExpoPushToken { get; private set; }
        public ContactType ContactType { get; private set; }
        public ApprovalStatus ApprovalStatus { get; private set; }
        public UserRole Role { get; private set; } = UserRole.User;
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public ICollection<Pet> Pets { get; private set; }
        public ICollection<Provider> Providers { get; private set; }
        public ICollection<AdoptionRequest> OwnerAdoptionRequests { get; private set; }
        public ICollection<AdoptionRequest> AdopterAdoptionRequests { get; private set; }
        public ICollection<Notification> Notification { get; private set; }

        private User() { }

        // Factory/constructor (customize as needed)
        public User(string email, string name, string phone, bool notifications, string cpf)
        {
            Id = Guid.NewGuid();
            Email = email;
            Name = name;
            Phone = phone;
            CPF = cpf;
            Notifications = notifications;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            ApprovalStatus = ApprovalStatus.Pending;
            Providers = new List<Provider>();
            OwnerAdoptionRequests = new List<AdoptionRequest>();
            AdopterAdoptionRequests = new List<AdoptionRequest>();
            Notification = new List<Notification>();
        }

        public void SetAdditionalInfo(string phone, string bio, string clerkId, ContactType contactType, DateTime birthDate)
        {
            Phone = phone;
            Bio = bio;
            ClerkId = clerkId;
            ContactType = contactType;
            BirthDate = birthDate;
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

        public void SetAvatar(string avatar)
        {
            Avatar = avatar;
        }

        public void SetRole(UserRole role)
        {
            Role = role;
        }

        public void SetPushToken(string expoPushToken)
        {
            ExpoPushToken = expoPushToken;
        }
    }
}
