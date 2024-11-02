using Parcial_1.Models;
using System.Net.Http.Json;

namespace Parcial_1.Services
{
    public class OpcionCaracteristicaService
    {
        private readonly HttpClient _httpClient;

        public OpcionCaracteristicaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<OpcionCaracteristicaProducto>> BuscarOpcionCaracteristica(string valor)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7120/api/OpcionCaracteristicaProducto/buscarOpcionCaracteristica?valor={valor}");
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<IEnumerable<OpcionCaracteristicaProducto>>()
                : Enumerable.Empty<OpcionCaracteristicaProducto>();
        }
    }
}
