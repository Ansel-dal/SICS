using SICS.Client.Servicios.Contrato;
using SICS.Shared;
using System.Net.Http.Json;

namespace SICS.Client.Servicios.Implementacion
{
    public class DashBoardServicio : IDashBoardServicio
    {
        private readonly HttpClient _http;
        public DashBoardServicio(HttpClient http)
        {
            _http = http;
        }
        public async Task<ResponseDTO<DashBoardDTO>> Resumen()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<DashBoardDTO>>("api/dashboard/Resumen");
            return result!;
        }
    }
}
