using System;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Ativos;
using InvestQ.Domain.Enum;

namespace InvestQ.Data.Interfaces.Ativos
{
    public interface IAtivoRepo : IGeralRepo
    {
        Task<Ativo[]> GetAllAtivosByTipoDeAtivoAsync(TipoDeAtivo tipoDeAtivo);
        Task<Ativo> GetAtivoByTipoDeAtivoDescricaoAsync(TipoDeAtivo tipoDeAtivo, string descricao);
        Task<Ativo> GetAtivoByTesouroDiretoIdAsync(Guid tesouroDiretoId);
        Task<Ativo> GetAtivoByFundoImobiliarioIdAsync(Guid fundoImobiliarioId);
        Task<Ativo> GetAtivoByIdAsync(Guid id);
        Task<Ativo> GetAtivoByIdsAsync(Guid id, TipoDeAtivo tipoDeAtivo);
    }
}