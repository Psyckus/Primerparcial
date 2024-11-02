using Parcial_1.Models;
using System.Net.Http.Json;

namespace Parcial_1.Services
{
    public class ResenaService
    {
        private readonly HttpClient _httpClient;

        public ResenaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ResenaCliente>> BuscarResenas(int? valorClasificacion)
        {
            // Construcción de la URL con parámetros de consulta
            var query = $"https://localhost:7120/api/ResenaCliente/BuscarResenas?valorClasificacion={valorClasificacion}";
            var response = await _httpClient.GetAsync(query);

            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<IEnumerable<ResenaCliente>>()
                : Enumerable.Empty<ResenaCliente>();
        }
    }
}
