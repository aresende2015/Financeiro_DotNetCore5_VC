using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;
using InvestQ.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Clientecontroller : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public Clientecontroller(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            try
            {
                 var clientes = await _clienteService.GetAllClientesAsync();

                 if (clientes == null) return NoContent();

                 return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Clientes. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                 var cliente = await _clienteService.GetClienteByIdAsync(id);

                 if (cliente == null) return NoContent();

                 return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Cliente com id ${id}. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Cliente model) 
        {
            try
            {
                 var cliente = await _clienteService.AdicionarCliente(model);
                 if (cliente == null) return NoContent();

                 return Ok(cliente);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar um Cliente. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Cliente model)
        {
            try
            {
                if (model.Id != id) 
                    return StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando atualizar um Cliente errado.");

                 var cliente = await _clienteService.AtualizarCliente(model);
                 if (cliente == null) return NoContent();

                 return Ok(cliente);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar um Cliente com id: ${id}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cliente = await _clienteService.GetClienteByIdAsync(id);
                if (cliente == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar um Cliente que não existe.");

                if(await _clienteService.DeletarCliente(id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar o Cliente.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar deletar o Cliente com id: ${id}. Erro: {ex.Message}");
            }
        }

    }
}