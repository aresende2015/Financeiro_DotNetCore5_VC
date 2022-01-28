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
    public class Corretoracontroller : ControllerBase
    {
        private readonly ICorretoraService _corretoraService;

        public Corretoracontroller(ICorretoraService corretoraService)
        {
            _corretoraService = corretoraService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            try
            {
                 var corretoras = await _corretoraService.GetAllCorretorasAsync(true);

                 if (corretoras == null) return NoContent();

                 return Ok(corretoras);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todas as Corretoras. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCorretoraById(int id)
        {
            try
            {
                 var corretora = await _corretoraService.GetCorretoraByIdAsync(id, true);

                 if (corretora == null) return NoContent();

                 return Ok(corretora);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar a Corretora com id ${id}. Erro: {ex.Message}");
            }
        }

        [HttpGet("{descricao}/descricao")]
        public async Task<IActionResult> GetCorretoraByDescricao(string descricao)
        {
            try
            {
                 var corretora = await _corretoraService.GetCorretoraByDescricaoAsync(descricao, true);

                 if (corretora == null) return NoContent();

                 return Ok(corretora);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar a Corretora com a ${descricao}. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CorretoraDto model) 
        {
            try
            {
                 var corretora = await _corretoraService.AdicionarCorretora(model);
                 if (corretora == null) return BadRequest("Erro ao tentar adicionar a corretora.");

                 return Ok(corretora);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar a Corretora. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CorretoraDto model)
        {
            try
            {
                if (model.Id != id) 
                    return StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando atualizar a Corretora errada.");

                 var corretora = await _corretoraService.AtualizarCorretora(model);
                 if (corretora == null) return NoContent();

                 return Ok(corretora);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar a Corretora com id: ${id}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var corretora = await _corretoraService.GetCorretoraByIdAsync(id, false);
                if (corretora == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar um Corretora que não existe.");

                if(await _corretoraService.DeletarCorretora(id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar a Corretora.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar deletar a Corretora com id: ${id}. Erro: {ex.Message}");
            }
        }
    }
}