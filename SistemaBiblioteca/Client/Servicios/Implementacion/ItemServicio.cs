using SICS.Client.Servicios.Contrato;
using SICS.Shared;
using System.Net.Http.Json;

namespace SICS.Client.Servicios.Implementacion
{
    public class ItemServicio : IItemServicio
    {
        
        private readonly HttpClient _http;
        public ItemServicio(HttpClient http)
        {
            _http = http;
        }

        public async Task<ResponseDTO<List<ItemDTO>>> Buscar(string valor)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<ItemDTO>>>($"api/item/Buscar?valor={valor}");
            return result!;
        }

        public async Task<ResponseDTO<ItemDTO>> Crear(ItemDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/item/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<ItemDTO>>();
            return response!;
        }

        public async Task<bool> Editar(ItemDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/item/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<ItemDTO>>();

            return response!.status;
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/item/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
        }

        public async Task<ResponseDTO<List<ItemDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<ItemDTO>>>("api/item/Lista");
            return result!;
        }

        public async Task<ResponseDTO<ItemDTO>> Obtener(int idItem)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<ItemDTO>>($"api/item/Obtener/{idItem}");
            return result!;
        }
        public async Task<ResponseDTO<List<ItemDTO>>> Filtrar(string categoriaItem)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<ItemDTO>>>($"api/item/filtrar?categoriaItem={categoriaItem}");
            return result!;
        }
    }
}
