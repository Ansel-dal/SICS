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
    public class PedidoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPedidoRepositorio _PedidoRepositorio;
        public PedidoController(IPedidoRepositorio PedidoRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _PedidoRepositorio = PedidoRepositorio;
        }

        //[HttpGet]
        //[Route("Buscar")]
        //public async Task<IActionResult> Buscar(string estadoPedido, string codigoConsumidor)
        //{
        //    ResponseDTO<List<PedidoDTO>> _ResponseDTO = new ResponseDTO<List<PedidoDTO>>();

        //    try
        //    {
        //        List<PedidoDTO> listaPedido = new List<PedidoDTO>();
        //        IQueryable<Pedido> query = await _PedidoRepositorio.Consultar(
        //            p => p.IdEstadoPedidoNavigation.Descripcion.ToLower().Equals(
        //                estadoPedido.ToLower() == "todos" ? p.IdEstadoPedidoNavigation.Descripcion.ToLower() : estadoPedido.ToLower())
        //            &&
        //            p.IdConsumidorNavigation.Codigo.ToLower().Equals(
        //                    codigoConsumidor == "na" ? p.IdConsumidorNavigation.Codigo.ToLower() : codigoConsumidor.ToLower())
        //            );

        //        query = query.Include(e => e.IdEstadoPedidoNavigation)
        //            .Include(lt => lt.IdConsumidorNavigation);

        //        listaPedido = _mapper.Map<List<PedidoDTO>>(query.ToList());

        //        _ResponseDTO = new ResponseDTO<List<PedidoDTO>>() { status = true, msg = "ok", value = listaPedido };

        //        return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
        //    }
        //    catch (Exception ex)
        //    {
        //        _ResponseDTO = new ResponseDTO<List<PedidoDTO>>() { status = false, msg = ex.Message, value = null };
        //        return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
        //    }
        //}

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<PedidoDTO>> _ResponseDTO = new ResponseDTO<List<PedidoDTO>>();

            try
            {
                List<PedidoDTO> listaPedido = new List<PedidoDTO>();
                IQueryable<Pedido> query = await _PedidoRepositorio.Consultar();
                query = query
                    .Include(lt => lt.IdConsumidorNavigation)
                    .Include(e => e.ProductosPedidos).ThenInclude(pp => pp.IdProductoNavigation)
                    .Include(e=>e.IdEstadoPedidoNavigation);
                listaPedido = _mapper.Map<List<PedidoDTO>>(query.ToList());

                _ResponseDTO = new ResponseDTO<List<PedidoDTO>>() { status = true, msg = "ok", value = listaPedido };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _ResponseDTO = new ResponseDTO<List<PedidoDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }


        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] PedidoDTO request)
        {
            ResponseDTO<PedidoDTO> _ResponseDTO = new ResponseDTO<PedidoDTO>();
            try
            {
                Pedido _Pedido = _mapper.Map<Pedido>(request);

                Pedido _PedidoCreado = await _PedidoRepositorio.Crear(_Pedido);

                if (_PedidoCreado.IdConsumidor != 0)
                    _ResponseDTO = new ResponseDTO<PedidoDTO>() { status = true, msg = "ok", value = _mapper.Map<PedidoDTO>(_PedidoCreado) };
                else
                    _ResponseDTO = new ResponseDTO<PedidoDTO>() { status = false, msg = "No se pudo crear el Pedido" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<PedidoDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }


        //[HttpPut]
        //[Route("Editar")]
        //public async Task<IActionResult> Editar([FromBody] PedidoDTO request)
        //{
        //    ResponseDTO<PedidoDTO> _ResponseDTO = new ResponseDTO<PedidoDTO>();
        //    try
        //    {
        //        Pedido _Pedido = _mapper.Map<Pedido>(request);
        //        Pedido _PedidoParaEditar = await _PedidoRepositorio.Obtener(u => u.IdPedido == _Pedido.IdPedido);

        //        if (_PedidoParaEditar != null)
        //        {

        //            _PedidoParaEditar.IdEstadoPedido = _Pedido.IdEstadoPedido;
        //            _PedidoParaEditar.FechaConfirmacionDevolucion = _Pedido.FechaConfirmacionDevolucion;
        //            _PedidoParaEditar.EstadoRecibido = _Pedido.EstadoRecibido;

        //            bool respuesta = await _PedidoRepositorio.Editar(_PedidoParaEditar);

        //            if (respuesta)
        //                _ResponseDTO = new ResponseDTO<PedidoDTO>() { status = true, msg = "ok", value = _mapper.Map<PedidoDTO>(_PedidoParaEditar) };
        //            else
        //                _ResponseDTO = new ResponseDTO<PedidoDTO>() { status = false, msg = "No se pudo editar el Pedido" };
        //        }
        //        else
        //        {
        //            _ResponseDTO = new ResponseDTO<PedidoDTO>() { status = false, msg = "No se encontró el Pedido" };
        //        }

        //        return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
        //    }
        //    catch (Exception ex)
        //    {
        //        _ResponseDTO = new ResponseDTO<PedidoDTO>() { status = false, msg = ex.Message };
        //        return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
        //    }
        //}

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            ResponseDTO<string> _ResponseDTO = new ResponseDTO<string>();
            try
            {
                Pedido _PedidoEliminar = await _PedidoRepositorio.Obtener(u => u.IdConsumidor == id);

                if (_PedidoEliminar != null)
                {

                    bool respuesta = await _PedidoRepositorio.Eliminar(_PedidoEliminar);

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
