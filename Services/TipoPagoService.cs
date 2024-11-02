using Parcial_1.Models;
using System.Net.Http.Json;

namespace Parcial_1.Services
{
    public class TipoPagoService
    {
        private readonly HttpClient _httpClient;

        public TipoPagoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<TipoPago>> BuscarTipoPago(string descripcion)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7120/api/TipoPago/buscarTipoPago?descripcion={descripcion}");
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<IEnumerable<TipoPago>>()
                : Enumerable.Empty<TipoPago>();
        }
    }
}
