using System;
using System.Threading.Tasks;
using InvestQ.API.Extensions;
using InvestQ.Application.Dtos.FundosImobiliarios;
using InvestQ.Application.Interfaces.FundosImobiliarios;
using InvestQ.Data.Paginacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FundoImobiliarioController : ControllerBase
    {
        private readonly IFundoImobiliarioService _fundoImobiliarioService;
        public FundoImobiliarioController(IFundoImobiliarioService fundoImobiliarioService)
        {
            _fundoImobiliarioService = fundoImobiliarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams) 
        {
            try
            {
                 var fundosImobiliarios = await _fundoImobiliarioService.GetAllFundosImobiliariosAsync(pageParams);

                 if (fundosImobiliarios == null) return NoContent();

                 Response.AddPagination(fundosImobiliarios.CurrentPage, 
                                        fundosImobiliarios.PageSize, 
                                        fundosImobiliarios.TotalCount, 
                                        fundosImobiliarios.TotalPages);

                 return Ok(fundosImobiliarios);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Fundos Imobiliários. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFundoImobiliarioById(Guid id)
        {
            try
            {
                 var fundoImobiliario = await _fundoImobiliarioService.GetFundoImobiliarioByIdAsync(id);

                 if (fundoImobiliario == null) return NoContent();

                 return Ok(fundoImobiliario);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Fundo Imobiliário com id ${id}. Erro: {ex.Message}");
            }
        }

        [HttpGet("{nomePregao}/nomepregao")]
        public async Task<IActionResult> GetFundoImobiliarioByDescricao(string descricao)
        {
            try
            {
                 var fundoImobiliario = await _fundoImobiliarioService.GetFundoImobiliarioByDescricaoAsync(descricao);

                 if (fundoImobiliario == null) return NoContent();

                 return Ok(fundoImobiliario);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Fundo Imobiliario com a ${descricao}. Erro: {ex.Message}");
            }
        }

        [HttpGet("{segmentoAnbimaId}/segmentoanbimaid")]
        public async Task<IActionResult> GetFundoImobiliarioBySegmentoAnbimaId(Guid segmentoAnbimaId) 
        {
            try
            {
                 var fundoImobiliario = await _fundoImobiliarioService
                                            .GetFundosImobliariosBySegmentoAnbimaIdAsync(segmentoAnbimaId);

                 if (fundoImobiliario == null) return NoContent();

                 return Ok(fundoImobiliario);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Fundos Imobiliários. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tipoDeInvestimentoId}/tipodeinvestimentoid")]
        public async Task<IActionResult> GetFundoImobiliarioByTipoDeInvestimentoId(Guid tipoDeInvestimentoId) 
        {
            try
            {
                 var fundoImobiliario = await _fundoImobiliarioService
                                            .GetFundosImobliariosByTipoDeInvestimentoIdAsync(tipoDeInvestimentoId);

                 if (fundoImobiliario == null) return NoContent();

                 return Ok(fundoImobiliario);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Fundos Imobiliários. Erro: {ex.Message}");
            }
        }

        [HttpGet("{administradorDeFundoImobiliarioId}/administradordefundoimobiliarioid")]
        public async Task<IActionResult> GetFundoImobiliarioByAdministradorDeFundoImobiliarioId(Guid administradorDeFundoImobiliarioId) 
        {
            try
            {
                 var fundoImobiliario = await _fundoImobiliarioService
                        .GetFundosImobliariosByAdministradorDeFundoImobiliarioIdAsync(administradorDeFundoImobiliarioId);

                 if (fundoImobiliario == null) return NoContent();

                 return Ok(fundoImobiliario);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Fundos Imobiliários. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(FundoImobiliarioDto model) 
        {
            try
            {
                 var fundoImobiliario = await _fundoImobiliarioService.AdicionarFundoImobiliario(model);
                 if (fundoImobiliario == null) return BadRequest("Erro ao tentar adicionar o Fundo Imobiliário.");

                 return Ok(fundoImobiliario);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar o Fundo Imobiliário. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, FundoImobiliarioDto model)
        {
            try
            {
                if (model.Id != id) 
                    return StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando atualizar o Fundo Imobiliário errado.");

                 var fundoImobiliario = await _fundoImobiliarioService.AtualizarFundoImobiliario(model);
                 if (fundoImobiliario == null) return NoContent();

                 return Ok(fundoImobiliario);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar o Fundo Imobiliário com id: ${id}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var fundoImobiliario = await _fundoImobiliarioService.GetFundoImobiliarioByIdAsync(id);
                if (fundoImobiliario == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar um SegmentoAnbima que não existe.");

                if(await _fundoImobiliarioService.DeletarFundoImobiliario(id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar o Fundo Imobiliário.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar deletar o Fundo Imobiliário com id: ${id}. Erro: {ex.Message}");
            }
        }
    }
}