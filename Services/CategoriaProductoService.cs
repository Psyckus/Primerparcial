using Parcial_1.Models;
using System.Net.Http.Json;

namespace Parcial_1.Services
{
    public class CategoriaProductoService
    {
        private readonly HttpClient _httpClient;

        public CategoriaProductoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoriaProducto>> BuscarCategoria(string nombreCategoria)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7120/api/CategoriaProducto/buscarCategoria?nombreCategoria={nombreCategoria}");
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<IEnumerable<CategoriaProducto>>()
                : Enumerable.Empty<CategoriaProducto>();
        }
    }
}
