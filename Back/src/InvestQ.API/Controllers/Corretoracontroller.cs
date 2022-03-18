using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Clientes;
using InvestQ.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CorretoraController : ControllerBase
    {
        private readonly ICorretoraService _corretoraService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CorretoraController(ICorretoraService corretoraService, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _corretoraService = corretoraService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            try
            {
                 var corretoras = await _corretoraService.GetAllCorretorasAsync(true);

                 if (corretoras == null) return NoContent();

                 return Ok(corretoras);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar todas as Corretoras. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCorretoraById(int id)
        {
            try
            {
                 var corretora = await _corretoraService.GetCorretoraByIdAsync(id, true);

                 if (corretora == null) return NoContent();

                 return Ok(corretora);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar a Corretora com id ${id}. Erro: {ex.Message}");
            }
        }

        [HttpGet("{descricao}/descricao")]
        public async Task<IActionResult> GetCorretoraByDescricao(string descricao)
        {
            try
            {
                 var corretora = await _corretoraService.GetCorretoraByDescricaoAsync(descricao, true);

                 if (corretora == null) return NoContent();

                 return Ok(corretora);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar recuperar a Corretora com a ${descricao}. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CorretoraDto model) 
        {
            try
            {
                 var corretora = await _corretoraService.AdicionarCorretora(model);
                 if (corretora == null) return BadRequest("Erro ao tentar adicionar a corretora.");

                 return Ok(corretora);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar a Corretora. Erro: {ex.Message}");
            }
        }

        [HttpPost("upload-image/{corretoraId}")]
        public async Task<IActionResult> UploadImage(int corretoraId) 
        {
            try
            {
                var corretora = await _corretoraService.GetCorretoraByIdAsync(corretoraId, true);

                if (corretora == null) return NoContent();

                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    DeleteImage(corretora.Imagen);
                    corretora.Imagen = await SaveImage(file);
                }

                var CorretoraRetorno = await _corretoraService.AtualizarCorretora(corretora);

                return Ok(CorretoraRetorno);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar a Corretora. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CorretoraDto model)
        {
            try
            {
                if (model.Id != id) 
                    return StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando atualizar a Corretora errada.");

                 var corretora = await _corretoraService.AtualizarCorretora(model);
                 if (corretora == null) return NoContent();

                 return Ok(corretora);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar a Corretora com id: ${id}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var corretora = await _corretoraService.GetCorretoraByIdAsync(id, false);
                if (corretora == null)
                    StatusCode(StatusCodes.Status409Conflict,
                        "Você está tetando deletar um Corretora que não existe.");

                if(await _corretoraService.DeletarCorretora(id))
                {
                    DeleteImage(corretora.Imagen);
                    return Ok(new { message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar a Corretora.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar deletar a Corretora com id: ${id}. Erro: {ex.Message}");
            }
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new string(Path
                                            .GetFileNameWithoutExtension(imageFile.FileName)
                                            .Take(10)
                                            .ToArray()
                                        ).Replace(' ', '-');

            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";

            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/images", imageName);

            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}