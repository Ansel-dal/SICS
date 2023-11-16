using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SICS.Shared
{
    //la tabla se llama Numero Correlativo
    public partial class IdentificadoresDTO
    {
        public int IdNumeroCorrelativo { get; set; }

        public string Prefijo { get; set; } = null!;

        public string Tipo { get; set; } = null!;

        public int UltimoNumero { get; set; }

        public DateTime? FechaRegistro { get; set; }
    }
}
