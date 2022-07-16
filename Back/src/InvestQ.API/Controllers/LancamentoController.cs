using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.API.Extensions;
using InvestQ.Application.Dtos.Clientes;
using InvestQ.Application.Interfaces.Clientes;
using InvestQ.Data.Paginacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LancamentoController : ControllerBase
    {
        private readonly ILancamentoService _lacamentoService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public LancamentoController(ILancamentoService lancamentoService, 
                                    IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _lacamentoService = lancamentoService;
        }
        
        [HttpGet("carteiraid/{carteiraId}")]
        public async Task<IActionResult> Get(Guid carteiraId, [FromQuery]PageParams pageParams) 
        {
            try
            {
                 var lancamentos = await _lacamentoService.GetAllLancamentosByCarteiraIdAsync(carteiraId, pageParams);

                 if (lancamentos == null) return NoContent();

                 Response.AddPagination(lancamentos.CurrentPage, lancamentos.PageSize, lancamentos.TotalCount, lancamentos.TotalPages);

                 return Ok(lancamentos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Lançamento de uma carteira. Erro: {ex.Message}");
            }
        }

        [HttpGet("possuilancamento/{carteiraId}/{possuiLancamento}")]
        public bool Get(Guid carteiraId, bool possuiLancamento) 
        {
            possuiLancamento = _lacamentoService.GetPossuiLancamentosByCarteiraId(carteiraId);
            
            return possuiLancamento;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLancamentoById(Guid id)
        {
            try
            {
                 var lancamento = await _lacamentoService.GetLancamentoByIdAsync(id, true, true);

                 if (lancamento == null) return NoContent();

                 return Ok(lancamento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Lançamento com id ${id}. Erro: {ex.Message}");
            }
        }

        [HttpGet("{carteiraId}/{ativoId}")]
        public async Task<IActionResult> Get(Guid carteiraId, Guid ativoId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var lancamento = await _lacamentoService.GetAllLancamentosByCarteiraIdAtivoIdAsync(carteiraId, ativoId, pageParams, true, true);

                if (lancamento == null) return NoContent();

                return Ok(lancamento);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Lançamento. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(LancamentoDto model) 
        {
            try
            {
                 var lancamento = await _lacamentoService.AdicionarLancamento(model);
                 if (lancamento == null) return BadRequest("Erro ao tentar adicionar o Lançamento.");

                 return Ok(lancamento);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar o Lançamento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, LancamentoDto model)
        {
            try
            {
                if (model.Id != id) 
                    return StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando atualizar o Lançamento errada.");

                 var lancamento = await _lacamentoService.AtualizarLancamento(model);
                 if (lancamento == null) return NoContent();

                 return Ok(lancamento);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar o Lançamento com id: ${id}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var lancamento = await _lacamentoService.GetLancamentoByIdAsync(id, false, false);
                if (lancamento == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar um Lançamento que não existe.");

                if(await _lacamentoService.DeletarLancamento(id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar o Lançamento.");
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