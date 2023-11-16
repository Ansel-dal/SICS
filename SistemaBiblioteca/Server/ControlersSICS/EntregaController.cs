using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SICS.Server.Models;
using SICS.Server.RepositorioSICS.Contrato;
using SICS.Shared;

namespace SICS.Server.ControlersSICS
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntregaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEntregaRepositorio _prestamoRepositorio;
        public EntregaController(IEntregaRepositorio prestamoRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _prestamoRepositorio = prestamoRepositorio;
        }

        
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<EntregaDTO>> _ResponseDTO = new ResponseDTO<List<EntregaDTO>>();

            try
            {
                List<EntregaDTO> listaEntrega = new List<EntregaDTO>();
                IQueryable<Entrega> query = await _prestamoRepositorio.Consultar();
                query = query
                    .Include(lt => lt.IdConsumidorNavigation)
                    .Include(a => a.IdItemNavigation);

                listaEntrega = _mapper.Map<List<EntregaDTO>>(query.ToList());

                _ResponseDTO = new ResponseDTO<List<EntregaDTO>>() { status = true, msg = "ok", value = listaEntrega };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<EntregaDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] EntregaDTO request)
        {
            ResponseDTO<EntregaDTO> _ResponseDTO = new ResponseDTO<EntregaDTO>();
            try
            {
                Entrega _prestamo = _mapper.Map<Entrega>(request);

                Entrega _prestamoCreado = await _prestamoRepositorio.Crear(_prestamo);

                if (_prestamoCreado.IdConsumidor != 0)
                    _ResponseDTO = new ResponseDTO<EntregaDTO>() { status = true, msg = "ok", value = _mapper.Map<EntregaDTO>(_prestamoCreado) };
                else
                    _ResponseDTO = new ResponseDTO<EntregaDTO>() { status = false, msg = "No se pudo crear el prestamo" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<EntregaDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> Buscar(string codigoConsumidor)
        {
            ResponseDTO<List<EntregaDTO>> _ResponseDTO = new ResponseDTO<List<EntregaDTO>>();

            try
            {
                List<EntregaDTO> listaEntrega = new List<EntregaDTO>();
                IQueryable<Entrega> query = await _prestamoRepositorio.Consultar(
                    p => 
                    p.IdConsumidorNavigation.Codigo.ToLower().Equals(
                            codigoConsumidor == "na" ? p.IdConsumidorNavigation.Codigo.ToLower() : codigoConsumidor.ToLower())
                    );

                query = query
                    .Include(lt => lt.IdConsumidorNavigation);

                listaEntrega = _mapper.Map<List<EntregaDTO>>(query.ToList());

                _ResponseDTO = new ResponseDTO<List<EntregaDTO>>() { status = true, msg = "ok", value = listaEntrega };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<EntregaDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] EntregaDTO request)
        {
            ResponseDTO<EntregaDTO> _ResponseDTO = new ResponseDTO<EntregaDTO>();
            try
            {
                Entrega _prestamo = _mapper.Map<Entrega>(request);
                Entrega _prestamoParaEditar = await _prestamoRepositorio.Obtener(u => u.IdEntrega == _prestamo.IdEntrega);

                if (_prestamoParaEditar != null)
                {

                    _prestamoParaEditar.IdItem = _prestamo.IdItem;
                    _prestamoParaEditar.Codigo = _prestamo.Codigo;
                    _prestamoParaEditar.IdConsumidor = _prestamo.IdConsumidor;
                    _prestamoParaEditar.FotoEntregado = _prestamo.FotoEntregado;

                    bool respuesta = await _prestamoRepositorio.Editar(_prestamoParaEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<EntregaDTO>() { status = true, msg = "ok", value = _mapper.Map<EntregaDTO>(_prestamoParaEditar) };
                    else
                        _ResponseDTO = new ResponseDTO<EntregaDTO>() { status = false, msg = "No se pudo editar el prestamo" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<EntregaDTO>() { status = false, msg = "No se encontró el prestamo" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<EntregaDTO>() { status = false, msg = ex.Message };
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
                Entrega _prestamoEliminar = await _prestamoRepositorio.Obtener(u => u.IdConsumidor == id);

                if (_prestamoEliminar != null)
                {

                    bool respuesta = await _prestamoRepositorio.Eliminar(_prestamoEliminar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<string>() { status = true, msg = "ok", value = "" };
                    else
                        _ResponseDTO = new ResponseDTO<string>() { status = false, msg = "No se pudo eliminar el Consumidor", value = "" };
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
