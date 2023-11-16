using System;
using System.Collections.Generic;

namespace SICS.Server.Models;

public partial class Prestamo
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

    public bool? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Consumidor? IdConsumidorNavigation { get; set; }

    public virtual EstadoPrestamo? IdEstadoPrestamoNavigation { get; set; }

    public virtual Item? IdItemNavigation { get; set; }
}
