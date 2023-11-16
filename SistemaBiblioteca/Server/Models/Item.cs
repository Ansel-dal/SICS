using System;
using System.Collections.Generic;

namespace SICS.Server.Models;

public partial class Item
{
    public int IdItem { get; set; }

    public string? Marca { get; set; }

    public string? Descripcion { get; set; }

    public int? IdCategoria { get; set; }

    public string? Codigo { get; set; }

    public int? Ejemplares { get; set; }

    public byte[]? Foto { get; set; }

    public bool? Estado { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public virtual ICollection<Entrega> Entregas { get; set; } = new List<Entrega>();

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
