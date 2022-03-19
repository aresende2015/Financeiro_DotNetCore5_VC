using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Application.Dtos.Clientes
{
    public class ClienteCorretoraDto
    {
        public Guid ClienteId { get; set; }
        public ClienteDto Cliente { get; set; }
        public Guid CorretoraId { get; set; }
        public CorretoraDto Corretora { get; set; }
    }
}