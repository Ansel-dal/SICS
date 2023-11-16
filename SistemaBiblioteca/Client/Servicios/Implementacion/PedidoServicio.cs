using SICS.Client.Servicios.Contrato;
using SICS.Shared;
using System.Net.Http.Json;

namespace SICS.Client.Servicios.Implementacion
{
    public class PedidoServicio : IPedidoServicio
    {
        private readonly HttpClient _http;
        public PedidoServicio(HttpClient http)
        {
            _http = http;
        }

        public async Task<ResponseDTO<List<PedidoDTO>>> Buscar(string estadoPedido, string codigoConsumidor)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<PedidoDTO>>>($"api/Pedido/Buscar?estadoPedido={estadoPedido}&codigoConsumidor={codigoConsumidor}");
            return result!;
        }

        public async Task<ResponseDTO<PedidoDTO>> Crear(PedidoDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/Pedido/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<PedidoDTO>>();
            return response!;
        }

        public async Task<bool> Editar(PedidoDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/Pedido/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<PedidoDTO>>();
            return response!.status;
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Pedido/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
        }

        public async Task<ResponseDTO<List<PedidoDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<PedidoDTO>>>("api/Pedido/Lista");
            return result!;
        }

        public async Task<ResponseDTO<PedidoDTO>> Obtener(int idPedido)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<PedidoDTO>>($"api/Pedido/Obtener/{idPedido}");
            return result!;
        }
    }
}
