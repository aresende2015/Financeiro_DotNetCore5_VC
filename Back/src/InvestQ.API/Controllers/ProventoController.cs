using System;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Ativos;
using InvestQ.Application.Interfaces.Ativos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProventoController : ControllerBase
    {
        private readonly IProventoService _proventoService;

        public ProventoController(IProventoService proventoService)
        {
            _proventoService = proventoService;
        }

        [HttpGet("{ativoId}")]
        public async Task<IActionResult> Get(Guid ativoId) 
        {
            try
            {
                 var proventos = await _proventoService.GetAllProventosByAtivoIdAsync(ativoId);

                 if (proventos == null) return NoContent();

                 return Ok(proventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Proventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProventoById(Guid id)
        {
            try
            {
                 var provento = await _proventoService.GetProventoByIdAsync(id);

                 if (provento == null) return NoContent();

                 return Ok(provento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Provento com id ${id}. Erro: {ex.Message}");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(ProventoDto model) 
        {
            try
            {
                 var provento = await _proventoService.AdicionarProvento(model);
                 if (provento == null) return BadRequest("Erro ao tentar adicionar o Provento.");

                 return Ok(provento);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar o Provento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ProventoDto model)
        {
            try
            {
                if (model.Id != id) 
                    return StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando atualizar o Provento errado.");

                 var provento = await _proventoService.AtualizarProvento(model);
                 if (provento == null) return NoContent();

                 return Ok(provento);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar o Provento com id: ${id}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var provento = await _proventoService.GetProventoByIdAsync(id);
                if (provento == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar um Provento que não existe.");

                if(await _proventoService.DeletarProvento(id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar o Provento.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar deletar o Provento com id: ${id}. Erro: {ex.Message}");
            }
        }
    }
}