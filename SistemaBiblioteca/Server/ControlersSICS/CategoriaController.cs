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
    public class CategoriaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        public CategoriaController(ICategoriaRepositorio categoriaRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _categoriaRepositorio = categoriaRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<CategoriaDTO>> _ResponseDTO = new ResponseDTO<List<CategoriaDTO>>();

            try
            {
                List<CategoriaDTO> listaCategorias = new List<CategoriaDTO>();
                var categorias = await _categoriaRepositorio.Lista();

                listaCategorias = _mapper.Map<List<CategoriaDTO>>(categorias);

                _ResponseDTO = new ResponseDTO<List<CategoriaDTO>>() { status = true, msg = "ok", value = listaCategorias };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<CategoriaDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpGet]
        [Route("Filtrar")]
        public async Task<IActionResult> Filtrar(string descripcion)
        {
            ResponseDTO<List<CategoriaDTO>> _ResponseDTO = new ResponseDTO<List<CategoriaDTO>>();

            try
            {
                List<CategoriaDTO> categoria = new List<CategoriaDTO>();
                IQueryable<Categoria> query = await _categoriaRepositorio.Consultar(
                    p => p.Descripcion.ToLower().Equals(
                        descripcion== null ? p.Descripcion.ToLower() : descripcion.ToLower()));


                categoria = _mapper.Map<List<CategoriaDTO>>(query.ToList());

                _ResponseDTO = new ResponseDTO<List<CategoriaDTO>>() { status = true, msg = "ok", value = categoria };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<CategoriaDTO>>() { status = false, msg = ex.Message, value = null };
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
                Categoria _categoriaEliminar = await _categoriaRepositorio.Obtener(u => u.IdCategoria == id);
                if (_categoriaEliminar != null)
                {

                    bool respuesta = await _categoriaRepositorio.Eliminar(_categoriaEliminar);

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
        public async Task<IActionResult> Guardar([FromBody] CategoriaDTO request)
        {
            ResponseDTO<CategoriaDTO> _ResponseDTO = new ResponseDTO<CategoriaDTO>();
            try
            {
                Categoria _Categoria = _mapper.Map<Categoria>(request);

                Categoria _CategoriaCreado = await _categoriaRepositorio.Crear(_Categoria);

                if (_CategoriaCreado.IdCategoria != 0)
                    _ResponseDTO = new ResponseDTO<CategoriaDTO>() { status = true, msg = "ok", value = _mapper.Map<CategoriaDTO>(_CategoriaCreado) };
                else
                    _ResponseDTO = new ResponseDTO<CategoriaDTO>() { status = false, msg = "No se pudo crear el identificador" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<CategoriaDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] CategoriaDTO request)
        {
            ResponseDTO<CategoriaDTO> _ResponseDTO = new ResponseDTO<CategoriaDTO>();
            try
            {
                Categoria _Categoria = _mapper.Map<Categoria>(request);
                Categoria _CategoriaParaEditar = await _categoriaRepositorio.Obtener(u => u.IdCategoria == _Categoria.IdCategoria);

                if (_CategoriaParaEditar != null)
                {

                    _CategoriaParaEditar.Descripcion = _Categoria.Descripcion;

                    bool respuesta = await _categoriaRepositorio.Editar(_CategoriaParaEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<CategoriaDTO>() { status = true, msg = "ok", value = _mapper.Map<CategoriaDTO>(_CategoriaParaEditar) };
                    else
                        _ResponseDTO = new ResponseDTO<CategoriaDTO>() { status = false, msg = "No se pudo editar el identificador" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<CategoriaDTO>() { status = false, msg = "No se encontró el identificador" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<CategoriaDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }
    }
}
