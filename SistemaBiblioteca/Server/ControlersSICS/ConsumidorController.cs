using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SICS.Server.Models;
using SICS.Server.RepositorioSICS.Contrato;
using SICS.Shared;

namespace SICS.Server.ControllersSICS
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumidorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConsumidorRepositorio _ConsumidorRepositorio;
        public ConsumidorController(IConsumidorRepositorio ConsumidorRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _ConsumidorRepositorio = ConsumidorRepositorio;
        }

        [HttpGet]
        [Route("Obtener/{idConsumidor}")]
        public async Task<IActionResult> Obtener(int idConsumidor)
        {
            ResponseDTO<ConsumidorDTO> _ResponseDTO = new ResponseDTO<ConsumidorDTO>();

            try
            {
                ConsumidorDTO ConsumidorDTO;
                Consumidor encontrado = await _ConsumidorRepositorio.Obtener(l => l.IdConsumidor == idConsumidor);

                if(encontrado != null)
                {
                    ConsumidorDTO = _mapper.Map<ConsumidorDTO>(encontrado);
                    _ResponseDTO = new ResponseDTO<ConsumidorDTO>() { status = true, msg = "ok", value = ConsumidorDTO };
                }
                else 
                    _ResponseDTO = new ResponseDTO<ConsumidorDTO>() { status = false, msg = "", value = null };


                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<ConsumidorDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<ConsumidorDTO>> _ResponseDTO = new ResponseDTO<List<ConsumidorDTO>>();

            try
            {
                List<ConsumidorDTO> listaConsumidores = new List<ConsumidorDTO>();
                IQueryable<Consumidor> query = await _ConsumidorRepositorio.Consultar();

                listaConsumidores = _mapper.Map<List<ConsumidorDTO>>(query.ToList());

                _ResponseDTO = new ResponseDTO<List<ConsumidorDTO>>() { status = true, msg = "ok", value = listaConsumidores };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<ConsumidorDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] ConsumidorDTO request)
        {
            ResponseDTO<ConsumidorDTO> _ResponseDTO = new ResponseDTO<ConsumidorDTO>();
            try
            {
                Consumidor _Consumidor = _mapper.Map<Consumidor>(request);

                Consumidor _ConsumidorCreado = await _ConsumidorRepositorio.Crear(_Consumidor);

                if (_ConsumidorCreado.IdConsumidor != 0)
                    _ResponseDTO = new ResponseDTO<ConsumidorDTO>() { status = true, msg = "ok", value = _mapper.Map<ConsumidorDTO>(_ConsumidorCreado) };
                else
                    _ResponseDTO = new ResponseDTO<ConsumidorDTO>() { status = false, msg = "No se pudo crear el Consumidor" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<ConsumidorDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }


        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] ConsumidorDTO request)
        {
            ResponseDTO<ConsumidorDTO> _ResponseDTO = new ResponseDTO<ConsumidorDTO>();
            try
            {
                Consumidor _Consumidor = _mapper.Map<Consumidor>(request);
                Consumidor _ConsumidorParaEditar = await _ConsumidorRepositorio.Obtener(u => u.IdConsumidor == _Consumidor.IdConsumidor);

                if (_ConsumidorParaEditar != null)
                {

                    _ConsumidorParaEditar.Nombre = _Consumidor.Nombre;
                    _ConsumidorParaEditar.Apellido = _Consumidor.Apellido;
                    _ConsumidorParaEditar.Correo = _Consumidor.Correo;

                    bool respuesta = await _ConsumidorRepositorio.Editar(_ConsumidorParaEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<ConsumidorDTO>() { status = true, msg = "ok", value = _mapper.Map<ConsumidorDTO>(_ConsumidorParaEditar) };
                    else
                        _ResponseDTO = new ResponseDTO<ConsumidorDTO>() { status = false, msg = "No se pudo editar el Consumidor" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<ConsumidorDTO>() { status = false, msg = "No se encontró el Consumidor" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<ConsumidorDTO>() { status = false, msg = ex.Message };
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
                Consumidor _ConsumidorEliminar = await _ConsumidorRepositorio.Obtener(u => u.IdConsumidor == id);

                if (_ConsumidorEliminar != null)
                {

                    bool respuesta = await _ConsumidorRepositorio.Eliminar(_ConsumidorEliminar);

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
