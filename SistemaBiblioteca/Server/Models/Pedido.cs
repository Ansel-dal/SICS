using System;
using System.Collections.Generic;

namespace SICS.Server.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public int? IdConsumidor { get; set; }

    public byte[]? FotoPedido { get; set; }

    public byte[]? FotoPedidoFirmado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? IdEstadoPedido { get; set; }

    public virtual Consumidor? IdConsumidorNavigation { get; set; }

    public virtual EstadoPedido? IdEstadoPedidoNavigation { get; set; }

    public virtual ICollection<ProductosPedido> ProductosPedidos { get; set; } = new List<ProductosPedido>();
}
