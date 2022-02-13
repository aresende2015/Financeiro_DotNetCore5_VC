using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos;
using InvestQ.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubsetorController : ControllerBase
    {
        private readonly ISubsetorService _subsetorService;
        public SubsetorController(ISubsetorService subsertorService)
        {
            _subsetorService =subsertorService;
        }

        [HttpGet("{setorId}")]
        public async Task<IActionResult> Get(int setorId) 
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

        [HttpPut("{setorId}")]
        public async Task<IActionResult> Put(int setorId, SubsetorDto[] models)
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
        public async Task<IActionResult> Delete(int setorId, int subsetorId)
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