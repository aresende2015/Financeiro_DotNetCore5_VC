using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Enum;

namespace InvestQ.Application.Dtos.Identity
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public TipoDeUsuarioDto Funcao { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}