using Parcial_1.Models;
using System.Net.Http.Json;

namespace Parcial_1.Services
{
    public class ItemProductoService
    {
        private readonly HttpClient _httpClient;

        public ItemProductoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ItemProducto>> BuscarItemProducto(string codigoBarras, int? cantidadDisponible, string urlImagen, decimal? precio)
        {
            var query = $"https://localhost:7120/api/ItemProducto/buscarItemProducto?codigoBarras={codigoBarras}&cantidadDisponible={cantidadDisponible}&urlImagen={urlImagen}&precio={precio}";
            var response = await _httpClient.GetAsync(query);
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<IEnumerable<ItemProducto>>()
                : Enumerable.Empty<ItemProducto>();
        }
    }
}
