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
    public class FundoImobiliarioController : ControllerBase
    {
        private readonly IFundoImobiliarioService _fundoImobiliarioService;
        public FundoImobiliarioController(IFundoImobiliarioService fundoImobiliarioService)
        {
            _fundoImobiliarioService = fundoImobiliarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            try
            {
                 var fundosImobiliarios = await _fundoImobiliarioService.GetAllFundosImobiliariosAsync();

                 if (fundosImobiliarios == null) return NoContent();

                 return Ok(fundosImobiliarios);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Fundos Imobiliários. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFundoImobiliarioById(int id)
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
        public async Task<IActionResult> GetFundoImobiliarioByNomePregao(string nomePregao)
        {
            try
            {
                 var fundoImobiliario = await _fundoImobiliarioService.GetFundoImobiliarioByNomePregaoAsync(nomePregao);

                 if (fundoImobiliario == null) return NoContent();

                 return Ok(fundoImobiliario);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Fundo Imobiliario com a ${nomePregao}. Erro: {ex.Message}");
            }
        }

        [HttpGet("{segmentoAnbimaId}/segmentoanbimaid")]
        public async Task<IActionResult> GetFundoImobiliarioBySegmentoAnbimaId(int segmentoAnbimaId) 
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
        public async Task<IActionResult> GetFundoImobiliarioByTipoDeInvestimentoId(int tipoDeInvestimentoId) 
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
        public async Task<IActionResult> GetFundoImobiliarioByAdministradorDeFundoImobiliarioId(int administradorDeFundoImobiliarioId) 
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
        public async Task<IActionResult> Put(int id, FundoImobiliarioDto model)
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
        public async Task<IActionResult> Delete(int id)
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