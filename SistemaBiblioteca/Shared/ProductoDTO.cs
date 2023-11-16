using System;
using System.Collections.Generic;

namespace SICS.Shared;

public partial class ProductoDTO
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

    public virtual ItemDTO? IdItemNavigation { get; set; }

}
