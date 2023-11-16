using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SICS.Server.Models;
using SICS.Server.RepositorioSICS.Contrato;
using SICS.Server.RepositorioSICS.Implementacion;
using SICS.Shared;

namespace SICS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IItemRepositorio _ItemRepositorio;
        public ItemController(IItemRepositorio ItemRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _ItemRepositorio = ItemRepositorio;
        }

        [HttpGet]
        [Route("Filtrar")]
        public async Task<IActionResult> Filtrar(string categoriaItem)
        {
            ResponseDTO<List<ItemDTO>> _ResponseDTO = new ResponseDTO<List<ItemDTO>>();

            try
            {
                List<ItemDTO> listaPrestamo = new List<ItemDTO>();
                IQueryable<Item> query = await _ItemRepositorio.Consultar(
                    p => p.IdCategoriaNavigation.Descripcion.ToLower().Equals(
                        categoriaItem.ToLower() == "todos" ? p.IdCategoriaNavigation.Descripcion.ToLower() : categoriaItem.ToLower()));

                query = query.Include(e => e.IdCategoriaNavigation);

                listaPrestamo = _mapper.Map<List<ItemDTO>>(query.ToList());

                _ResponseDTO = new ResponseDTO<List<ItemDTO>>() { status = true, msg = "ok", value = listaPrestamo };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<ItemDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpGet]
        [Route("Obtener/{idItem}")]
        public async Task<IActionResult> Obtener(int idItem)
        {
            ResponseDTO<ItemDTO> _ResponseDTO = new ResponseDTO<ItemDTO>();

            try
            {
                ItemDTO ItemDTO;
                Item encontrado = await _ItemRepositorio.Obtener(l => l.IdItem == idItem);

                if (encontrado != null)
                {
                    ItemDTO = _mapper.Map<ItemDTO>(encontrado);
                    _ResponseDTO = new ResponseDTO<ItemDTO>() { status = true, msg = "ok", value = ItemDTO };
                }
                else
                    _ResponseDTO = new ResponseDTO<ItemDTO>() { status = false, msg = "", value = null };


                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<ItemDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> Buscar(string valor)
        {
            ResponseDTO<List<ItemDTO>> _ResponseDTO = new ResponseDTO<List<ItemDTO>>();

            try
            {
                List<ItemDTO> listaItem = new List<ItemDTO>();
                IQueryable<Item> query = await _ItemRepositorio.Consultar(l => l.Descripcion!.ToLower().Contains(valor.ToLower()));
                query = query.Include(r => r.IdCategoriaNavigation);

                listaItem = _mapper.Map<List<ItemDTO>>(query.ToList());

                if (listaItem.Count > 0)
                    _ResponseDTO = new ResponseDTO<List<ItemDTO>>() { status = true, msg = "ok", value = listaItem };
                else
                    _ResponseDTO = new ResponseDTO<List<ItemDTO>>() { status = false, msg = "", value = null };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<ItemDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<ItemDTO>> _ResponseDTO = new ResponseDTO<List<ItemDTO>>();

            try
            {
                List<ItemDTO> listaItem = new List<ItemDTO>();
                IQueryable<Item> query = await _ItemRepositorio.Consultar();
                query = query.Include(r => r.IdCategoriaNavigation);
                listaItem = _mapper.Map<List<ItemDTO>>(query.ToList());

                _ResponseDTO = new ResponseDTO<List<ItemDTO>>() { status = true, msg = "ok", value = listaItem };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<ItemDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] ItemDTO request)
        {
            ResponseDTO<ItemDTO> _ResponseDTO = new ResponseDTO<ItemDTO>();
            try
            {
                Item _Item = _mapper.Map<Item>(request);

                Item _ItemCreado = await _ItemRepositorio.Crear(_Item);

                if (_ItemCreado.IdItem != 0)
                    _ResponseDTO = new ResponseDTO<ItemDTO>() { status = true, msg = "ok", value = _mapper.Map<ItemDTO>(_ItemCreado) };
                else
                    _ResponseDTO = new ResponseDTO<ItemDTO>() { status = false, msg = "No se pudo crear el Item" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<ItemDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }


        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] ItemDTO request)
        {
            ResponseDTO<ItemDTO> _ResponseDTO = new ResponseDTO<ItemDTO>();
            try
            {
                Item _Item = _mapper.Map<Item>(request);
                Item _ItemParaEditar = await _ItemRepositorio.Obtener(u => u.IdItem == _Item.IdItem);

                if (_ItemParaEditar != null)
                {

                    _ItemParaEditar.Descripcion = _Item.Descripcion;
                    _ItemParaEditar.Estado = _Item.Estado;
                    _ItemParaEditar.IdCategoria = _Item.IdCategoria;
                    _ItemParaEditar.Marca = _Item.Marca;
                    _ItemParaEditar.Ejemplares = _Item.Ejemplares;

                    if (_Item.Foto != null)
                        _ItemParaEditar.Foto = _Item.Foto;

                   

                    bool respuesta = await _ItemRepositorio.Editar(_ItemParaEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<ItemDTO>() { status = true, msg = "ok", value = _mapper.Map<ItemDTO>(_ItemParaEditar) };
                    else
                        _ResponseDTO = new ResponseDTO<ItemDTO>() { status = false, msg = "No se pudo editar el Item" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<ItemDTO>() { status = false, msg = "No se encontró el Item" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<ItemDTO>() { status = false, msg = ex.Message };
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
                Item _ItemEliminar = await _ItemRepositorio.Obtener(u => u.IdItem == id);

                if (_ItemEliminar != null)
                {

                    bool respuesta = await _ItemRepositorio.Eliminar(_ItemEliminar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<string>() { status = true, msg = "ok", value = "" };
                    else
                        _ResponseDTO = new ResponseDTO<string>() { status = false, msg = "No se pudo eliminar el Item", value = "" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<string>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

    }
}
