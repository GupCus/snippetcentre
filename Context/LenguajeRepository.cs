using Clases;
using Microsoft.EntityFrameworkCore;

namespace Context
{
    public class NoEncontradoException : Exception
    {
        public NoEncontradoException(string message) : base(message)
        {
        }
    }
    public static class LenguajeRepository
    {
        public static async Task<Lenguaje> CrearLenguajeAsync(Lenguaje lenguaje)
        {
            using (SnippetsCentreContext bd = new())
            {
                bd.Lenguajes.Add(lenguaje);
                await bd.SaveChangesAsync();
                return lenguaje;
            }
        }

        public static async Task EliminarLenguajeAsync(int? id)
        {
            using (SnippetsCentreContext bd = new())
            {
                Lenguaje? lenguajeelim = await bd.Lenguajes.FindAsync(id);
                if (lenguajeelim != null)
                {
                    bd.Lenguajes.Remove(lenguajeelim);
                    await bd.SaveChangesAsync();
                }
                else
                {
                    throw new NoEncontradoException("No se Encontró el Lenguaje");
                }
            }
        }

        public static async Task<List<Lenguaje>> EncontrarLenguajesAsync()
        {
            using (SnippetsCentreContext bd = new())
            {
                return await bd.Lenguajes.Include(l => l.Snippets).ToListAsync();
            }
        }

        public static async Task<Lenguaje?> EncontrarLenguajeAsync(int? idL)
        {
            using (SnippetsCentreContext bd = new())
            {
                return await bd.Lenguajes
                    .Include(l => l.Snippets)
                    .FirstOrDefaultAsync(l => l.Id == idL);
            }
        }

        public static async Task ActualizarLenguajeAsync(Lenguaje l)
        {
            using (SnippetsCentreContext bd = new())
            {
                Lenguaje? lenguajeact = await bd.Lenguajes.FindAsync(l.Id);
                if (lenguajeact != null)
                {
                    lenguajeact.Nombre = l.Nombre;
                    await bd.SaveChangesAsync();
                }
                else
                {
                    throw new NoEncontradoException("No se Encontró el Lenguaje");
                }
            }
        }
    }
}
