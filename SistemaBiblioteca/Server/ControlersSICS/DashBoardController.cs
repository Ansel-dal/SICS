﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SICS.Server.RepositorioSICS.Contrato;
using SICS.Shared;

namespace SICS.Server.ControllersSICS
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDashBoardRepositorio _dashboardRepositorio;
        public DashBoardController(IDashBoardRepositorio dashboardRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _dashboardRepositorio = dashboardRepositorio;
        }

        [HttpGet]
        [Route("Resumen")]
        public async Task<IActionResult> Resumen()
        {
            ResponseDTO<DashBoardDTO> _response = new ResponseDTO<DashBoardDTO>();

            try
            {
                DashBoardDTO vmDashboard = new DashBoardDTO();

                vmDashboard.TotalItems = await _dashboardRepositorio.TotalItems();
                vmDashboard.TotalConsumidores = await _dashboardRepositorio.TotalConsumidores();
                vmDashboard.PrestamosRegistrados = await _dashboardRepositorio.PrestamosRegistrados();
                vmDashboard.PrestamosPendientes = await _dashboardRepositorio.PrestamosPendientes();

                _response = new ResponseDTO<DashBoardDTO>() { status = true, msg = "ok", value = vmDashboard };
                return StatusCode(StatusCodes.Status200OK, _response);

            }
            catch (Exception ex)
            {
                _response = new ResponseDTO<DashBoardDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }

    }
}
