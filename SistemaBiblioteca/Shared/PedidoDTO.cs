using System;
using System.Collections.Generic;

namespace SICS.Shared;

public partial class PedidoDTO
{
    public int IdPedido { get; set; }

    public int? IdConsumidor { get; set; }

    public byte[]? FotoPedido { get; set; }

    public byte[]? FotoPedidoFirmado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ConsumidorDTO? IdConsumidorNavigation { get; set; }
    public virtual EstadoPedidoDTO? IdEstadoPedidoNavigation { get; set; }

    public virtual ICollection<ProductosPedidoDTO> ProductosPedidos { get; set; } = new List<ProductosPedidoDTO>();
}
