using System;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos.Ativos;
using InvestQ.Application.Interfaces.Ativos;
using InvestQ.Data.Interfaces.Ativos;
using InvestQ.Domain.Entities.Ativos;

namespace InvestQ.Application.Services.Ativos
{
    public class ProventoService : IProventoService
    {
        private readonly IProventoRepo _proventoRepo;
        private readonly IMapper _mapper;

        public ProventoService(IProventoRepo proventoRepo,
                                  IMapper mapper)
        {
            _proventoRepo = proventoRepo;
            _mapper = mapper;
        } 
        public async Task<ProventoDto> AdicionarProvento(ProventoDto model)
        {
            var provento = _mapper.Map<Provento>(model);

            if( await _proventoRepo.GetProventoByIdAsync(provento.Id) == null)
            {
                _proventoRepo.Adicionar(provento);

                if (await _proventoRepo.SalvarMudancasAsync())
                {
                    var retorno = await _proventoRepo.GetProventoByIdAsync(provento.Id);

                    return _mapper.Map<ProventoDto>(retorno);
                }
            }

            return null;
        }

        public async Task<ProventoDto> AtualizarProvento(ProventoDto model)
        {
            try
            {
                var provento = await _proventoRepo.GetProventoByIdAsync(model.Id);

                if (provento != null)
                {
                    if (provento.Inativo)
                        throw new Exception("Não se pode alterar um Provento inativo.");

                    _mapper.Map(model, provento);

                    _proventoRepo.Atualizar(provento);

                    if (await _proventoRepo.SalvarMudancasAsync())
                        return _mapper.Map<ProventoDto>(provento);
                }

                return null;
            }
            catch (Exception ex)
            {                
               throw new Exception(ex.Message);
            } 
        }

        public async Task<bool> DeletarProvento(Guid proventoId)
        {
            var provento = await _proventoRepo.GetProventoByIdAsync(proventoId);

            if (provento == null)
                throw new Exception("O Provento que tentou deletar não existe.");

            _proventoRepo.Deletar(provento);

            return await _proventoRepo.SalvarMudancasAsync();
        }

        public async Task<ProventoDto[]> GetAllProventosByAtivoIdAsync(Guid ativoId)
        {
            try
            {
                var tiposDeAtivos = await _proventoRepo.GetAllProventosByAtivoIdAsync(ativoId);

                if (tiposDeAtivos == null) return null;

                return _mapper.Map<ProventoDto[]>(tiposDeAtivos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProventoDto> GetProventoByIdAsync(Guid id)
        {
            try
            {
                var provento = await _proventoRepo.GetProventoByIdAsync(id);

                if (provento == null) return null;

                return _mapper.Map<ProventoDto>(provento);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarProvento(ProventoDto model)
        {
            if (model != null)
            {
                var provento = _mapper.Map<Provento>(model);

                provento.Inativar();
                _proventoRepo.Atualizar(provento);
                return await _proventoRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarProvento(ProventoDto model)
        {
            if (model != null)
            {
                var provento = _mapper.Map<Provento>(model);

                provento.Reativar();
                _proventoRepo.Atualizar(provento);
                return await _proventoRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}