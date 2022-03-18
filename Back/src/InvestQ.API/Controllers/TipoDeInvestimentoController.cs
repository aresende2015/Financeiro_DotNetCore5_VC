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
    public class TipoDeInvestimentoController : ControllerBase
    {
        private readonly ITipoDeInvestimentoService _tipoDeInvestimentoService;
        public TipoDeInvestimentoController(ITipoDeInvestimentoService tipoDeInvestimentoService)
        {
            _tipoDeInvestimentoService = tipoDeInvestimentoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            try
            {
                 var tiposDeInvestimentos = await _tipoDeInvestimentoService.GetAllTiposDeInvestimentosAsync();

                 if (tiposDeInvestimentos == null) return NoContent();

                 return Ok(tiposDeInvestimentos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Tipos de Investimentos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoDeInvestimentoById(Guid id)
        {
            try
            {
                 var tipoDeInvestimento = await _tipoDeInvestimentoService.GetTipoDeInvestimentoByIdAsync(id);

                 if (tipoDeInvestimento == null) return NoContent();

                 return Ok(tipoDeInvestimento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Tipo de Investimento com id ${id}. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(TipoDeInvestimentoDto model) 
        {
            try
            {
                 var tipoDeInvestimento = await _tipoDeInvestimentoService.AdicionarTipoDeInvestimento(model);
                 if (tipoDeInvestimento == null) return BadRequest("Erro ao tentar adicionar o Tipo de Investimento.");

                 return Ok(tipoDeInvestimento);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar o Tipo de Investimento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, TipoDeInvestimentoDto model)
        {
            try
            {
                if (model.Id != id) 
                    return StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando atualizar o Tipo de Investimento errado.");

                 var tipoDeInvestimento = await _tipoDeInvestimentoService.AtualizarTipoDeInvestimento(model);
                 if (tipoDeInvestimento == null) return NoContent();

                 return Ok(tipoDeInvestimento);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar o Tipo de Investimento com id: ${id}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var tipoDeInvestimento = await _tipoDeInvestimentoService.GetTipoDeInvestimentoByIdAsync(id);
                if (tipoDeInvestimento == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar um SegmentoAnbima que não existe.");

                if(await _tipoDeInvestimentoService.DeletarTipoDeInvestimento(id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar o Tipo de Investimento.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar deletar o Tipo de Investimento com id: ${id}. Erro: {ex.Message}");
            }
        }
    }
}