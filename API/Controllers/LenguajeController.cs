using Clases;
using Context;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LenguajeController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lenguajes = await LenguajeRepository.EncontrarLenguajesAsync();
            if (lenguajes.Count != 0)
            {
                return Ok(lenguajes);
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var lenguaje = await LenguajeRepository.EncontrarLenguajeAsync(id);
            if (lenguaje != null)
            {
                return Ok(lenguaje);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Lenguaje len)
        {
            try
            {
                var l = await LenguajeRepository.CrearLenguajeAsync(len);
                return Created($"api/lenguaje/{l.Id}", l);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                return Conflict("El lenguaje ya existe o viola una restricción de la base de datos.");
            }
        }
    }
}
