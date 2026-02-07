using Clases;
using Context;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SnippetController : Controller
    {

        [HttpPost]
        public async Task<IActionResult> Create(Snippet snip)
        {
            try
            {
                snip.Id = null;
                await SnippetRepository.CrearSnippetAsync(snip.LenguajeId, snip);
                return NoContent();
            }
            catch (NoEncontradoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return Conflict("Hubo un problema con la base de datos");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Snippet Snip)
        {
            try
            {
                Snip.Id = id;
                await SnippetRepository.ActualizarSnippetAsync(Snip);
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
        [HttpDelete("{idLenguaje}/{idSnippet}")]
        public async Task<IActionResult> Delete(int idLenguaje, int idSnippet)
        {
            try
            {
                await SnippetRepository.EliminarSnippetAsync(idLenguaje, idSnippet);
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
