using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Enum;
using InvestQ.Application.Interfaces.Clientes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PortifolioController : ControllerBase
    {
        private readonly IPortifolioService _portifolioService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PortifolioController(IPortifolioService portifolioService, 
                                    IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _portifolioService = portifolioService;
        }

        [HttpGet("carteiraid/{carteiraId}")]
        public async Task<IActionResult> Get(Guid carteiraId) 
        {
            try
            {
                 var portifolio = await _portifolioService.GetAllPortifoliosByCarteiraIdAsync(carteiraId);

                 if (portifolio == null) return NoContent();

                 return Ok(portifolio);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Lançamento de uma carteira. Erro: {ex.Message}");
            }
        }

        [HttpGet("carteiraid/{carteiraId}/{tipoDeAtivo}")]
        public async Task<IActionResult> Get(Guid carteiraId, TipoDeAtivoDto tipoDeAtivo) 
        {
            try
            {
                 var portifolio = await _portifolioService.GetAllPortifoliosByCarteiraIdTipoDeAtivoAsync(carteiraId, tipoDeAtivo);

                 if (portifolio == null) return NoContent();

                 return Ok(portifolio);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Lançamento de uma carteira. Erro: {ex.Message}");
            }
        }
        
    }
}