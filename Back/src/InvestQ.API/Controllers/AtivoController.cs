using System;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Ativos;
using InvestQ.Application.Dtos.Enum;
using InvestQ.Application.Interfaces.Ativos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AtivoController : ControllerBase
    {
        private readonly IAtivoService _ativoService;

        public AtivoController(IAtivoService ativoService)
        {
            _ativoService = ativoService;
        }

        [HttpGet("{tipoDeAtivoDto}/tipodeativodto")]
        public async Task<IActionResult> GetAllAtivosByTipoDeAtivo(TipoDeAtivoDto tipoDeAtivoDto)
        {
            try
            {
                 var ativos = await _ativoService.GetAllAtivosByTipoDeAtivoAsync(tipoDeAtivoDto);

                 if (ativos == null) return NoContent();

                 return Ok(ativos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar os Ativos do tipo ${tipoDeAtivoDto}. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(AtivoDto model) 
        {
            try
            {
                 var ativo = await _ativoService.AdicionarAtivo(model);
                 if (ativo == null) return BadRequest("Erro ao tentar adicionar o Ativo.");

                 return Ok(ativo);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar o Ativo. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, AtivoDto model)
        {
            try
            {
                if (model.Id != id) 
                    return StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando atualizar o Ativo errado.");

                 var ativo = await _ativoService.AtualizarAtivo(model);
                 if (ativo == null) return NoContent();

                 return Ok(ativo);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar o Ativo com id: ${id}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var ativo = await _ativoService.GetAtivoByIdAsync(id);
                if (ativo == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar o Ativo que não existe.");

                if(await _ativoService.DeletarAtivo(id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar o Ativo.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar deletar o Ativo com id: ${id}. Erro: {ex.Message}");
            }
        }

    }


}