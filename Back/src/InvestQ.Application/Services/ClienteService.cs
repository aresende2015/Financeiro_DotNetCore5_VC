using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos;
using InvestQ.Application.Interfaces;
using InvestQ.Data.Interfaces;
using InvestQ.Domain.Entities;

namespace InvestQ.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepo _clienteRepo;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepo clienteRepo,
                              IMapper mapper)
        {
            _clienteRepo = clienteRepo;
            _mapper = mapper;
        }
        public async Task<ClienteDto> AdicionarCliente(int userId, ClienteDto model)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(model);
                cliente.UserId = userId;

                if (cliente.Inativo)
                    throw new Exception("Não é possível incluir um Cliente já inativo.");

                if (await _clienteRepo.GetClienteByCpfAsync(userId, cliente.Cpf, false) != null)
                    throw new Exception("Já existe um Cliente com esse CPF.");

                if( await _clienteRepo.GetClienteByIdAsync(userId, cliente.Id,false) == null)
                {
                    _clienteRepo.Adicionar(cliente);

                    if (await _clienteRepo.SalvarMudancasAsync()) {
                        var retorno = await _clienteRepo.GetClienteByIdAsync(userId, cliente.Id, false);

                        return _mapper.Map<ClienteDto>(retorno);
                    }
                        
                }

                return null;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClienteDto> AtualizarCliente(int userId, int clienteId, ClienteDto model)
        {
            try
            {
                if (clienteId != model.Id)
                    throw new Exception("Está tentando alterar o Id errado.");

                if (model.Inativo)
                    throw new Exception("Não é possível atualizar um Cliente já inativo.");

                var cliente = await _clienteRepo.GetClienteByIdAsync(userId, clienteId, false);
                
                if (cliente != null) 
                {
                    if (cliente.Inativo)
                        throw new Exception("Não se pode alterar um Cliente inativo.");

                    model.Inativo = cliente.Inativo;
                    model.DataDeCriacao = cliente.DataDeCriacao;
                    model.UserId = userId;

                    _mapper.Map(model, cliente);

                    _clienteRepo.Atualizar(cliente);

                    if (await _clienteRepo.SalvarMudancasAsync())
                        return _mapper.Map<ClienteDto>(cliente);
                }

                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<bool> DeletarCliente(int userId, int clienteId)
        {
            var cliente = await _clienteRepo.GetClienteByIdAsync(userId, clienteId, false);

            if (cliente == null)
                throw new Exception("O Cliente que tentou deletar não existe.");

            _clienteRepo.Deletar(cliente);

            return await _clienteRepo.SalvarMudancasAsync();
        }

        public async Task<ClienteDto[]> GetAllClientesAsync(int userId, bool includeCorretora = false)
        {
            try
            {
                var clientes = await _clienteRepo.GetAllClientesAsync(userId, includeCorretora);

                if (clientes == null) return null;

                var RetornoDto = _mapper.Map<ClienteDto[]>(clientes);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClienteDto[]> GetAllClientesByCorretoraAsync(int userId, int corretoraId, bool includeCorretora)
        {
            try
            {
                var clientes = await _clienteRepo.GetAllClientesByCorretoraId(userId, corretoraId, includeCorretora);

                if (clientes == null) return null;

                var RetornoDto = _mapper.Map<ClienteDto[]>(clientes);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClienteDto> GetClienteByIdAsync(int userId, int clienteId, bool includeCorretora = false)
        {
            try
            {
                var cliente = await _clienteRepo.GetClienteByIdAsync(userId, clienteId, includeCorretora);

                if (cliente == null) return null;

                var RetornoDto = _mapper.Map<ClienteDto>(cliente);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarCliente(int userId, ClienteDto model)
        {
            if (model != null)
            {
                var cliente = _mapper.Map<Cliente>(model);

                cliente.Inativar();
                _clienteRepo.Atualizar(cliente);
                return await _clienteRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarCliente(int userId, ClienteDto model)
        {
            if (model != null)
            {
                var cliente = _mapper.Map<Cliente>(model);

                cliente.Reativar();
                _clienteRepo.Atualizar(cliente);
                return await _clienteRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}