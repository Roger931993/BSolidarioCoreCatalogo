using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Catalog.Application.DTOs
{
    public class parametroDto
    {
        public int? parametros_id { get; set; }
        public string? nombre_parametro { get; set; }
        public string? valor { get; set; }
    }
}
