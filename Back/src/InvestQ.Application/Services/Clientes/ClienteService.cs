using System;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos.Clientes;
using InvestQ.Application.Interfaces.Clientes;
using InvestQ.Data.Interfaces.Clientes;
using InvestQ.Data.Paginacao;
using InvestQ.Domain.Entities.Clientes;

namespace InvestQ.Application.Services.Clientes
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

        public async Task<ClienteDto> AtualizarCliente(int userId, Guid clienteId, ClienteDto model)
        {
            try
            {
                if (clienteId != model.Id)
                    throw new Exception("Está tentando alterar o Id errado.");

                var cliente = await _clienteRepo.GetClienteByIdAsync(userId, clienteId, false);
                
                if (cliente != null) 
                {
                    if (cliente.Inativo)
                        throw new Exception("Não se pode alterar um Cliente inativo.");

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

        public async Task<bool> DeletarCliente(int userId, Guid clienteId)
        {
            var cliente = await _clienteRepo.GetClienteByIdAsync(userId, clienteId, false);

            if (cliente == null)
                throw new Exception("O Cliente que tentou deletar não existe.");

            _clienteRepo.Deletar(cliente);

            return await _clienteRepo.SalvarMudancasAsync();
        }

        public async Task<PageList<ClienteDto>> GetAllClientesAsync(int userId, PageParams pageParams, bool includeCorretora = false)
        {
            try
            {
                var clientes = await _clienteRepo.GetAllClientesAsync(userId, pageParams, includeCorretora);

                if (clientes == null) return null;

                var RetornoDto = _mapper.Map<PageList<ClienteDto>>(clientes);

                RetornoDto.CurrentPage = clientes.CurrentPage;
                RetornoDto.TotalPages = clientes.TotalPages;
                RetornoDto.PageSize = clientes.PageSize;
                RetornoDto.TotalCount = clientes.TotalCount;

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<ClienteDto>> GetAllClientesByCorretoraAsync(int userId, PageParams pageParams, Guid corretoraId, bool includeCorretora)
        {
            try
            {
                var clientes = await _clienteRepo.GetAllClientesByCorretoraId(userId, pageParams, corretoraId, includeCorretora);

                if (clientes == null) return null;

                var RetornoDto = _mapper.Map<PageList<ClienteDto>>(clientes);

                RetornoDto.CurrentPage = clientes.CurrentPage;
                RetornoDto.TotalPages = clientes.TotalPages;
                RetornoDto.PageSize = clientes.PageSize;
                RetornoDto.TotalCount = clientes.TotalCount;

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClienteDto> GetClienteByIdAsync(int userId, Guid clienteId, bool includeCorretora = false)
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