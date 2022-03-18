using System;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.FundosImobiliarios;
using InvestQ.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AdministradorDeFundoImobiliarioController : ControllerBase
    {
        private readonly IAdministradorDeFundoImobiliarioService _administradorDeFundoImobiliarioService;
        public AdministradorDeFundoImobiliarioController(IAdministradorDeFundoImobiliarioService administradorDeFundoImobiliarioService)
        {
            _administradorDeFundoImobiliarioService = administradorDeFundoImobiliarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            try
            {
                 var administradoresDeFundosImobiliarios = await _administradorDeFundoImobiliarioService
                                            .GetAllAdministradoresDeFundosImobiliariosAsync(true);

                 if (administradoresDeFundosImobiliarios == null) return NoContent();

                 return Ok(administradoresDeFundosImobiliarios);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todos os Administradores de Fundos Imobiliários. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdministradorDeFundoImobiliarioById(int id)
        {
            try
            {
                 var administradorDeFundoImobiliario = await _administradorDeFundoImobiliarioService
                                        .GetAdministradorDeFundoImobiliarioByIdAsync(id, true);

                 if (administradorDeFundoImobiliario == null) return NoContent();

                 return Ok(administradorDeFundoImobiliario);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar o Administrador de Fundo Imobiliário com id ${id}. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(AdministradorDeFundoImobiliarioDto model) 
        {
            try
            {
                 var administradorDeFundoImobiliario = await _administradorDeFundoImobiliarioService
                                                            .AdicionarAdministradorDeFundoImobiliario(model);
                 if (administradorDeFundoImobiliario == null) return BadRequest("Erro ao tentar adicionar o Administrador de Fundo Imobiliário.");

                 return Ok(administradorDeFundoImobiliario);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar o Administrador de Fundo Imobiliário. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AdministradorDeFundoImobiliarioDto model)
        {
            try
            {
                if (model.Id != id) 
                    return StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando atualizar o Administrador de Fundo Imobiliário errado.");

                 var administradorDeFundoImobiliario = await _administradorDeFundoImobiliarioService
                                                            .AtualizarAdministradorDeFundoImobiliario(model);
                 if (administradorDeFundoImobiliario == null) return NoContent();

                 return Ok(administradorDeFundoImobiliario);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar o Administrador de Fundo Imobiliário com id: ${id}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var administradorDeFundoImobiliario = await _administradorDeFundoImobiliarioService
                                                        .GetAdministradorDeFundoImobiliarioByIdAsync(id, false);
                if (administradorDeFundoImobiliario == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar um Administrador de Fundo Imobiliário que não existe.");

                if(await _administradorDeFundoImobiliarioService.DeletarAdministradorDeFundoImobiliario(id))
                {
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar o Administrador de Fundo Imobiliário.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar deletar o Administrador de Fundo Imobiliário com id: ${id}. Erro: {ex.Message}");
            }
        }
    }
}