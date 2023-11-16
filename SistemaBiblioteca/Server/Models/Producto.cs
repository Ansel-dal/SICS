using System;
using System.Collections.Generic;

namespace SICS.Server.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? Descripcion { get; set; }

    public int? IdItem { get; set; }

    public string? Codigo { get; set; }

    public string? Ubicacion { get; set; }

    public byte[]? Foto { get; set; }

    public byte[]? Factura { get; set; }

    public bool? Estado { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public virtual Item? IdItemNavigation { get; set; }

    public virtual ICollection<ProductosPedido> ProductosPedidos { get; set; } = new List<ProductosPedido>();
}
