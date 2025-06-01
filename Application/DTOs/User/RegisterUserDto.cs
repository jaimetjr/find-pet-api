using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class RegisterUserDto
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
        public string? Avatar { get; set; } = null;
        public string? Phone { get; set; } = null;
        public string? Location { get; set; } = null;
        public string? Bio { get; set; } = null;
        public bool Notifications { get; set; } = false;
    }
}
