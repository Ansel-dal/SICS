using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SICS.Shared;
using SICS.Server.RepositorioSICS.Contrato;
using SICS.Server.Models;
using SICS.Server.RepositorioSICS.Implementacion;
using Microsoft.EntityFrameworkCore;

namespace SICS.Server.ControlersSICS
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductoRepositorio _productoRepositorio;
        public ProductoController(IProductoRepositorio ProductoRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _productoRepositorio = ProductoRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<ProductoDTO>> _ResponseDTO = new ResponseDTO<List<ProductoDTO>>();

            try
            {

                List<ProductoDTO> listaProductos = new List<ProductoDTO>();
                IQueryable<Producto> query = await _productoRepositorio.Consultar();
                query = query.Include(r => r.IdItemNavigation).ThenInclude(r => r.IdCategoriaNavigation);
                listaProductos = _mapper.Map<List<ProductoDTO>>(query.ToList());

                _ResponseDTO = new ResponseDTO<List<ProductoDTO>>() { status = true, msg = "ok", value = listaProductos };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<ProductoDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }
        [HttpGet]
        [Route("Filtrar")]
        public async Task<IActionResult> Filtrar(string categoriaProducto)
        {
            ResponseDTO<List<ProductoDTO>> _ResponseDTO = new ResponseDTO<List<ProductoDTO>>();

            try
            {
                List<ProductoDTO> listaPrestamo = new List<ProductoDTO>();
                IQueryable<Producto> query = await _productoRepositorio.Consultar(p => p.IdItemNavigation.IdCategoriaNavigation.Descripcion == categoriaProducto);

                query = query.Include(e => e.IdItemNavigation).ThenInclude(e => e.IdCategoriaNavigation);

                listaPrestamo = _mapper.Map<List<ProductoDTO>>(query.ToList());

                _ResponseDTO = new ResponseDTO<List<ProductoDTO>>() { status = true, msg = "ok", value = listaPrestamo };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                _ResponseDTO = new ResponseDTO<List<ProductoDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }
        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            ResponseDTO<string> _ResponseDTO = new ResponseDTO<string>();
            try
            {
                Producto _ProductoEliminar = await _productoRepositorio.Obtener(u => u.IdProducto == id);
                if (_ProductoEliminar != null)
                {

                    bool respuesta = await _productoRepositorio.Eliminar(_ProductoEliminar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<string>() { status = true, msg = "ok", value = "" };
                    else
                        _ResponseDTO = new ResponseDTO<string>() { status = false, msg = "No se pudo eliminar el identificador", value = "" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<string>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] ProductoDTO request)
        {
            ResponseDTO<ProductoDTO> _ResponseDTO = new ResponseDTO<ProductoDTO>();
            try
            {
                Producto _Producto = _mapper.Map<Producto>(request);

                Producto _ProductoCreado = await _productoRepositorio.Crear(_Producto);

                if (_ProductoCreado.IdProducto != 0)
                    _ResponseDTO = new ResponseDTO<ProductoDTO>() { status = true, msg = "ok", value = _mapper.Map<ProductoDTO>(_ProductoCreado) };
                else
                    _ResponseDTO = new ResponseDTO<ProductoDTO>() { status = false, msg = "No se pudo crear el identificador" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<ProductoDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] ProductoDTO request)
        {
            ResponseDTO<ProductoDTO> _ResponseDTO = new ResponseDTO<ProductoDTO>();
            try
            {
                Producto _Producto = _mapper.Map<Producto>(request);
                Producto _ProductoParaEditar = await _productoRepositorio.Obtener(u => u.IdProducto == _Producto.IdProducto);

                if (_ProductoParaEditar != null)
                {

                    _ProductoParaEditar.Descripcion = _Producto.Descripcion;

                    bool respuesta = await _productoRepositorio.Editar(_ProductoParaEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<ProductoDTO>() { status = true, msg = "ok", value = _mapper.Map<ProductoDTO>(_ProductoParaEditar) };
                    else
                        _ResponseDTO = new ResponseDTO<ProductoDTO>() { status = false, msg = "No se pudo editar el identificador" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<ProductoDTO>() { status = false, msg = "No se encontró el identificador" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<ProductoDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }
    }
}
