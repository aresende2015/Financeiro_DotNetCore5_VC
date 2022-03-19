using System;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos.Acoes;
using InvestQ.Application.Interfaces.Acoes;
using InvestQ.Data.Interfaces.Acoes;
using InvestQ.Domain.Entities.Acoes;

namespace InvestQ.Application.Services.Acoes
{
    public class SetorService : ISetorService
    {
        private readonly ISetorRepo _setorRepo;
        private readonly IMapper _mapper;

        public SetorService(ISetorRepo setorRepo,
                            IMapper mapper)
        {
            _setorRepo = setorRepo;
            _mapper = mapper;
        }
        public async Task<SetorDto> AdicionarSetor(SetorDto model)
        {
            try
            {
                var setor = _mapper.Map<Setor>(model);

                if (setor.Inativo)
                    throw new Exception("Não é possível incluir um Setor já inativo.");

                if (await _setorRepo.GetSetorByDescricaoAsync(setor.Descricao, false) != null)
                    throw new Exception("Já existe um Setor com esse CPF.");

                if( await _setorRepo.GetSetorByIdAsync(setor.Id,false) == null)
                {
                    _setorRepo.Adicionar(setor);

                    if (await _setorRepo.SalvarMudancasAsync()) {
                        var retorno = await _setorRepo.GetSetorByIdAsync(setor.Id, false);

                        return _mapper.Map<SetorDto>(retorno);
                    }
                        
                }

                return null;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<SetorDto> AtualizarSetor(int setorId, SetorDto model)
        {
            try
            {
                if (setorId != model.Id)
                    throw new Exception("Está tentando alterar o Id errado.");

                if (model.Inativo)
                    throw new Exception("Não é possível atualizar um Setor já inativo.");

                var setor = await _setorRepo.GetSetorByIdAsync(setorId, false);
                
                if (setor != null) 
                {
                    if (setor.Inativo)
                        throw new Exception("Não se pode alterar um Setor inativo.");

                    model.Inativo = setor.Inativo;
                    model.DataDeCriacao = setor.DataDeCriacao;

                    _mapper.Map(model, setor);

                    _setorRepo.Atualizar(setor);

                    if (await _setorRepo.SalvarMudancasAsync())
                        return _mapper.Map<SetorDto>(setor);
                }

                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletarSetor(int setorId)
        {
            var setor = await _setorRepo.GetSetorByIdAsync(setorId, false);

            if (setor == null)
                throw new Exception("O Setor que tentou deletar não existe.");

            _setorRepo.Deletar(setor);

            return await _setorRepo.SalvarMudancasAsync();
        }

        public async Task<SetorDto[]> GetAllSetoresAsync(bool includeSubsetor)
        {
            try
            {
                var setores = await _setorRepo.GetAllSetoresAsync(includeSubsetor);

                if (setores == null) return null;

                var RetornoDto = _mapper.Map<SetorDto[]>(setores);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SetorDto> GetSetorByIdAsync(int setorId, bool includeSubsetor)
        {
            try
            {
                var setor = await _setorRepo.GetSetorByIdAsync(setorId, includeSubsetor);

                if (setor == null) return null;

                var RetornoDto = _mapper.Map<SetorDto>(setor);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarSetor(SetorDto model)
        {
            if (model != null)
            {
                var setor = _mapper.Map<Setor>(model);

                setor.Inativar();
                _setorRepo.Atualizar(setor);
                return await _setorRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarSetor(SetorDto model)
        {
            if (model != null)
            {
                var setor = _mapper.Map<Setor>(model);

                setor.Reativar();
                _setorRepo.Atualizar(setor);
                return await _setorRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}