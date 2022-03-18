using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Application.Dtos.Identity
{
    public class UserLoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}