using SICS.Client.Servicios.Contrato;
using SICS.Shared;
using System.Net.Http.Json;

namespace SICS.Client.Servicios.Implementacion
{
    public class IdentificadoresServicio : IIdentificadoresServicio
    {
        private readonly HttpClient _http;
        public IdentificadoresServicio(HttpClient http)
        {
            _http = http;
        }
        public async Task<ResponseDTO<List<IdentificadoresDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<IdentificadoresDTO>>>("api/identificadores/lista");
            return result!;
        }
        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/identificadores/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
        }

        public async Task<ResponseDTO<IdentificadoresDTO>> Crear(IdentificadoresDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/identificadores/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<IdentificadoresDTO>>();
            return response!;
        }

        public async Task<bool> Editar(IdentificadoresDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/identificadores/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<IdentificadoresDTO>>();

            return response!.status;
        }
    }
}
