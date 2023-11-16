using System;
using System.Collections.Generic;

namespace SICS.Server.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string? Descripcion { get; set; }

    public string? Tipo { get; set; }

    public bool? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
