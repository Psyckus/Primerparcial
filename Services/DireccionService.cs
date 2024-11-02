using Parcial_1.Models;
using System.Net.Http.Json;

namespace Parcial_1.Services
{
    public class DireccionService
    {
        private readonly HttpClient _httpClient;

        public DireccionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Direccion>> BuscarDirecciones(string direccionExacta = null, string codigoPostal = null)
        {
            var query = $"https://localhost:7120/api/Direccion/BuscarDirecciones?direccionExacta={direccionExacta}&codigoPostal={codigoPostal}";
            var response = await _httpClient.GetAsync(query);
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<IEnumerable<Direccion>>()
                : Enumerable.Empty<Direccion>();
        }
    }
}
