using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos;
using InvestQ.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TipoDeAtivoController : ControllerBase
    {
        private readonly ITipoDeAtivoService _tipoDeAtivoService;

        public TipoDeAtivoController(ITipoDeAtivoService tipoDeAtivoService)
        {
            _tipoDeAtivoService = tipoDeAtivoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            try
            {
                 var tiposDeAtivos = await _tipoDeAtivoService.GetAllTiposDeAtivosAsync(true);

                 if (tiposDeAtivos == null) return NoContent();

                 return Ok(tiposDeAtivos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Tipos de Ativos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoDeAtivoById(int id)
        {
            try
            {
                 var tipoDeAtivo = await _tipoDeAtivoService.GetTipoDeAtivoByIdAsync(id, true);

                 if (tipoDeAtivo == null) return NoContent();

                 return Ok(tipoDeAtivo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Tipo de Ativo com id ${id}. Erro: {ex.Message}");
            }
        }

        [HttpGet("{descricao}/descricao")]
        public async Task<IActionResult> GetTipoDeAtivoByDescricao(string descricao)
        {
            try
            {
                 var tipoDeAtivo = await _tipoDeAtivoService.GetTipoDeAtivoByDescricaoAsync(descricao, true);

                 if (tipoDeAtivo == null) return NoContent();

                 return Ok(tipoDeAtivo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Tipo de Ativo com a ${descricao}. Erro: {ex.Message}");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(TipoDeAtivoDto model) 
        {
            try
            {
                 var tipoDeAtivo = await _tipoDeAtivoService.AdicionarTipoDeAtivo(model);
                 if (tipoDeAtivo == null) return BadRequest("Erro ao tentar adicionar o Tipo de Ativo.");

                 return Ok(tipoDeAtivo);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar o Tipo de Ativo. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TipoDeAtivoDto model)
        {
            try
            {
                if (model.Id != id) 
                    return StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando atualizar o Tipo de Ativo errado.");

                 var tipoDeAtivo = await _tipoDeAtivoService.AtualizarTipoDeAtivo(model);
                 if (tipoDeAtivo == null) return NoContent();

                 return Ok(tipoDeAtivo);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar o Tipo de Ativo com id: ${id}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var tipoDeAtivo = await _tipoDeAtivoService.GetTipoDeAtivoByIdAsync(id, false);
                if (tipoDeAtivo == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar um Tipo de Ativo que não existe.");

                if(await _tipoDeAtivoService.DeletarTipoDeAtivo(id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar o Tipo de Ativo.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar deletar o Tipo de Ativo com id: ${id}. Erro: {ex.Message}");
            }
        }
    }
}