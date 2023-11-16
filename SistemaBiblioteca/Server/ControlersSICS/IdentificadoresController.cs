using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SICS.Server.Models;
using SICS.Server.RepositorioSICS.Contrato;
using SICS.Server.RepositorioSICS.Implementacion;
using SICS.Shared;

namespace SICS.Server.ControlersSICS
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentificadoresController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IIdentificadoresRepositorio _identificadoresRepositorio;
        public IdentificadoresController(IIdentificadoresRepositorio categoriaRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _identificadoresRepositorio = categoriaRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<IdentificadoresDTO>> _ResponseDTO = new ResponseDTO<List<IdentificadoresDTO>>();

            try
            {
                List<IdentificadoresDTO> listaCategorias = new List<IdentificadoresDTO>();
                var categorias = await _identificadoresRepositorio.Lista();

                listaCategorias = _mapper.Map<List<IdentificadoresDTO>>(categorias);

                _ResponseDTO = new ResponseDTO<List<IdentificadoresDTO>>() { status = true, msg = "ok", value = listaCategorias };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<IdentificadoresDTO>>() { status = false, msg = ex.Message, value = null };
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
                NumeroCorrelativo _IdentificadorEliminar = await _identificadoresRepositorio.Obtener(u => u.IdNumeroCorrelativo == id);

                if (_IdentificadorEliminar != null)
                {

                    bool respuesta = await _identificadoresRepositorio.Eliminar(_IdentificadorEliminar);

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
        public async Task<IActionResult> Guardar([FromBody] IdentificadoresDTO request)
        {
            ResponseDTO<IdentificadoresDTO> _ResponseDTO = new ResponseDTO<IdentificadoresDTO>();
            try
            {
                NumeroCorrelativo _NumeroCorrelativo = _mapper.Map<NumeroCorrelativo>(request);

                NumeroCorrelativo _NumeroCorrelativoCreado = await _identificadoresRepositorio.Crear(_NumeroCorrelativo);

                if (_NumeroCorrelativoCreado.IdNumeroCorrelativo != 0)
                    _ResponseDTO = new ResponseDTO<IdentificadoresDTO>() { status = true, msg = "ok", value = _mapper.Map<IdentificadoresDTO>(_NumeroCorrelativoCreado) };
                else
                    _ResponseDTO = new ResponseDTO<IdentificadoresDTO>() { status = false, msg = "No se pudo crear el identificador" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<IdentificadoresDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] IdentificadoresDTO request)
        {
            ResponseDTO<IdentificadoresDTO> _ResponseDTO = new ResponseDTO<IdentificadoresDTO>();
            try
            {
                NumeroCorrelativo _NumeroCorrelativo = _mapper.Map<NumeroCorrelativo>(request);
                NumeroCorrelativo _NumeroCorrelativoParaEditar = await _identificadoresRepositorio.Obtener(u => u.IdNumeroCorrelativo == _NumeroCorrelativo.IdNumeroCorrelativo);

                if (_NumeroCorrelativoParaEditar != null)
                {

                    _NumeroCorrelativoParaEditar.Tipo = _NumeroCorrelativo.Tipo;
                    _NumeroCorrelativoParaEditar.Prefijo = _NumeroCorrelativo.Prefijo;

                    bool respuesta = await _identificadoresRepositorio.Editar(_NumeroCorrelativoParaEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<IdentificadoresDTO>() { status = true, msg = "ok", value = _mapper.Map<IdentificadoresDTO>(_NumeroCorrelativoParaEditar) };
                    else
                        _ResponseDTO = new ResponseDTO<IdentificadoresDTO>() { status = false, msg = "No se pudo editar el identificador" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<IdentificadoresDTO>() { status = false, msg = "No se encontró el identificador" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<IdentificadoresDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

    }
}
