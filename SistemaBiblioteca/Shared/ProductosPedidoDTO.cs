using System;
using System.Collections.Generic;

namespace SICS.Shared;

public partial class ProductosPedidoDTO
{
    //public int IdTransaccion { get; set; }

    //public int? IdPedido { get; set; }

    //public int? IdProducto { get; set; }

    public virtual ProductoDTO? IdProductoNavigation { get; set; }
}
