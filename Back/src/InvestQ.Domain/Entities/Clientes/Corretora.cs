using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestQ.Domain.Entities.Clientes
{
    public class Corretora : Entity
    {
        public Corretora() 
        {
        }
        public Corretora(Guid id, 
                         string descricao,
                         string imagem)
        {
            Id = id;
            Descricao = descricao;
            Imagen = imagem;
        }
        public void Inativar()
        {
            if (Inativo)
                Inativo = true;
            else
                throw new Exception($"A Corretora já estava inativa.");
        }
        public void Reativar()
        {
            if (!Inativo)
                Inativo = false;
            else
                throw new Exception($"A Corretora já estava ativa.");
        }
        public string Descricao { get; set; }
        public string Imagen { get; set; }
        public IEnumerable<Carteira> Carteiras { get; set; }
    }
}