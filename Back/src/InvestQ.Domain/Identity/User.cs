using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Enum;
using Microsoft.AspNetCore.Identity;

namespace InvestQ.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        //public string NomeCompleto { get() {}  }
        public string Descricao { get; set; }
        public string ImagemURL { get; set; }
        public Funcao Funcao { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}