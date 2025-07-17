using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string ClerkId { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Avatar { get; set; }
        public string? Phone { get; set; }
        public string? Bio { get; set; }
        public DateTime BirthDate { get; set; }
        public string CPF { get; set; }
        public string Address { get; set; }
        public string Neighborhood { get; set; }
        public string CEP { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public bool Notifications { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public UserRole Role { get; set; }
        public ContactType ContactType { get; set; }

        public UserDto() { }

        public UserDto(Domain.Entities.User user)
        {
            ClerkId = user.ClerkId;
            Name = user.Name;
            Avatar = user.Avatar;
        }
    }
}
