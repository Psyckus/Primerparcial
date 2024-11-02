using Parcial_1.Models;
using System.Net.Http.Json;

namespace Parcial_1.Services
{
    public class MetodoEnvioService
    {
        private readonly HttpClient _httpClient;

        public MetodoEnvioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<MetodoEnvio>> BuscarMetodoEnvio(string nombre, decimal? precio)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7120/api/MetodoEnvio/buscarMetodoEnvio?nombre={nombre}&precio={precio}");
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<IEnumerable<MetodoEnvio>>()
                : Enumerable.Empty<MetodoEnvio>();
        }
    }
}
