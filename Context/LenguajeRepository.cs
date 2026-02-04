using Clases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Context
{
    public static class LenguajeRepository
    {
        public static dsLenguajes Lenguajes = new();

        private static string rutaxml = "xmlSnippetCentre.xml";

        static LenguajeRepository()
        {
            CargarDatos();
        }
        private static void CargarDatos()
        {
            if (File.Exists(rutaxml))
            {
                Lenguajes.ReadXml(rutaxml);
            }
        }
        public static async Task GuardarDatos()
        {
            await Task.Run(()=> Lenguajes.WriteXml(rutaxml));
        }

        public static async Task CrearLenguajeAsync(Lenguaje lenguaje)
        {
            
            var nuevoLenguaje = Lenguajes.dtLenguajes.NewdtLenguajesRow();
            nuevoLenguaje.Nombre = lenguaje.Nombre;
            Lenguajes.dtLenguajes.AdddtLenguajesRow(nuevoLenguaje);
            await GuardarDatos();

        }

        public static async Task EliminarLenguajeAsync(int? id)
        {
            var lenguaje = Lenguajes.dtLenguajes.FirstOrDefault(l => l.Id == id);
            if (lenguaje != null)
            {
                lenguaje.Delete();
                await GuardarDatos();
            }
        }

        public static async Task<List<Lenguaje>> EncontrarLenguajesAsync()
        {
            return await Task.Run(() => {
                List<Lenguaje> listaLenguajes = Lenguajes.dtLenguajes
                .Select(row => {
                    var l = new Lenguaje
                    {
                        Id = row.Id,
                        Nombre = row.Nombre,
                    };
                    l.Snippets.AddRange(row.GetdtSnippetsRows().Select(snip =>
                        new Snippet
                        {
                            Id = snip.Id,
                            Titulo = snip.Titulo,
                            Codigo = snip.Codigo
                        }).ToList());
                    return l;
                }
                ).ToList();
                return listaLenguajes;
            });
        }

        public static async Task<Lenguaje?> EncontrarLenguajeAsync(int? idL)
        {
            return await Task.Run(() =>
            {
                var l1 = Lenguajes.dtLenguajes.FirstOrDefault(l => l.Id == idL);
                if (l1 != null)
                {
                    var l = new Lenguaje
                    {
                        Id = l1.Id,
                        Nombre = l1.Nombre,
                    };
                    l.Snippets.AddRange(l1.GetdtSnippetsRows().Select(snip =>
                        new Snippet
                        {
                            Id = snip.Id,
                            Titulo = snip.Titulo,
                            Codigo = snip.Codigo
                        }).ToList());
                    return l;
                }
                return null;
            }
            
            );
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
            }
        }

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
                }
            }
        }
    }
}
