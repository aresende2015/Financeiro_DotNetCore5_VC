using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos.Acoes;
using InvestQ.Application.Interfaces.Acoes;
using InvestQ.Data.Interfaces.Acoes;
using InvestQ.Domain.Entities.Acoes;

namespace InvestQ.Application.Services.Acoes
{
    public class SubsetorService : ISubsetorService
    {
        private readonly ISubsetorRepo _subsetorRepo;
        private readonly IMapper _mapper;

        public SubsetorService(ISubsetorRepo subsetorRepo,
                               IMapper mapper)
        {
            _subsetorRepo = subsetorRepo;
            _mapper = mapper;
        }
        
        public async Task AddSubsetor(Guid setorId, SubsetorDto model)
        {
            try
            {
                var subsetor = _mapper.Map<Subsetor>(model);

                subsetor.SetorId = setorId;
                
                _subsetorRepo.Adicionar(subsetor);

                await _subsetorRepo.SalvarMudancasAsync();
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<SubsetorDto[]> SalvarSubsetores(Guid setorId, SubsetorDto[] models)
        {
            try
            {
                var subsetores = await _subsetorRepo.GetSubsetoresBySetorIdAsync(setorId);
                
                if (subsetores != null) 
                {

                    foreach (var model in models)
                    {
                        if (model.Id == Guid.Empty) 
                        {
                            await AddSubsetor(setorId, model);
                        }
                        else
                        {
                            var subsetor = subsetores.FirstOrDefault(s => s.Id == model.Id);
                            
                            model.SetorId = setorId;

                            _mapper.Map(model, subsetor);

                            _subsetorRepo.Atualizar(subsetor);

                            await _subsetorRepo.SalvarMudancasAsync();
                        }
                    }

                    var subsetorRetorno = await _subsetorRepo.GetSubsetoresBySetorIdAsync(setorId);

                    return _mapper.Map<SubsetorDto[]>(subsetorRetorno);

                }

                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletarSubsetor(Guid setorId, Guid subsetorId)
        {
            try
            {
                var subsetor = await _subsetorRepo.GetSubsetorByIdsAsync(setorId, subsetorId);

                if (subsetor == null)
                    throw new Exception("O Subsetor que tentou deletar não existe.");

                _subsetorRepo.Deletar(subsetor);

                return await _subsetorRepo.SalvarMudancasAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<SubsetorDto[]> GetSubsetoresBySetorIdAsync(Guid setorId)
        {
            try
            {
                var subsetores = await _subsetorRepo.GetSubsetoresBySetorIdAsync(setorId);

                if (subsetores == null) return null;

                var RetornoDto = _mapper.Map<SubsetorDto[]>(subsetores);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SubsetorDto> GetSubsetorByIdsAsync(Guid setorId, Guid subsetorId)
        {
            try
            {
                var subsetor = await _subsetorRepo.GetSubsetorByIdsAsync(setorId, subsetorId);

                if (subsetor == null) return null;

                var RetornoDto = _mapper.Map<SubsetorDto>(subsetor);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SubsetorDto> GetSubsetorByIdAsync(Guid subsetorId)
        {
            try
            {
                var subsetor = await _subsetorRepo.GetSubsetorByIdAsync(subsetorId);

                if (subsetor == null) return null;

                var RetornoDto = _mapper.Map<SubsetorDto>(subsetor);

                return RetornoDto;

            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
    }
}