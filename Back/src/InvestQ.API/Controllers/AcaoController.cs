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
    public class AcaoController : ControllerBase
    {
        private readonly IAcaoService _acaoService;

        public AcaoController(IAcaoService acaoService)
        {
            _acaoService = acaoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            try
            {
                 var acoes = await _acaoService.GetAllAcoesAsync();

                 if (acoes == null) return NoContent();

                 return Ok(acoes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todas as ações. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAcaoById(Guid id)
        {
            try
            {
                 var acao = await _acaoService.GetAcaoByIdAsync(id);

                 if (acao == null) return NoContent();

                 return Ok(acao);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar a ação com id ${id}. Erro: {ex.Message}");
            }
        }

        [HttpGet("{codigo}/codigo")]
        public async Task<IActionResult> GetAcaoByCodigo(string codigo)
        {
            try
            {
                 var acao = await _acaoService.GetAcaoByCodigoAsync(codigo);

                 if (acao == null) return NoContent();

                 return Ok(acao);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Fundo Imobiliario com a ${codigo}. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tipoDeInvestimentoId}/tipodeinvestimentoid")]
        public async Task<IActionResult> GetAcaoByTipoDeInvestimentoId(Guid tipoDeInvestimentoId) 
        {
            try
            {
                 var acoes = await _acaoService
                                            .GetAcoesByTipoDeInvestimentoIdAsync(tipoDeInvestimentoId);

                 if (acoes == null) return NoContent();

                 return Ok(acoes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todas as ações. Erro: {ex.Message}");
            }
        }

        [HttpGet("{segumentoId}/segmentoid")]
        public async Task<IActionResult> GetAcaoBySegmentoId(Guid segumentoId) 
        {
            try
            {
                 var acoes = await _acaoService
                                            .GetAcoesBySegmentoIdAsync(segumentoId);

                 if (acoes == null) return NoContent();

                 return Ok(acoes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todas as ações. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(AcaoDto model) 
        {
            try
            {
                 var acao = await _acaoService.AdicionarAcao(model);
                 if (acao == null) return BadRequest("Erro ao tentar adicionar a ação.");

                 return Ok(acao);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar a ação. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, AcaoDto model)
        {
            try
            {
                if (model.Id != id) 
                    return StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando atualizar a ação errado.");

                 var acao = await _acaoService.AtualizarAcao(model);
                 if (acao == null) return NoContent();

                 return Ok(acao);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar a ação com id: ${id}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var acao = await _acaoService.GetAcaoByIdAsync(id);
                if (acao == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar um Tesouro Direto que não existe.");

                if(await _acaoService.DeletarAcao(id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar a ação.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar deletar a ação com id: ${id}. Erro: {ex.Message}");
            }
        }
    }
}