using Parcial_1.Models;
using System.Net.Http.Json;

namespace Parcial_1.Services
{
    public class PromocionService
    {
        private readonly HttpClient _httpClient;

        public PromocionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Promocion>> BuscarPromocion(string descripcion, int? porcentajeDescuento, DateTime? fechaInicia, DateTime? fechaFinaliza)
        {
            var query = $"https://localhost:7120/api/Promocion/buscarPromocion?descripcion={descripcion}&porcentajeDescuento={porcentajeDescuento}&fechaInicia={fechaInicia}&fechaFinaliza={fechaFinaliza}";
            var response = await _httpClient.GetAsync(query);
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<IEnumerable<Promocion>>()
                : Enumerable.Empty<Promocion>();
        }
    }
}
