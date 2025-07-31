using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class AuthResponseDto
    {
        public UserDto User { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
