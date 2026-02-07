using System.Net.Http.Headers;

namespace Negocio
{
    public static class Conexion
    {
        public static HttpClient Cliente { get; }

        static Conexion()
        {
            Cliente = new HttpClient();
            Cliente.BaseAddress = new Uri("https://localhost:7109/api/");
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
        }
    }
}
