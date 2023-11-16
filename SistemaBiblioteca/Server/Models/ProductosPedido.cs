using System;
using System.Collections.Generic;

namespace SICS.Server.Models;

public partial class ProductosPedido
{
    public int IdTransaccion { get; set; }

    public int? IdPedido { get; set; }

    public int? IdProducto { get; set; }

    public bool? Estado { get; set; }

    public virtual Pedido? IdPedidoNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
