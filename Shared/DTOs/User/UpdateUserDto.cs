using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UpdateUserDto
    {       
        public string? Avatar { get; set; } = default!;
        public string? Phone { get; set; } = default!;
        public string? Bio { get; set; } = default!;
        public DateTime BirthDate { get; set; } = default!;
        public string CPF { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Neighborhood { get; set; } = default!;
        public string CEP { get; set; } = default!;
        public string State { get; set; } = default!;
        public string City { get; set; } = default!;
        public bool Notifications { get; set; } = default!;
        public string Number { get; set; } = default!;
        public string Complement { get; set; } = default!;
        public ContactType ContactType { get; set; } = default!;
    }
}
