using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTO
{
    public class DTOLogin
    {
        public class LoginResponse
        {
            public string UserId { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public bool IsAdmin { get; set; }
        }
    }
}
