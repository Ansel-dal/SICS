using SICS.Client.Servicios.Contrato;
using SICS.Shared;
using System.Net.Http.Json;

namespace SICS.Client.Servicios.Implementacion
{
    public class ConsumidorServicio : IConsumidorServicio
    {
        private readonly HttpClient _http;
        public ConsumidorServicio(HttpClient http)
        {
            _http = http;
        }
        public async Task<ResponseDTO<ConsumidorDTO>> Crear(ConsumidorDTO entidad)
        {

            var result = await _http.PostAsJsonAsync("api/Consumidor/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<ConsumidorDTO>>();
            return response!;
        }

        public async Task<bool> Editar(ConsumidorDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/Consumidor/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<ConsumidorDTO>>();

            return response!.status;
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Consumidor/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
        }

        public async Task<ResponseDTO<List<ConsumidorDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<ConsumidorDTO>>>("api/Consumidor/Lista");
            return result!;
        }

        public async Task<ResponseDTO<ConsumidorDTO>> Obtener(int idConsumidor)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<ConsumidorDTO>>($"api/Consumidor/Obtener/{idConsumidor}");
            return result!;
        }
    }
}
