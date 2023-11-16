using System;
using System.Collections.Generic;

namespace SICS.Server.Models;

public partial class EstadoPedido
{
    public int IdEstadoPedido { get; set; }

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
