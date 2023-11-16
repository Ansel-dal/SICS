using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SICS.Shared
{
    public class ItemDTO
    {
        public int IdItem { get; set; }
        public string Descripcion { get; set; }
        public string? Marca { get; set; }

        //algún código interno que tenga el ítem
        public string? Codigo { get; set; }

        public int? IdCategoria { get; set; }
        public byte[]? Foto { get; set; }
        public CategoriaDTO? IdCategoriaNavigation { get; set; }
    }
}
