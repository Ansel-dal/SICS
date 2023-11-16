using SICS.Client.Servicios.Contrato;
using SICS.Shared;
using System.Net.Http.Json;

namespace SICS.Client.Servicios.Implementacion
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly HttpClient _http;
        public ProductoServicio(HttpClient http)
        {
            _http = http;
        }
        public async Task<ResponseDTO<List<ProductoDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<ProductoDTO>>>("api/Producto/lista");
            return result!;
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Producto/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
        }

        public async Task<ResponseDTO<ProductoDTO>> Crear(ProductoDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/Producto/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<ProductoDTO>>();
            return response!;
        }

        public async Task<bool> Editar(ProductoDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/Producto/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<ProductoDTO>>();

            return response!.status;
        }
        public async Task<ResponseDTO<List<ProductoDTO>>> Filtrar(string categoriaItem)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<ProductoDTO>>>($"api/producto/filtrar?categoriaProducto={categoriaItem}");
            return result!;
        }
    }
}
