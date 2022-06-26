using System;

namespace InvestQ.Domain.Entities.Clientes
{
    public class Carteira
    {
        public Carteira() { }
        public Carteira(Guid clienteId, 
                        Guid corretoraId) 
        {
            this.ClienteId = clienteId;
            this.CorretoraId = corretoraId;   
        }
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public Guid CorretoraId { get; set; }
        public Corretora Corretora { get; set; }
    }
}