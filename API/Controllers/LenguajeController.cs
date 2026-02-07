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
                len.Id = null;
                var l = await LenguajeRepository.CrearLenguajeAsync(len);
                return CreatedAtAction(nameof(GetOne), new { id = l.Id }, l);
            }
            catch (Exception)
            {
                return Conflict("Hubo un problema con la base de datos");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Lenguaje l)
        {
            try
            {
                l.Id = id;
                await LenguajeRepository.ActualizarLenguajeAsync(l);
                return NoContent();
            }
            catch (NoEncontradoException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Hubo un problema con la solicitud");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await LenguajeRepository.EliminarLenguajeAsync(id);
                return NoContent();
            }
            catch (NoEncontradoException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Hubo un problema con la solicitud");
            }
        }

    }
}
