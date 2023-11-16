using System;
using System.Collections.Generic;

namespace SICS.Shared;

public partial class EntregaDTO
{
    public int IdEntrega { get; set; }

    public string? Codigo { get; set; }
    public byte[]? FotoEntregado { get; set; }

    public virtual ConsumidorDTO? IdConsumidorNavigation { get; set; }

    public virtual ItemDTO? IdItemNavigation { get; set; }
}
