using Clases;
using Microsoft.EntityFrameworkCore;

namespace Context
{
    public static class SnippetRepository
    {
        public static async Task CrearSnippetAsync(int? idLenguaje, Snippet snippet)
        {
            using (SnippetsCentreContext bd = new())
            {
                Lenguaje? lenguajeact = await bd.Lenguajes
                    .Include(len => len.Snippets)
                    .FirstOrDefaultAsync(len => len.Id == idLenguaje);

                if (lenguajeact != null)
                {
                    lenguajeact.Snippets.Add(snippet);
                    await bd.SaveChangesAsync();
                }
                else
                {
                    throw new NoEncontradoException("No se Encontró el Lenguaje");
                }
            }
        }

        public static async Task ActualizarSnippetAsync(Snippet snippetActualizado)
        {
            using (SnippetsCentreContext bd = new())
            {
                Snippet? snippet = await bd.Set<Snippet>().FindAsync(snippetActualizado.Id);

                if (snippet != null)
                {
                    snippet.Titulo = snippetActualizado.Titulo;
                    snippet.Codigo = snippetActualizado.Codigo;
                    await bd.SaveChangesAsync();
                }
                else
                {
                    throw new NoEncontradoException("No se Encontró el Snippet");
                }
            }
        }

        public static async Task EliminarSnippetAsync(int? idLenguaje, int? idSnippet)
        {
            using (SnippetsCentreContext bd = new())
            {
                Lenguaje? lenguajeact = await bd.Lenguajes
                    .Include(len => len.Snippets)
                    .FirstOrDefaultAsync(len => len.Id == idLenguaje);

                if (lenguajeact != null)
                {
                    Snippet? snippetElim = lenguajeact.Snippets
                        .FirstOrDefault(s => s.Id == idSnippet);

                    if (snippetElim != null)
                    {
                        lenguajeact.Snippets.Remove(snippetElim);
                        await bd.SaveChangesAsync();
                    }
                    else
                    {
                        throw new NoEncontradoException("No se Encontró el Snippet");
                    }
                }
                else
                {
                    throw new NoEncontradoException("No se Encontró el Lenguaje");
                }
            }
        }
    }
}
