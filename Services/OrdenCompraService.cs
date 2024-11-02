using Parcial_1.Models;
using System.Net.Http.Json;

namespace Parcial_1.Services
{
    public class OrdenCompraService
    {
        private readonly HttpClient _httpClient;

        public OrdenCompraService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<OrdenCompra>> BuscarOrdenCompra(decimal? montoOrden, int? idEstadoOrden)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7120/api/OrdenCompra/buscarOrdenCompra?montoOrden={montoOrden}&idEstadoOrden={idEstadoOrden}");
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<IEnumerable<OrdenCompra>>()
                : Enumerable.Empty<OrdenCompra>();
        }
    }
}
