using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Interfaces;
using InvestQ.Data.Interfaces;
using InvestQ.Domain.Entities;

namespace InvestQ.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepo _clienteRepo;

        public ClienteService(IClienteRepo clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }
        public async Task<Cliente> AdicionarCliente(Cliente model)
        {
            try
            {
                if (model.Inativo)
                    throw new Exception("Não é possível incluir um Cliente já inativo.");

                if (await _clienteRepo.GetClienteByCpfAsync(model.Cpf, false) != null)
                    throw new Exception("Já existe um Cliente com esse CPF.");

                if( await _clienteRepo.GetClienteByIdAsync(model.Id,false) == null)
                {
                    _clienteRepo.Adicionar(model);
                    if (await _clienteRepo.SalvarMudancasAsync())
                        return await _clienteRepo.GetClienteByIdAsync(model.Id, false);                    
                }

                return null;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }            
        }

        public async Task<Cliente> AtualizarCliente(Cliente model)
        {
            if (model.Inativo)
                throw new Exception("Não é possível atualizar um Cliente já inativo.");

            var cliente = await _clienteRepo.GetClienteByIdAsync(model.Id, false);

            if (cliente != null)
            {
                if (cliente.Inativo)
                    throw new Exception("Não se pode alterar um Cliente inativo.");

                _clienteRepo.Atualizar(model);

                if (await _clienteRepo.SalvarMudancasAsync())
                    return model;
            }

            return null;
        }

        public async Task<bool> DeletarCliente(int clienteId)
        {
            var cliente = await _clienteRepo.GetClienteByIdAsync(clienteId, false);

            if (cliente == null)
                throw new Exception("O Cliente que tentou deletar não existe.");

            _clienteRepo.Deletar(cliente);

            return await _clienteRepo.SalvarMudancasAsync();
        }

        public async Task<Cliente[]> GetAllClientesAsync(bool includeCorretora = false)
        {
            try
            {
                var clientes = await _clienteRepo.GetAllClientesAsync(includeCorretora);

                if (clientes == null) return null;

                return clientes;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cliente[]> GetAllClientesByCorretoraAsync(int corretoraId, bool includeCorretora)
        {
            try
            {
                var clientes = await _clienteRepo.GetAllClientesByCorretoraId(corretoraId, includeCorretora);

                if (clientes == null) return null;

                return clientes;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cliente> GetClienteByIdAsync(int clienteId, bool includeCorretora = false)
        {
            try
            {
                var cliente = await _clienteRepo.GetClienteByIdAsync(clienteId, includeCorretora);

                if (cliente == null) return null;

                return cliente;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarCliente(Cliente model)
        {
            if (model != null)
            {
                model.Inativar();
                _clienteRepo.Atualizar(model);
                return await _clienteRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarCliente(Cliente model)
        {
            if (model != null)
            {
                model.Reativar();
                _clienteRepo.Atualizar(model);
                return await _clienteRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}