using System;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.TesourosDiretos;
using InvestQ.Application.Interfaces.TesourosDiretos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TesouroDiretoController : ControllerBase
    {
        private readonly ITesouroDiretoService _tesouroDiretoService;
        public TesouroDiretoController(ITesouroDiretoService tesouroDiretoService)
        {
            _tesouroDiretoService = tesouroDiretoService;
            
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            try
            {
                 var tesourosDiretos = await _tesouroDiretoService.GetAllTeseourosDiretosAsync();

                 if (tesourosDiretos == null) return NoContent();

                 return Ok(tesourosDiretos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Tesouros Diretos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTesouroDiretoById(int id)
        {
            try
            {
                 var tesouroDireto = await _tesouroDiretoService.GetTesouroDiretoByIdAsync(id);

                 if (tesouroDireto == null) return NoContent();

                 return Ok(tesouroDireto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Tesouro Direto com id ${id}. Erro: {ex.Message}");
            }
        }

        [HttpGet("{descricao}/descricao")]
        public async Task<IActionResult> GetTesouroDiretoByDescricao(string descricao)
        {
            try
            {
                 var tesouroDireto = await _tesouroDiretoService.GetTesouroDiretoByDescricaoAsync(descricao);

                 if (tesouroDireto == null) return NoContent();

                 return Ok(tesouroDireto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Fundo Imobiliario com a ${descricao}. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tipoDeInvestimentoId}/tipodeinvestimentoid")]
        public async Task<IActionResult> GetTesouroDiretoByTipoDeInvestimentoId(int tipoDeInvestimentoId) 
        {
            try
            {
                 var tesourosDiretos = await _tesouroDiretoService
                                            .GetTeseourosDiretosByTipoDeInvestimentoIdAsync(tipoDeInvestimentoId);

                 if (tesourosDiretos == null) return NoContent();

                 return Ok(tesourosDiretos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Tesouros Diretos. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(TesouroDiretoDto model) 
        {
            try
            {
                 var tesouroDireto = await _tesouroDiretoService.AdicionarTesouroDireto(model);
                 if (tesouroDireto == null) return BadRequest("Erro ao tentar adicionar o Tesouro Direto.");

                 return Ok(tesouroDireto);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar o Tesouro Direto. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TesouroDiretoDto model)
        {
            try
            {
                if (model.Id != id) 
                    return StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando atualizar o Tesouro Direto errado.");

                 var tesouroDireto = await _tesouroDiretoService.AtualizarTesouroDireto(model);
                 if (tesouroDireto == null) return NoContent();

                 return Ok(tesouroDireto);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar o Tesouro Direto com id: ${id}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var tesouroDireto = await _tesouroDiretoService.GetTesouroDiretoByIdAsync(id);
                if (tesouroDireto == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar um Tesouro Direto que não existe.");

                if(await _tesouroDiretoService.DeletarTesouroDireto(id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar o Tesouro Direto.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar deletar o Tesouro Direto com id: ${id}. Erro: {ex.Message}");
            }
        }
    }
}