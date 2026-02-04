using Clases;

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
            await Task.Run(() => Lenguajes.WriteXml(rutaxml));
        }

        public static async Task CrearLenguajeAsync(Lenguaje lenguaje)
        {

            var nuevoLenguaje = Lenguajes.dtLenguajes.NewdtLenguajesRow();
            nuevoLenguaje.Nombre = lenguaje.Nombre;
            Lenguajes.dtLenguajes.AdddtLenguajesRow(nuevoLenguaje);
            await GuardarDatos();

        }

        public static async Task EliminarLenguajeAsync(int id)
        {
            var lenguaje = Lenguajes.dtLenguajes.FindById(id);
            if (lenguaje != null)
            {
                lenguaje.Delete();
                await GuardarDatos();
            }
        }

        public static async Task<List<Lenguaje>> EncontrarLenguajesAsync()
        {
            return await Task.Run(() =>
            {
                List<Lenguaje> listaLenguajes = Lenguajes.dtLenguajes
                .Select(row =>
                {
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

        public static async Task<Lenguaje?> EncontrarLenguajeAsync(int idL)
        {
            return await Task.Run(() =>
            {
                var l1 = Lenguajes.dtLenguajes.FindById(idL);
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
            var l1 = Lenguajes.dtLenguajes.FindById(l.Id!.Value);
            if (l1 != null)
            {
                l1.Nombre = l.Nombre;
                await GuardarDatos();
            }
        }

        public static async Task CrearSnippetAsync(int idLenguaje, Snippet snippet)
        {
            var l1 = Lenguajes.dtLenguajes.FindById(idLenguaje);
            if (l1 != null)
            {
                var snip = Lenguajes.dtSnippets.NewdtSnippetsRow();
                snip.Titulo = snippet.Titulo;
                snip.Codigo = snippet.Codigo;
                snip.IdLenguaje = idLenguaje;
                Lenguajes.dtSnippets.AdddtSnippetsRow(snip);
                await GuardarDatos();
            }

        }

        public static async Task ActualizarSnippetAsync(Snippet snippetActualizado)
        {
            var snippet = Lenguajes.dtSnippets.FindById(snippetActualizado.Id!.Value);
            if (snippet != null)
            {
                snippet.Titulo = snippetActualizado.Titulo;
                snippet.Codigo = snippetActualizado.Codigo;
                await GuardarDatos();
            }

        }

        public static async Task EliminarSnippetAsync(int idLenguaje, int idSnippet)
        {
            var snippet = Lenguajes.dtSnippets.FindById(idSnippet);
            if (snippet != null)
            {
                snippet.Delete();
                await GuardarDatos();
            }
        }
    }
}
