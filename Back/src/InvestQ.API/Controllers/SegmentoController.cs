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
    public class SegmentoController : ControllerBase
    {
        private readonly ISegmentoService _segmentoService;
        public SegmentoController(ISegmentoService segmentoService)
        {
            _segmentoService = segmentoService;
        }

        [HttpGet("{subsetorId}")]
        public async Task<IActionResult> Get(int subsetorId) 
        {
            try
            {
                 var segmentos = await _segmentoService.GetSegmentosBySubsetorIdAsync(subsetorId);

                 if (segmentos == null) return NoContent();

                 return Ok(segmentos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Segmentos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{subsetorId}/{segmentoId}")]
        public async Task<IActionResult> Get(int subsetorId, int segmentoId)
        {
            try
            {
                var segmento = await _segmentoService.GetSegmentoByIdsAsync(subsetorId, segmentoId);

                if (segmento == null) return NoContent();

                return Ok(segmento);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Segmento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{subsetorId}")]
        public async Task<IActionResult> Put(int subsetorId, SegmentoDto[] models)
        {
            try
            {
                 var segmentos = await _segmentoService.SalvarSegmentos(subsetorId, models);
                 if (segmentos == null) return NoContent();

                 return Ok(segmentos);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar salvar os Segmentos. Erro: {ex.Message}");
            }
        }

        [HttpPut("{subsetorId}/{segmentoId}")]
        public async Task<IActionResult> SalvarSegmento(int subsetorId, int segmentoId, SegmentoDto model)
        {
            try
            {
                var segmento = await _segmentoService.SalvarSegmento(subsetorId, segmentoId, model);

                if (segmento == null) return NoContent();

                 return Ok(segmento);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar o Segmento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{subsetorId}/{segmentoId}")]
        public async Task<IActionResult> Delete(int subsetorId, int segmentoId)
        {
            try
            {
                var segmento = await _segmentoService.GetSegmentoByIdsAsync(subsetorId, segmentoId);
                if (segmento == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar um Segmento que não existe.");

                if(await _segmentoService.DeletarSegmento(segmento.SubsetorId, segmento.Id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar o Segmento.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar deletar Segmento. Erro: {ex.Message}");
            }
        }
    }
}