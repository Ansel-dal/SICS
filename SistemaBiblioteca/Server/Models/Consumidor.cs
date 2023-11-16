using System;
using System.Collections.Generic;

namespace SICS.Server.Models;

public partial class Consumidor
{
    public int IdConsumidor { get; set; }

    public string? Codigo { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Correo { get; set; }

    public bool? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Entrega> Entregas { get; set; } = new List<Entrega>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
