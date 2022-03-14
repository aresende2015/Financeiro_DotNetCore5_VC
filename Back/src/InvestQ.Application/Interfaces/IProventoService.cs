using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos;

namespace InvestQ.Application.Interfaces
{
    public interface IProventoService
    {
        Task<ProventoDto> AdicionarProvento(ProventoDto model);
        Task<ProventoDto> AtualizarProvento(ProventoDto model);
        Task<bool> DeletarProvento(int proventoId);
        
        Task<bool> InativarProvento(ProventoDto model);
        Task<bool> ReativarProvento(ProventoDto model);

        Task<ProventoDto[]> GetAllProventosByAtivoIdAsync(int ativoId);
        Task<ProventoDto> GetProventoByIdAsync(int Id);
    }
}