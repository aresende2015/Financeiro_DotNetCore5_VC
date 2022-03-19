using System;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Acoes;
using InvestQ.Application.Interfaces.Acoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SubsetorController : ControllerBase
    {
        private readonly ISubsetorService _subsetorService;
        public SubsetorController(ISubsetorService subsertorService)
        {
            _subsetorService =subsertorService;
        }

        [HttpGet("setor/{setorId}")]
        public async Task<IActionResult> Get(Guid setorId) 
        {
            try
            {
                 var subsetores = await _subsetorService.GetSubsetoresBySetorIdAsync(setorId);

                 if (subsetores == null) return NoContent();

                 return Ok(subsetores);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Subsetores. Erro: {ex.Message}");
            }
        }

        [HttpGet("{subsetorId}")]
        public async Task<IActionResult> GetSubsetor(Guid subsetorId) 
        {
            try
            {
                 var subsetor = await _subsetorService.GetSubsetorByIdAsync(subsetorId);

                 if (subsetor == null) return NoContent();

                 return Ok(subsetor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar Subsetor. Erro: {ex.Message}");
            }
        }

        [HttpPut("{setorId}")]
        public async Task<IActionResult> Put(Guid setorId, SubsetorDto[] models)
        {
            try
            {
                 var subsetores = await _subsetorService.SalvarSubsetores(setorId, models);
                 if (subsetores == null) return NoContent();

                 return Ok(subsetores);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar salvar Subsetores. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{setorId}/{subsetorId}")]
        public async Task<IActionResult> Delete(Guid setorId, Guid subsetorId)
        {
            try
            {
                var subsetor = await _subsetorService.GetSubsetorByIdsAsync(setorId, subsetorId);
                if (subsetor == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar um Subsetor que não existe.");

                if(await _subsetorService.DeletarSubsetor(subsetor.SetorId, subsetor.Id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar o Subsetor.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar deletar Subsetores. Erro: {ex.Message}");
            }
        }

    }
    
}