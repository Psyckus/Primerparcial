using Parcial_1.Models;
using System.Net.Http.Json;

namespace Parcial_1.Services
{
    public class ClienteService
    {
        private readonly HttpClient _httpClient;

        public ClienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Cliente>> BuscarClientes(string nombre = null, string apellidos = null, string correo = null)
        {
            var query = $"https://localhost:7120/api/Cliente/BuscarClientes?nombre={nombre}&apellidos={apellidos}&correo={correo}";
        
            var response = await _httpClient.GetAsync(query);
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<IEnumerable<Cliente>>()
                : Enumerable.Empty<Cliente>();
        }
    }
}
