using Parcial_1.Models;
using System.Net.Http.Json;

namespace Parcial_1.Services
{
    public class EstadoOrdenService
    {
        private readonly HttpClient _httpClient;

        public EstadoOrdenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<EstadoOrdenCompra>> BuscarEstadoOrden(string estado)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7120/api/EstadoOrdenCompra/buscarEstadoOrden?estado={estado}");
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<IEnumerable<EstadoOrdenCompra>>()
                : Enumerable.Empty<EstadoOrdenCompra>();
        }
    }
}
