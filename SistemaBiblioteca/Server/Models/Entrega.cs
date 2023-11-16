using System;
using System.Collections.Generic;

namespace SICS.Server.Models;

public partial class Entrega
{
    public int IdEntrega { get; set; }

    public string? Codigo { get; set; }

    public int? IdConsumidor { get; set; }

    public int? IdItem { get; set; }

    public byte[]? FotoEntregado { get; set; }

    public bool? Estado { get; set; }

    public virtual Consumidor? IdConsumidorNavigation { get; set; }

    public virtual Item? IdItemNavigation { get; set; }
}
