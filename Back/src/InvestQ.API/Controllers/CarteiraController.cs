using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Clientes;
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
    public class CarteiraController : ControllerBase
    {
        private readonly ICarteiraService _carteiraService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CarteiraController(ICarteiraService carteiraService, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _carteiraService = carteiraService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            try
            {
                 var carteiras = await _carteiraService.GetAllCarteirasAsync(true, true);

                 if (carteiras == null) return NoContent();

                 return Ok(carteiras);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todas as Carteiras. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCorretoraById(Guid id)
        {
            try
            {
                 var carteira = await _carteiraService.GetCarteiraByIdAsync(id, true, true);

                 if (carteira == null) return NoContent();

                 return Ok(carteira);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar a Carteira com id ${id}. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CarteiraDto model) 
        {
            try
            {
                 var carteira = await _carteiraService.AdicionarCarteira(model);
                 if (carteira == null) return BadRequest("Erro ao tentar adicionar a carteira.");

                 return Ok(carteira);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar a Cartcarteira. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, CarteiraDto model)
        {
            try
            {
                if (model.Id != id) 
                    return StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando atualizar a Carteira errada.");

                 var carteira = await _carteiraService.AtualizarCarteira(model);
                 if (carteira == null) return NoContent();

                 return Ok(carteira);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar a Carteira com id: ${id}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var carteira = await _carteiraService.GetCarteiraByIdAsync(id, false, false);
                if (carteira == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar um Carteira que não existe.");

                if(await _carteiraService.DeletarCarteira(id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar a Carteira.");
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