using Parcial_1.Models;
using System.Net.Http.Json;

namespace Parcial_1.Services
{
    public class ItemOrdenService
    {
        private readonly HttpClient _httpClient;

        public ItemOrdenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ItemOrdenCompra>> BuscarItemOrden(int? cantidad, decimal? precio)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7120/api/ItemOrdenCompra/buscarItemOrden?cantidad={cantidad}&precio={precio}");
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<IEnumerable<ItemOrdenCompra>>()
                : Enumerable.Empty<ItemOrdenCompra>();
        }
    }
}
