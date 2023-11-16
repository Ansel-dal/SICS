using SICS.Client.Servicios.Contrato;
using SICS.Shared;
using System.Net.Http.Json;

namespace SICS.Client.Servicios.Implementacion
{
    public class EntregaServicio : IEntregaServicio
    {
        private readonly HttpClient _http;
        public EntregaServicio(HttpClient http)
        {
            _http = http;
        }

        public async Task<ResponseDTO<List<EntregaDTO>>> Buscar( string codigoConsumidor)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<EntregaDTO>>>($"api/entrega/Buscar?codigoConsumidor={codigoConsumidor}");
            return result!;
        }

        public async Task<ResponseDTO<EntregaDTO>> Crear(EntregaDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/entrega/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<EntregaDTO>>();
            return response!;
        }

        public async Task<bool> Editar(EntregaDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/entrega/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<EntregaDTO>>();
            return response!.status;
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/entrega/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
        }

        public async Task<ResponseDTO<List<EntregaDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<EntregaDTO>>>("api/entrega/Lista");
            return result!;
        }

        public async Task<ResponseDTO<EntregaDTO>> Obtener(int idEntrega)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<EntregaDTO>>($"api/entrega/Obtener/{idEntrega}");
            return result!;
        }
    }
}
