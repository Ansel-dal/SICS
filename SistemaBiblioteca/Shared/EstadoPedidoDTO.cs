using System;
using System.Collections.Generic;

namespace SICS.Shared;

public partial class EstadoPedidoDTO
{
    public int IdEstadoPedido { get; set; }

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

}
