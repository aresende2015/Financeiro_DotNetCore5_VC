using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;
using InvestQ.Domain.Interfaces.Repositories;
using InvestQ.Domain.Interfaces.Services;

namespace InvestQ.Domain.Service
{
    public class CorretoraService : ICorretoraService
    {
        private readonly ICorretoraRepo _corretoraRepo;

        public CorretoraService(ICorretoraRepo corretoraRepo)
        {
            _corretoraRepo = corretoraRepo;
        }
        public async Task<Corretora> AdicionarCorretora(Corretora model)
        {
            if (model.Inativo)
                throw new Exception("Não é possível incluir uma Corretora já inativa.");

            if (await _corretoraRepo.GetCorretoraByDescricaoAsync(model.Descricao) != null)
                throw new Exception("Já existe uma Corretora com essa descrição.");

            if( await _corretoraRepo.GetCorretoraByIdAsync(model.Id) == null)
            {
                _corretoraRepo.Adicionar(model);
                if (await _corretoraRepo.SalvarMudancasAsync())
                    return model;
            }

            return null;
        }

        public async Task<Corretora> AtualizarCorretora(Corretora model)
        {
            if (model.Inativo)
                throw new Exception("Não é possível atualizar uma Corretora já inativa.");

            var corretora = await _corretoraRepo.GetCorretoraByIdAsync(model.Id);

            if (corretora != null)
            {
                if (corretora.Inativo)
                    throw new Exception("Não se pode alterar uma Corretora inativa.");

                _corretoraRepo.Atualizar(model);

                if (await _corretoraRepo.SalvarMudancasAsync())
                    return model;
            }

            return null;
        }

        public async Task<bool> DeletarCorretora(int corretoraId)
        {
            var corretora = await _corretoraRepo.GetCorretoraByIdAsync(corretoraId);

            if (corretora == null)
                throw new Exception("A Corretora que tentou deletar não existe.");

            _corretoraRepo.Deletar(corretora);

            return await _corretoraRepo.SalvarMudancasAsync();
        }

        public async Task<Corretora[]> GetAllCorretorasAsync()
        {
            try
            {
                var corretoras = await _corretoraRepo.GetAllCorretorasAsync(true);

                if (corretoras == null) return null;

                return corretoras;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Corretora> GetCorretoraByIdAsync(int corretoraId)
        {
            try
            {
                var corretora = await _corretoraRepo.GetCorretoraByIdAsync(corretoraId);

                if (corretora == null) return null;

                return corretora;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarCorretora(Corretora model)
        {
            if (model != null)
            {
                model.Inativar();
                _corretoraRepo.Atualizar(model);
                return await _corretoraRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarCorretora(Corretora model)
        {
            if (model != null)
            {
                model.Reativar();
                _corretoraRepo.Atualizar(model);
                return await _corretoraRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}