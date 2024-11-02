using Parcial_1.Models;
using System.Net.Http.Json;

namespace Parcial_1.Services
{
    public class MetodoPagoService
    {
        private readonly HttpClient _httpClient;

        public MetodoPagoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<MetodoPagoCliente>> BuscarMetodoPago(string proveedor, string cuenta, DateTime? fechaExpira)
        {
            var query = $"https://localhost:7120/api/MetodoPagoCliente/buscarMetodoPago?proveedor{proveedor}&cuenta={cuenta}&fechaExpira={fechaExpira}";
            var response = await _httpClient.GetAsync(query);
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<IEnumerable<MetodoPagoCliente>>()
                : Enumerable.Empty<MetodoPagoCliente>();
        }
    }
}
