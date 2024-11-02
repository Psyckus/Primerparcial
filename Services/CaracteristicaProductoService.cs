using Parcial_1.Models;

namespace Parcial_1.Services
{
    public class CaracteristicaProductoService
    {
        private readonly HttpClient _httpClient;

        public CaracteristicaProductoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CaracteristicaProducto>> BuscarCaracteristicaPorNombre(string nombre)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7120/api/CaracteristicaProducto/BuscarCaracteristica?nombre={nombre}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<CaracteristicaProducto>>();
            }

            // Manejo de error en caso de fallo
            return Enumerable.Empty<CaracteristicaProducto>();
        }
    }
}
