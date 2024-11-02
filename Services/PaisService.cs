using Parcial_1.Models;
using System.Net.Http.Json;

namespace Parcial_1.Services
{
    public class PaisService
    {
        private readonly HttpClient _httpClient;

        public PaisService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Pais>> BuscarPaises(string nombre)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7120/api/Pais/BuscarPaises?nombre={nombre}");
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<IEnumerable<Pais>>()
                : Enumerable.Empty<Pais>();
        }
    }
}
