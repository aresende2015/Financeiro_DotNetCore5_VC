using System;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.FundosImobiliarios;
using InvestQ.Application.Interfaces.FundosImobiliarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SegmentoAnbimaController : ControllerBase
    {
        private readonly ISegmentoAnbimaService _segmentoAnbimaService;
        public SegmentoAnbimaController(ISegmentoAnbimaService segmentoAnbimaService)
        {
            _segmentoAnbimaService = segmentoAnbimaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            try
            {
                 var segmentosAnbimas = await _segmentoAnbimaService.GetAllSegmentosAnbimasAsync(true);

                 if (segmentosAnbimas == null) return NoContent();

                 return Ok(segmentosAnbimas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Segmentos ANBIMA. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSegmentoAnbimaById(int id)
        {
            try
            {
                 var segmentoAnbima = await _segmentoAnbimaService.GetSegmentoAnbimaByIdAsync(id, true);

                 if (segmentoAnbima == null) return NoContent();

                 return Ok(segmentoAnbima);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Segmento ANBIMA com id ${id}. Erro: {ex.Message}");
            }
        }

        [HttpGet("{descricao}/descricao")]
        public async Task<IActionResult> GetSegmentoAnbimaByDescricao(string descricao)
        {
            try
            {
                 var segmentoAnbima = await _segmentoAnbimaService.GetSegmentoAnbimaByDescricaoAsync(descricao, true);

                 if (segmentoAnbima == null) return NoContent();

                 return Ok(segmentoAnbima);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Segmento ANBIMA com a ${descricao}. Erro: {ex.Message}");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(SegmentoAnbimaDto model) 
        {
            try
            {
                 var segmentoAnbima = await _segmentoAnbimaService.AdicionarSegmentoAnbima(model);
                 if (segmentoAnbima == null) return BadRequest("Erro ao tentar adicionar o segmentoAnbima.");

                 return Ok(segmentoAnbima);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar o SegmentoAnbima. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, SegmentoAnbimaDto model)
        {
            try
            {
                if (model.Id != id) 
                    return StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando atualizar o SegmentoAnbima errado.");

                 var segmentoAnbima = await _segmentoAnbimaService.AtualizarSegmentoAnbima(model);
                 if (segmentoAnbima == null) return NoContent();

                 return Ok(segmentoAnbima);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar o SegmentoAnbima com id: ${id}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var segmentoAnbima = await _segmentoAnbimaService.GetSegmentoAnbimaByIdAsync(id, false);
                if (segmentoAnbima == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar um SegmentoAnbima que não existe.");

                if(await _segmentoAnbimaService.DeletarSegmentoAnbima(id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar o SegmentoAnbima.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar deletar o SegmentoAnbima com id: ${id}. Erro: {ex.Message}");
            }
        }
    }
}