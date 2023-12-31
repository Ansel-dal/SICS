﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SICS.Shared
{
    public class PrestamoDTO
    {
        public int IdPrestamo { get; set; }
        public string? Codigo { get; set; }
        public int? IdEstadoPrestamo { get; set; }
        public int? IdConsumidor { get; set; }
        public int? IdItem { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public DateTime? FechaConfirmacionDevolucion { get; set; }
        public string? EstadoEntregado { get; set; }
        public string? EstadoRecibido { get; set; }
        public byte[]? FotoEntregado { get; set; }
        public byte[]? FotoRecibido { get; set; }
        public virtual EstadoPrestamoDTO? IdEstadoPrestamoNavigation { get; set; }
        public virtual ConsumidorDTO? IdConsumidorNavigation { get; set; }
        public virtual ItemDTO? IdItemNavigation { get; set; }
    }
}
