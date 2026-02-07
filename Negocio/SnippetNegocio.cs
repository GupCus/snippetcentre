using Clases;
using System.Net.Http.Json;

namespace Negocio
{
    public static class SnippetNegocio
    {
        public async static Task<Boolean> Post(Snippet s)
        {
            var response = await Conexion.Cliente.PostAsJsonAsync("snippet", s);
            return response.IsSuccessStatusCode;
        }

        public async static Task<Boolean> Put(Snippet s)
        {
            var response = await Conexion.Cliente.PutAsJsonAsync($"snippet/{s.Id}", s);
            return response.IsSuccessStatusCode;
        }

        public async static Task<Boolean> Delete(int idLenguaje, int idSnippet)
        {
            var response = await Conexion.Cliente.DeleteAsync($"snippet/{idLenguaje}/{idSnippet}");
            return response.IsSuccessStatusCode;
        }
    }
}
