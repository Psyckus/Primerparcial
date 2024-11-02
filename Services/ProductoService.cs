using Parcial_1.Models;
using System.Net.Http.Json;

namespace Parcial_1.Services
{
    public class ProductoService
    {
        private readonly HttpClient _httpClient;

        public ProductoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Producto>> BuscarProductos(string nombreProducto, string descripcion, string urlImagen)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7120/api/Producto/buscarProductos?nombreProducto={nombreProducto}&descripcion={descripcion}&urlImagen={urlImagen}");
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<IEnumerable<Producto>>()
                : Enumerable.Empty<Producto>();
        }
    }
}
