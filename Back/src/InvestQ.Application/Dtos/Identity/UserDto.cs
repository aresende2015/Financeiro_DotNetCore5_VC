using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Enum;

namespace InvestQ.Application.Dtos.Identity
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public TipoDeUsuarioDto Funcao { get; set; }
    }
}