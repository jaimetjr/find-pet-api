using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class RegisterUserDto
    {
        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages), ErrorMessageResourceName = "NameRequired")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Resources.ValidationMessages), ErrorMessageResourceName = "FieldLength100")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages), ErrorMessageResourceName = "EmailRequired")]
        [MaxLength(200, ErrorMessageResourceType = typeof(Resources.ValidationMessages), ErrorMessageResourceName = "FieldLength200")]
        public string Email { get; set; } = default!;

        [MaxLength(30, ErrorMessageResourceType = typeof(Resources.ValidationMessages), ErrorMessageResourceName = "FieldLength30")]
        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages), ErrorMessageResourceName = "PhoneRequired")]
        public string Phone { get; set; } = default!;
        public string CPF { get; set; } = default!;

        public string Bio { get; set; } = default!;

        public string Address { get; set; } = default!;
        public string Neighborhood { get; set; } = default!;
        public string CEP { get; set; } = default!;
        public string Number { get; set; } = default!;
        public string Complement { get; set; } = default!;
        public string State { get; set; } = default!;
        public string City { get; set; } = default!;
        public DateTime BirthDate { get; set; } = default!;
        public string ClerkId { get; set; } = default!;
        public ContactType ContactType { get; set; } = default!;

        public bool Notifications { get; set; } = false;

        public ProviderType Provider { get; set; } = default!;
        public IFormFile? Avatar { get; set; } = default!;

    }
}
