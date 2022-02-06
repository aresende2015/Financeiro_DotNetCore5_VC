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

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            try
            {
                 var subsetores = await _subsetorService.GetAllSubsetoresAsync(true);

                 if (subsetores == null) return NoContent();

                 return Ok(subsetores);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Subsetores. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                 var subsetor = await _subsetorService.GetSubsetorByIdAsync(id, true);

                 if (subsetor == null) return NoContent();

                 return Ok(subsetor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Subsetor com id ${id}. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(SubsetorDto model) 
        {
            try
            {
                 var subsetor = await _subsetorService.AdicionarSubsetor(model);
                 if (subsetor == null) return BadRequest("Erro ao tentar adicionar o Subsetor.");

                 return Ok(subsetor);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar um Subsetor. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, SubsetorDto model)
        {
            try
            {
                 var subsetor = await _subsetorService.AtualizarSubsetor(id, model);
                 if (subsetor == null) return NoContent();

                 return Ok(subsetor);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar um Subsetor com id: ${id}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var subsetor = await _subsetorService.GetSubsetorByIdAsync(id,false);
                if (subsetor == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar um Subsetor que não existe.");

                if(await _subsetorService.DeletarSubsetor(id))
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
                    $"Erro ao tentar deletar o Subsetor com id: ${id}. Erro: {ex.Message}");
            }
        }

    }
    
}