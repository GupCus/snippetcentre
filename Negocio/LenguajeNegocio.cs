using Clases;
using System.Net.Http.Json;

namespace Negocio
{
    public static class LenguajeNegocio
    {
        public async static Task<IEnumerable<Lenguaje>> GetAll()
        {
            var response = await Conexion.Cliente.GetAsync("lenguaje");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Lenguaje>>();
            }

            return [];
        }

        public async static Task<Lenguaje?> GetOne(int id)
        {
            var response = await Conexion.Cliente.GetAsync($"lenguaje/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Lenguaje>();
            }
            return null;

        }
        public async static Task<Boolean> Post(Lenguaje l)
        {
            var response = await Conexion.Cliente.PostAsJsonAsync("lenguaje", l);
            return response.IsSuccessStatusCode;
        }

        public async static Task<Boolean> Put(Lenguaje l)
        {
            var response = await Conexion.Cliente.PutAsJsonAsync($"lenguaje/{l.Id}", l);
            return response.IsSuccessStatusCode;
        }

        public async static Task<Boolean> Delete(int id)
        {
            var response = await Conexion.Cliente.DeleteAsync($"lenguaje/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
